using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZoomIn.Models;

namespace ZoomIn.Controllers
{
    public class ItemtypesController : Controller
    {
        private readonly ModelContext _context;

        public ItemtypesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Itemtypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Itemtypes.ToListAsync());
        }

        // GET: Itemtypes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemtype = await _context.Itemtypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (itemtype == null)
            {
                return NotFound();
            }

            return View(itemtype);
        }

        // GET: Itemtypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Itemtypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,Typename,Typedescription,Createdate")] Itemtype itemtype)
        {
            if (ModelState.IsValid)
            {
                itemtype.Createdate = DateTime.Now;

                _context.Add(itemtype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemtype);
        }

        // GET: Itemtypes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemtype = await _context.Itemtypes.FindAsync(id);
            if (itemtype == null)
            {
                return NotFound();
            }
            return View(itemtype);
        }

        // POST: Itemtypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("TypeId,Typename,Typedescription,Createdate")] Itemtype itemtype)
        {
            if (id != itemtype.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemtype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemtypeExists(itemtype.TypeId))
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
            return View(itemtype);
        }

        // GET: Itemtypes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemtype = await _context.Itemtypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (itemtype == null)
            {
                return NotFound();
            }

            return View(itemtype);
        }

        // POST: Itemtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var itemtype = await _context.Itemtypes.FindAsync(id);
            _context.Itemtypes.Remove(itemtype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemtypeExists(decimal id)
        {
            return _context.Itemtypes.Any(e => e.TypeId == id);
        }
    }
}
