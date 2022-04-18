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
    public class UserrolesController : Controller
    {
        private readonly ModelContext _context;

        public UserrolesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Userroles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Userroles.ToListAsync());
        }

        // GET: Userroles/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userrole = await _context.Userroles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (userrole == null)
            {
                return NotFound();
            }

            return View(userrole);
        }

        // GET: Userroles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Userroles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,Roleindex,Rolename,Roledescription")] Userrole userrole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userrole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userrole);
        }

        // GET: Userroles/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userrole = await _context.Userroles.FindAsync(id);
            if (userrole == null)
            {
                return NotFound();
            }
            return View(userrole);
        }

        // POST: Userroles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("RoleId,Roleindex,Rolename,Roledescription")] Userrole userrole)
        {
            if (id != userrole.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userrole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserroleExists(userrole.RoleId))
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
            return View(userrole);
        }

        // GET: Userroles/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userrole = await _context.Userroles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (userrole == null)
            {
                return NotFound();
            }

            return View(userrole);
        }

        // POST: Userroles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userrole = await _context.Userroles.FindAsync(id);
            _context.Userroles.Remove(userrole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserroleExists(decimal id)
        {
            return _context.Userroles.Any(e => e.RoleId == id);
        }
    }
}
