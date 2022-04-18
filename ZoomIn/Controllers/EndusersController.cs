using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZoomIn.Common;
using ZoomIn.Models;

namespace ZoomIn.Controllers
{
    public class EndusersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EndusersController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Endusers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Endusers.Include(e => e.Creditc).Include(e => e.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Endusers
        public async Task<IActionResult> UsersReport()
        {
            
            /*ViewData.Keys.ElementAt(0);

            ViewData["CreditcId"] = new SelectList(_context.Creditcards, "CardId", "CardId");

            dynamic mymodel = new ExpandoObject();
            mymodel.Purchases = _context.Userpurchaseitems
                .Include(p => p.Balancetransaction)
                .Include(p => p.Usertransaction);
            mymodel.bTrans = _context.Balancetransactions
                .Include(b => b.Purchase);
            mymodel.uTrans = _context.Usertransactions
                .Include(u => u.Purchase);
            return View(mymodel);

            foreach (var enduser in _context.Endusers)
            {
                var userOwns = _context.Items.Where(i => i.OwnerId == enduser.UserId);
                var userOwns_C = userOwns.Count();
                var tSales_S = _context.Userpurchaseitems.Where(i => userOwns.Any(x => x.ItemId == i.ItemId)).Sum(t => t.Paymenttotal);
                var tPurchases_S = _context.Userpurchaseitems.Where(i => i.BuyerId == enduser.UserId).Sum(t => t.Paymenttotal);
            }*/


            var modelContext = _context.Endusers.Where(u => u.RoleId == 3).Include(e => e.Creditc).Include(e => e.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Endusers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var enduser = await _context.Endusers
                .Include(e => e.Creditc)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);

            var userOwns = _context.Items.Where(i => i.OwnerId == enduser.UserId);
            ViewBag.iOwns = userOwns
                .Include(i => i.Category)
                .Include(i => i.Owner)
                .Include(i => i.Type);
            ViewBag.iOwms_C = userOwns.Count();

            var userLikes = _context.Userlikeitems.Where(l => l.UserId == enduser.UserId);
            ViewBag.iLikes = _context.Items.Where(i => userLikes.Any(x => x.ItemId == i.ItemId))
                .Include(i => i.Category)
                .Include(i => i.Owner)
                .Include(i => i.Type);

            var tSales = _context.Userpurchaseitems.Where(i => userOwns.Any(x => x.ItemId == i.ItemId));
            ViewBag.tSales = tSales;
            ViewBag.tSales_C = tSales.Sum(t => t.Paymenttotal);

            var tPurchases = _context.Userpurchaseitems.Where(i => i.BuyerId == enduser.UserId);
            ViewBag.tPurchases = tPurchases
                .Include(t => t.Item);
            ViewBag.tPurchases_C = tPurchases.Sum(t => t.Paymenttotal);

            ViewBag.iPurchasedItems = _context.Items.Where(i => tPurchases.Any(x => x.ItemId == i.ItemId))
                .Include(i => i.Category)
                .Include(i => i.Owner)
                .Include(i => i.Type);

            if (enduser == null)
            {
                return NotFound();
            }

            return View(enduser);
        }

        // GET: Endusers/Create
        public IActionResult Create()
        {
            ViewData["CreditcId"] = new SelectList(_context.Creditcards, "CardId", "CardId");
            ViewData["RoleId"] = new SelectList(_context.Userroles, "RoleId", "RoleId");

            ViewData["CreditcName"] = new SelectList(_context.Creditcards, "CardId", "Cardnumber");
            ViewData["RoleName"] = new SelectList(_context.Userroles, "RoleId", "Rolename");
            return View();
        }

        // POST: Endusers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Fname,Lname,Country,City,Gender,Birthday,UserImage,Registerdate,UserUsername,UserPassword,ConfirmPassword,UserEmail,RoleId,CreditcId,ImageFile")] Enduser enduser)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await enduser.ImageFile.CopyToAsync(memoryStream);
                    enduser.UserImage = memoryStream.ToArray();
                    enduser.ImageExtension = Path.GetExtension(enduser.ImageFile.FileName).Split('.')[1];
                    
                    enduser.Registerdate = DateTime.Now;
                    _context.Add(enduser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["CreditcId"] = new SelectList(_context.Creditcards, "CardId", "CardId", enduser.CreditcId);
            ViewData["RoleId"] = new SelectList(_context.Userroles, "RoleId", "RoleId", enduser.RoleId);


            ViewData["CreditcName"] = new SelectList(_context.Creditcards, "CardId", "Cardnumber", enduser.CreditcId);
            ViewData["RoleName"] = new SelectList(_context.Userroles, "RoleId", "Rolename", enduser.RoleId);
            return View(enduser);
        }

        // GET: Endusers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enduser = await _context.Endusers.FindAsync(id);
            if (enduser == null)
            {
                return NotFound();
            }
            ViewData["CreditcId"] = new SelectList(_context.Creditcards, "CardId", "CardId");
            ViewData["RoleId"] = new SelectList(_context.Userroles, "RoleId", "RoleId");

            ViewData["CreditcName"] = new SelectList(_context.Creditcards, "CardId", "Cardnumber");
            ViewData["RoleName"] = new SelectList(_context.Userroles, "RoleId", "Rolename");

            /*using (var memoryStream = new MemoryStream(enduser.UserImage))
            {
                enduser.ImageFile = new FormFile(memoryStream, 0, enduser.UserImage.Length, null, Path.GetFileName(memoryStream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/" + enduser.ImageExtension
                };
            }*/

            return View(enduser);
        }

        // POST: Endusers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,Fname,Lname,Country,City,Gender,Birthday,UserImage,Registerdate," +
            "UserUsername,UserPassword,ConfirmPassword,UserEmail,RoleId,CreditcId,ImageFile")] Enduser enduser)
        {
            if (id != enduser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (enduser.ImageFile == null)
                    {
                        enduser.UserImage = _context.Endusers.AsNoTracking().FirstOrDefault(i => i.UserId == enduser.UserId).UserImage;
                        enduser.ImageExtension = _context.Endusers.AsNoTracking().FirstOrDefault(i => i.UserId == enduser.UserId).ImageExtension;

                        _context.Update(enduser);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await enduser.ImageFile.CopyToAsync(memoryStream);
                            enduser.UserImage = memoryStream.ToArray();
                            enduser.ImageExtension = Path.GetExtension(enduser.ImageFile.FileName).Split('.')[1];

                            _context.Update(enduser);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnduserExists(enduser.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreditcId"] = new SelectList(_context.Creditcards, "CardId", "CardId", enduser.CreditcId);
            ViewData["RoleId"] = new SelectList(_context.Userroles, "RoleId", "RoleId", enduser.RoleId);


            ViewData["CreditcName"] = new SelectList(_context.Creditcards, "CardId", "Cardnumber", enduser.CreditcId);
            ViewData["RoleName"] = new SelectList(_context.Userroles, "RoleId", "Rolename", enduser.RoleId);
            return View(enduser);
        }

        // GET: Endusers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enduser = await _context.Endusers
                .Include(e => e.Creditc)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (enduser == null)
            {
                return NotFound();
            }

            return View(enduser);
        }

        // POST: Endusers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var enduser = await _context.Endusers.FindAsync(id);
            _context.Endusers.Remove(enduser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Endusers/AccountSettings/5
        [ServiceFilter(typeof(HomeLayoutFilters))]
        public async Task<IActionResult> AccountSettings(/*decimal? id*/)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserObject") as string))
            {
                return NotFound();
            }

            var enduser = JsonConvert.DeserializeObject<Enduser>(HttpContext.Session.GetString("UserObject"));

            ViewBag.userRoleName = _context.Userroles.Where(r => r.RoleId == enduser.RoleId).FirstOrDefault().Rolename;

            if (enduser == null)
            {
                return NotFound();
            }

            var userOwns = _context.Items.Where(i => i.OwnerId == enduser.UserId);
            ViewBag.iOwms_C = userOwns.Count();

            var tSales = _context.Userpurchaseitems.Where(i => userOwns.Any(x => x.ItemId == i.ItemId));
            ViewBag.tSales = tSales
                .Include(t => t.Item)
                .Include(t => t.Buyer);
            ViewBag.tRevenues_C = tSales.Sum(t => t.Paymenttotal * (1 - t.Relatedsiterate));

            var tPurchases = _context.Userpurchaseitems.Where(i => i.BuyerId == enduser.UserId);
            ViewBag.tPurchases = tPurchases
                .Include(t => t.Item)
                .Include(t => t.Buyer);

            ViewBag.Cover = _context.Items
                .Where(t => (t.OwnerId == enduser.UserId) && ((double)t.Width > (1.2 * (double)t.Height)))
                .OrderByDescending(t => t.Popularity)
                .FirstOrDefault();

            ViewData["CreditcId"] = new SelectList(_context.Creditcards, "CardId", "CardId");
            ViewData["RoleId"] = new SelectList(_context.Userroles, "RoleId", "RoleId");

            ViewData["CreditcName"] = new SelectList(_context.Creditcards, "CardId", "Cardnumber");
            ViewData["RoleName"] = new SelectList(_context.Userroles, "RoleId", "Rolename");
            return View(enduser);
        }

        // POST: Endusers/AccountSettings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountSettings([Bind("UserId,Fname,Lname,Country,City,Gender,Birthday,UserImage,Registerdate," +
            "UserUsername,UserPassword,ConfirmPassword,UserEmail,RoleId,CreditcId,ImageFile")] Enduser enduser)
        {
            if (enduser.UserId != decimal.Parse(HttpContext.Session.GetString("UserID")))
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (enduser.ImageFile == null)
                    {
                        enduser.UserImage = _context.Endusers.AsNoTracking().FirstOrDefault(i => i.UserId == enduser.UserId).UserImage;
                        enduser.ImageExtension = _context.Endusers.AsNoTracking().FirstOrDefault(i => i.UserId == enduser.UserId).ImageExtension;

                        _context.Update(enduser);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await enduser.ImageFile.CopyToAsync(memoryStream);
                            enduser.UserImage = memoryStream.ToArray();
                            enduser.ImageExtension = Path.GetExtension(enduser.ImageFile.FileName).Split('.')[1];

                            _context.Update(enduser);
                            await _context.SaveChangesAsync();
                        }
                    }
                    return RedirectToAction("UserProfile", "Home", new { id = enduser.UserId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnduserExists(enduser.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["CreditcId"] = new SelectList(_context.Creditcards, "CardId", "CardId", enduser.CreditcId);
            ViewData["RoleId"] = new SelectList(_context.Userroles, "RoleId", "RoleId", enduser.RoleId);


            ViewData["CreditcName"] = new SelectList(_context.Creditcards, "CardId", "Cardnumber", enduser.CreditcId);
            ViewData["RoleName"] = new SelectList(_context.Userroles, "RoleId", "Rolename", enduser.RoleId);
            return View(enduser);
        }



        private bool EnduserExists(decimal id)
        {
            return _context.Endusers.Any(e => e.UserId == id);
        }
    }
}
