using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using ZoomIn.Models;

namespace ZoomIn.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ItemsController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Items.Include(i => i.Category).Include(i => i.Owner).Include(i => i.Type);
            return View(await modelContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(decimal? id)
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

            ViewBag.noViews = item.Noviews;
            ViewBag.noLikes = item.Nolikes;
            ViewBag.noPurchases = item.Nopurchases;
            /*ViewBag.noLikes = _context.Userlikeitems.Where(l => l.ItemId == item.ItemId).Count();
            ViewBag.noPurchases = _context.Userpurchaseitems.Where(l => l.ItemId == item.ItemId).Count();*/
            ViewBag.Purchases = _context.Userpurchaseitems.Where(l => l.ItemId == item.ItemId).Include(l => l.Buyer);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "CateId");
            ViewData["OwnerId"] = new SelectList(_context.Endusers, "UserId", "UserId");
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "TypeId");


            ViewData["CategoryName"] = new SelectList(_context.Itemcategories, "CateId", "Catename");
            ViewData["OwnerName"] = new SelectList(_context.Endusers, "UserId", "UserUsername");
            ViewData["TypeName"] = new SelectList(_context.Itemtypes, "TypeId", "Typename");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemInnerFile,Itemname,Uploaddate,Createdate,Width,Height,Price," +
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "CateId", item.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Endusers, "UserId", "UserId", item.OwnerId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "TypeId", item.TypeId);


            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "Catename", item.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Endusers, "UserId", "UserUsername", item.OwnerId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "Typename", item.TypeId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
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
            ViewData["OwnerId"] = new SelectList(_context.Endusers, "UserId", "UserId", item.OwnerId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "TypeId", item.TypeId);

            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "Catename", item.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Endusers, "UserId", "UserUsername", item.OwnerId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "Typename", item.TypeId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ItemId,ItemInnerFile,Itemname,Uploaddate,Createdate,Width,Height,Price," +
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Itemcategories, "CateId", "CateId", item.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Endusers, "UserId", "UserId", item.OwnerId);
            ViewData["TypeId"] = new SelectList(_context.Itemtypes, "TypeId", "TypeId", item.TypeId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
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
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(decimal id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
