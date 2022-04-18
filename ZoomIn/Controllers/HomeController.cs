using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ZoomIn.Models;
using ZoomIn.Common;

namespace ZoomIn.Controllers
{
    [ServiceFilter(typeof(HomeLayoutFilters))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            _context.Add(new Accessloger()
            {
                AccessPage = "Home_Index",
                AccessCounter = _context.Accesslogers.Count() + 1,
                AccessDate = DateTime.Now,
                AccessVisitor = "Guest"
            });
            _context.SaveChangesAsync();

            

            var categoriesImages = new List<Item>();
            foreach(var item in _context.Itemcategories)
            {
                categoriesImages.Add(_context.Items
                    .Where(i => i.CategoryId == item.CateId)
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.CategoryId)
                    .FirstOrDefault());
            }
            ViewBag.CategoriesImages = categoriesImages;

            /* ----- */
            var sliderImages = new List<Item>();
            var topFour = (from t in _context.Items
                           where (double)t.Width > (1.2 * (double)t.Height)
                           orderby t.Popularity descending
                           select t).Take(4).Include(t => t.Owner);
            foreach (var item in topFour)
            {
                sliderImages.Add(item);
            }
            ViewBag.SliderImages = sliderImages;
            /* ----- */

            ViewBag.Reviews = _context.Shopreviews
                .Include(r => r.Reviewer);


            return View();
        }
        
        public async Task<IActionResult> Gallery(string filter)
        {
            ViewBag.Filter = filter;

            var modelContext = _context.Items.Include(i => i.Category).Include(i => i.Owner).Include(i => i.Type).OrderByDescending(i => i.Popularity);
            ViewBag.Categories = _context.Itemcategories;
            return View(await modelContext.ToListAsync());
        }

        // GET: Shopreviews/Create
        public IActionResult About()
        {
            ViewBag.Reviews = _context.Shopreviews
                .Include(r => r.Reviewer);

            ViewBag.totalNoPurchases = _context.Userpurchaseitems.Count();
            ViewBag.totalNoUsers = _context.Endusers.Count();
            ViewBag.totalNoItems = _context.Items.Count();
            ViewBag.totalNoVisits = _context.Accesslogers.Count();

            ViewData["ReviewerId"] = new SelectList(_context.Endusers, "UserId", "ImageExtension");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> About([Bind("ReviewId,Rate,Reviewcomment,ReviewerId,ReviewDate")] Shopreview shopreview)
        {
            if (ModelState.IsValid)
            {
                if (shopreview.Rate == null)
                    shopreview.Rate = 1;

                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserObject") as string))
                {
                    var enduser = JsonConvert.DeserializeObject<Enduser>(HttpContext.Session.GetString("UserObject"));
                    shopreview.ReviewerId = enduser.UserId;
                    shopreview.ReviewDate = DateTime.Now;

                    var sessionUser = await _context.Endusers.FindAsync(shopreview.ReviewerId);
                    if (sessionUser != null)
                    {
                        try
                        {
                            _context.Shopreviews.Remove(_context.Shopreviews.Where(sr => sr.ReviewerId == sessionUser.UserId).FirstOrDefault());
                        } catch
                        { }
                    }
                    _context.Add(shopreview);
                    await _context.SaveChangesAsync();

                    ViewBag.Reviews = _context.Shopreviews
                        .Include(r => r.Reviewer);

                    return View();
                }
            }
            ViewData["ReviewerId"] = new SelectList(_context.Endusers, "UserId", "ImageExtension", shopreview.ReviewerId);
            return View(shopreview);
        }

        public IActionResult Contact()
        {
            return View();
        }


        public async Task<IActionResult> UserProfile(decimal? id)
        {
            if (id == null)
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserObject") as string))
                {
                    var sessionUser = JsonConvert.DeserializeObject<Enduser>(HttpContext.Session.GetString("UserObject"));
                    id = sessionUser.UserId;
                }
                else
                    return RedirectToAction("Index", "Home");
            }

            var enduser = await _context.Endusers
                .Include(e => e.Creditc)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);

            //Collections
            //Photos
            var userOwns = _context.Items.Where(i => i.OwnerId == enduser.UserId);
            ViewBag.pOwns = userOwns
                .Include(i => i.Category)
                .Include(i => i.Owner)
                .Include(i => i.Type);

            //Likes
            var userLikes = _context.Userlikeitems.Where(l => l.UserId == enduser.UserId);
            ViewBag.pLikes = _context.Items.Where(i => userLikes.Any(x => x.ItemId == i.ItemId))
                .Include(i => i.Category)
                .Include(i => i.Owner)
                .Include(i => i.Type);

            //Purchases
            var userPurchases = _context.Userpurchaseitems.Where(i => i.BuyerId == enduser.UserId);
            ViewBag.tPurchases = _context.Items.Where(i => userPurchases.Any(x => x.ItemId == i.ItemId));


            // Counts:
            //Photos
            ViewBag.photos_C = userOwns.Count();

            //Likes
            ViewBag.likes_C = userOwns.Sum(t => t.Nolikes);

            //Sales
            ViewBag.sales_C = userOwns.Sum(t => t.Nopurchases);

            //Views
            ViewBag.views_C = userOwns.Sum(t => t.Noviews);

            ViewBag.Categories = _context.Itemcategories;

            ViewBag.Cover = _context.Items
                .Where(t => (t.OwnerId == enduser.UserId) && ((double)t.Width > (1.2 * (double)t.Height)))
                .OrderByDescending(t => t.Popularity)
                .FirstOrDefault();

            if (enduser == null)
            {
                return NotFound();
            }
            return View(enduser);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> ItemProfile(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.Owner)
                .Include(i => i.Type)
                .FirstOrDefaultAsync(m => m.ItemId == id);

            item.Noviews++;
            item.Popularity = item.Noviews + item.Nolikes + item.Nopurchases;
            _context.Items.Attach(item).Property(x => x.Noviews).IsModified = true;
            await _context.SaveChangesAsync();
            _context.Items.Attach(item).Property(x => x.Popularity).IsModified = true;
            await _context.SaveChangesAsync();

            ViewBag.noViews = item.Noviews;
            //ViewBag.noLikes = _context.Userlikeitems.Where(l => l.ItemId == item.ItemId).Count();
            ViewBag.noLikes = item.Nolikes;
            //ViewBag.noPurchases = _context.Userpurchaseitems.Where(l => l.ItemId == item.ItemId).Count();
            ViewBag.noPurchases = item.Nopurchases;
            ViewBag.Purchases = _context.Userpurchaseitems.Where(l => l.ItemId == item.ItemId).Include(l => l.Buyer);


            ViewBag.isLike = false;
            ViewBag.isPurchased = false;
            Enduser sessionUser = null;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserObject") as string))
            {
                sessionUser = JsonConvert.DeserializeObject<Enduser>(HttpContext.Session.GetString("UserObject"));

                //Like
                var like = _context.Userlikeitems.Where(l => l.ItemId == id && l.UserId == sessionUser.UserId).FirstOrDefault();
                if (like != null)
                    ViewBag.isLike = true;

                //Purchase
                var purchase = _context.Userpurchaseitems.Where(p => p.ItemId == id && p.BuyerId == sessionUser.UserId).FirstOrDefault();
                if (purchase != null)
                    ViewBag.isPurchased = true;
            }

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public JsonResult LikeItem(string itemId, string userId)
        {
            var like = _context.Userlikeitems.Where(l => l.ItemId == decimal.Parse(itemId) && l.UserId == decimal.Parse(userId)).FirstOrDefault();

            var item = _context.Items.FirstOrDefault(m => m.ItemId == decimal.Parse(itemId));
            if (like != null)
            {
                _context.Userlikeitems.Remove(like);
                _context.SaveChangesAsync();

                item.Nolikes--;

                _context.Items.Attach(item).Property(x => x.Nolikes).IsModified = true;
                _context.SaveChangesAsync();

                item.Popularity = item.Noviews + item.Nolikes + item.Nopurchases;
                _context.Items.Attach(item).Property(x => x.Popularity).IsModified = true;
                _context.SaveChangesAsync();
            }
            else
            {
                _context.Userlikeitems.Add(new Userlikeitem
                {
                    Likedate = DateTime.Now,
                    ItemId = decimal.Parse(itemId),
                    UserId = decimal.Parse(userId)
                });
                _context.SaveChangesAsync();

                item.Nolikes++;

                _context.Items.Attach(item).Property(x => x.Nolikes).IsModified = true;
                _context.SaveChangesAsync();

                item.Popularity = item.Noviews + item.Nolikes + item.Nopurchases;
                _context.Items.Attach(item).Property(x => x.Popularity).IsModified = true;
                _context.SaveChangesAsync();
            }

            return Json("true");
        }


        public IActionResult Upload()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserObject") as string))
            {
                ViewBag.OwnerId = decimal.Parse(HttpContext.Session.GetString("UserID"));
            }

            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "CateId");
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "TypeId");

            ViewData["CategoryName"] = new SelectList(_context.Itemcategories, "CateId", "Catename");
            ViewData["TypeName"] = new SelectList(_context.Itemtypes, "TypeId", "Typename");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload([Bind("ItemId,ItemInnerFile,Itemname,Uploaddate,Createdate,Width,Height,Price," +
            "Itemdescription,Takenlocation,Noviews,Nolikes,Nopurchases,Popularity," +
            "TypeId,CategoryId,OwnerId,ItemFile")] Item item)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await item.ItemFile.CopyToAsync(memoryStream);
                    item.ItemInnerFile = memoryStream.ToArray();

                    item.ItemExtension = Path.GetExtension(item.ItemFile.FileName).Split('.')[1];
                    item.Uploaddate = DateTime.Now;

                    _context.Add(item);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("UserProfile", "Home", new { id = item.OwnerId });
            }
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserObject") as string))
            {
                item.OwnerId = decimal.Parse(HttpContext.Session.GetString("UserID"));
                ViewBag.OwnerId = item.OwnerId;
            }

            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "CateId", item.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "TypeId", item.TypeId);

            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "Catename", item.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "Typename", item.TypeId);
            return View(item);
        }


        public IActionResult Purchase(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _context.Items.FirstOrDefault(m => m.ItemId == id);

            ViewBag.ItemName = item.Itemname;
            ViewBag.ItemInnerFile = item.ItemInnerFile;
            ViewBag.ItemExtension = item.ItemExtension;
            ViewBag.ItemID = item.ItemId;
            ViewBag.tPrice = item.Price;
            ViewBag.Itemdescription = item.Itemdescription;
            var fees = _context.Balances.FirstOrDefault(r => r.BalanceLock == "X").Profitrate;
            ViewBag.FeesP = fees * 100;
            ViewBag.FeesV = item.Price * fees;
            ViewBag.uPrice = item.Price * (1 - fees);

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserObject") as string))
            {
                var sessionUser = JsonConvert.DeserializeObject<Enduser>(HttpContext.Session.GetString("UserObject"));
                ViewBag.BuyerId = sessionUser.UserId;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase([Bind("PurchaseId,Purchasedate,Paymenttotal,Relatedsiterate,BuyerId,ItemId")] Userpurchaseitem purchase)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserObject") as string))
                {
                    var buyerCredCard = _context.Creditcards
                    .Where(c => c.CardId ==
                    (_context.Endusers.Where(u => u.UserId == purchase.BuyerId).FirstOrDefault().CreditcId))
                    .FirstOrDefault();
                    if (buyerCredCard.Balance >= purchase.Paymenttotal)
                    {
                        purchase.Purchasedate = DateTime.Now;
                        purchase.Paymenttotal = _context.Items.Where(i => i.ItemId == purchase.ItemId).SingleOrDefault().Price;
                        purchase.Relatedsiterate = _context.Balances.Where(b => b.BalanceLock == "X").SingleOrDefault().Profitrate;

                        _context.Add(purchase);
                        await _context.SaveChangesAsync();

                        var bTrans = new Balancetransaction();
                        bTrans.Btransactiondate = DateTime.Now;
                        bTrans.Paymenttotal = purchase.Paymenttotal;
                        bTrans.Relatedsiterate = purchase.Relatedsiterate;
                        bTrans.Btransactionvalue = bTrans.Paymenttotal * bTrans.Relatedsiterate;
                        bTrans.Btdescription = $"User: [{purchase.BuyerId}] have purchased: [{purchase.ItemId}] at the value of [{purchase.Paymenttotal}].";
                        bTrans.PurchaseId = purchase.PurchaseId;
                        bTrans.FromId = purchase.BuyerId;
                        bTrans.Balancelock = _context.Balances.FirstOrDefault().BalanceLock;

                        _context.Add(bTrans);
                        await _context.SaveChangesAsync();

                        var uTrans = new Usertransaction();
                        uTrans.Utransactiondate = DateTime.Now;
                        uTrans.Paymenttotal = purchase.Paymenttotal;
                        uTrans.Relatedsiterate = purchase.Relatedsiterate;
                        uTrans.Utransactionvalue = bTrans.Paymenttotal * (1 - bTrans.Relatedsiterate);
                        uTrans.PurchaseId = purchase.PurchaseId;
                        uTrans.FromId = purchase.BuyerId;
                        uTrans.ToId = _context.Items.Where(i => i.ItemId == purchase.ItemId).SingleOrDefault().OwnerId;
                        uTrans.Utdescription = $"User: [{uTrans.FromId}] have purchased: [{purchase.ItemId}] from user: [{uTrans.ToId}], at the value of [{purchase.Paymenttotal}].";

                        _context.Add(uTrans);
                        await _context.SaveChangesAsync();

                        var tempItem = _context.Items.Where(i => i.ItemId == purchase.ItemId).FirstOrDefault();
                        tempItem.Nopurchases++;
                        _context.Items.Attach(tempItem).Property(x => x.Nopurchases).IsModified = true;
                        await _context.SaveChangesAsync();

                        tempItem.Popularity = tempItem.Noviews + tempItem.Nolikes + tempItem.Nopurchases;
                        _context.Items.Attach(tempItem).Property(x => x.Popularity).IsModified = true;
                        await _context.SaveChangesAsync();

                        _context.Creditcards
                            .Where(c => c.CardId ==
                            (_context.Endusers.Where(u => u.UserId == uTrans.FromId).FirstOrDefault().CreditcId))
                            .FirstOrDefault()
                            .Balance -= purchase.Paymenttotal;

                        _context.Balances
                            .FirstOrDefault()
                            .BalanceValue += bTrans.Btransactionvalue;

                        _context.Creditcards
                            .Where(c => c.CardId ==
                            (_context.Endusers.Where(u => u.UserId == uTrans.ToId).FirstOrDefault().CreditcId))
                            .FirstOrDefault()
                            .Balance += uTrans.Utransactionvalue;

                        await _context.SaveChangesAsync();

                        return RedirectToAction("PurchaseComplete", "Home");
                    }
                }
            }


            var item = _context.Items.FirstOrDefault(m => m.ItemId == purchase.ItemId);

            ViewBag.ItemName = item.Itemname;
            ViewBag.ItemInnerFile = item.ItemInnerFile;
            ViewBag.ItemExtension = item.ItemExtension;
            ViewBag.ItemID = item.ItemId;
            ViewBag.tPrice = item.Price;
            ViewBag.Itemdescription = item.Itemdescription;
            var fees = _context.Balances.FirstOrDefault(r => r.BalanceLock == "X").Profitrate;
            ViewBag.FeesP = fees * 100;
            ViewBag.FeesV = item.Price * fees;
            ViewBag.uPrice = item.Price * (1 - fees);

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserObject") as string))
            {
                var sessionUser = JsonConvert.DeserializeObject<Enduser>(HttpContext.Session.GetString("UserObject"));
                ViewBag.BuyerId = sessionUser.UserId;
            }
            return View(purchase);
        }

        public IActionResult PurchaseComplete()
        {
            return View();
        }

        public FileResult Download(decimal id)
        {
            var fileToRetrieve = _context.Items.FirstOrDefault(m => m.ItemId == id);
            return File(fileToRetrieve.ItemInnerFile, "image/" + fileToRetrieve.ItemExtension);
        }


        public async Task<IActionResult> EditItem(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "CateId", item.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "TypeId", item.TypeId);

            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "Catename", item.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "Typename", item.TypeId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(decimal id, 
            [Bind("ItemId,ItemInnerFile,Itemname,Uploaddate,Createdate,Width,Height,Price," +
            "Itemdescription,Takenlocation,Noviews,Nolikes,Nopurchases,Popularity," +
            "TypeId,CategoryId,OwnerId,ItemFile")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (item.ItemFile == null)
                    {
                        item.ItemInnerFile = _context.Items.AsNoTracking().FirstOrDefault(i => i.ItemId == item.ItemId).ItemInnerFile;
                        item.ItemExtension = _context.Items.AsNoTracking().FirstOrDefault(i => i.ItemId == item.ItemId).ItemExtension;

                        _context.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await item.ItemFile.CopyToAsync(memoryStream);
                            item.ItemInnerFile = memoryStream.ToArray();

                            item.ItemExtension = Path.GetExtension(item.ItemFile.FileName).Split('.')[1];
                            _context.Update(item);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ItemProfile", "Home", new { id = item.ItemId });
            }
            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "CateId", item.CategoryId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "TypeId", item.TypeId);
            return View(item);
        }

        // POST: Items/Delete/5
        [ActionName("DeleteItem")]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Gallery));
        }



        private bool ItemExists(decimal id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
