using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using venezia.Data;
using venezia.Models;

namespace venezia.Controllers
{
    public class MenuitemsController : Controller
    {
        private readonly veneziaContext _context;

        public MenuitemsController(veneziaContext context)
        {
            _context = context;
        }

        // GET: Menuitems
        public async Task<IActionResult> List(uint? id)
        {
            var selectedMenu = _context.Menu
                .FirstOrDefault(m => m.Id == id);
            ViewData["Title"] = selectedMenu.Name;

            var veneziaContext = _context.Menuitem
                .Include(m => m.Section)
                .Where(m => m.MenuId == id);
            return View(await veneziaContext.ToListAsync());
        }

        // GET: Menuitems/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuitem = await _context.Menuitem
                .Include(m => m.Menu)
                .Include(m => m.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuitem == null)
            {
                return NotFound();
            }

            return View(menuitem);
        }

        // GET: Menuitems/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id");
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Id");
            return View();
        }

        // POST: Menuitems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuId,SectionId,Name,Description,Price")] Menuitem menuitem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuitem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", menuitem.MenuId);
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Id", menuitem.SectionId);
            return View(menuitem);
        }

        // GET: Menuitems/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuitem = await _context.Menuitem.FindAsync(id);
            if (menuitem == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", menuitem.MenuId);
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Id", menuitem.SectionId);
            return View(menuitem);
        }

        // POST: Menuitems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,MenuId,SectionId,Name,Description,Price")] Menuitem menuitem)
        {
            if (id != menuitem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuitemExists(menuitem.Id))
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
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", menuitem.MenuId);
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Id", menuitem.SectionId);
            return View(menuitem);
        }

        // GET: Menuitems/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuitem = await _context.Menuitem
                .Include(m => m.Menu)
                .Include(m => m.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuitem == null)
            {
                return NotFound();
            }

            return View(menuitem);
        }

        // POST: Menuitems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var menuitem = await _context.Menuitem.FindAsync(id);
            _context.Menuitem.Remove(menuitem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuitemExists(uint id)
        {
            return _context.Menuitem.Any(e => e.Id == id);
        }
    }
}
