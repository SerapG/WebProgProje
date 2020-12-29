using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Models;

namespace Recipes.Controllers
{
    public class WorldCuisinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorldCuisinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorldCuisines
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorldCuisines.ToListAsync());
        }

        // GET: WorldCuisines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldCuisines = await _context.WorldCuisines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worldCuisines == null)
            {
                return NotFound();
            }

            return View(worldCuisines);
        }

        // GET: WorldCuisines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorldCuisines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] WorldCuisines worldCuisines)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worldCuisines);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(worldCuisines);
        }

        // GET: WorldCuisines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldCuisines = await _context.WorldCuisines.FindAsync(id);
            if (worldCuisines == null)
            {
                return NotFound();
            }
            return View(worldCuisines);
        }

        // POST: WorldCuisines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] WorldCuisines worldCuisines)
        {
            if (id != worldCuisines.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worldCuisines);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorldCuisinesExists(worldCuisines.Id))
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
            return View(worldCuisines);
        }

        // GET: WorldCuisines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldCuisines = await _context.WorldCuisines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worldCuisines == null)
            {
                return NotFound();
            }

            return View(worldCuisines);
        }

        // POST: WorldCuisines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worldCuisines = await _context.WorldCuisines.FindAsync(id);
            _context.WorldCuisines.Remove(worldCuisines);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorldCuisinesExists(int id)
        {
            return _context.WorldCuisines.Any(e => e.Id == id);
        }
    }
}
