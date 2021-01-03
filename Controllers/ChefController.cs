using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Models;


namespace Recipes.Controllers
{
    [Authorize]
    public class ChefController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ChefController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

       

        // GET: Chef
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Chef.Include(c => c.Gender).Include(c => c.WorldCuisines);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> FamousChefs()
        {
            var famousChefs = await _context.Chef.ToListAsync();
            return View(famousChefs);
        }

        public async Task<IActionResult> Info(int id)
        {
            var db = from chef in _context.Chef where chef.Id == id select chef;
            return View(await db.Include(c => c.WorldCuisines).FirstOrDefaultAsync());
        }

        // GET: Chef/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chef
                .Include(c => c.Gender)
                .Include(c => c.WorldCuisines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        // GET: Chef/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "GenderName");
            ViewData["WorldCuisinesId"] = new SelectList(_context.WorldCuisines, "Id", "Name");
            return View();
        }

        // POST: Chef/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Birth,Image,Information,GenderId,WorldCuisinesId")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;


                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\chef");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                chef.Image = @"\images\chef\" + fileName + extension;
                _context.Add(chef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "GenderName", chef.GenderId);
            ViewData["WorldCuisinesId"] = new SelectList(_context.WorldCuisines, "Id", "Name", chef.WorldCuisinesId);
            return View(chef);
        }

        // GET: Chef/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chef.FindAsync(id);
            if (chef == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "GenderName", chef.GenderId);
            ViewData["WorldCuisinesId"] = new SelectList(_context.WorldCuisines, "Id", "Name", chef.WorldCuisinesId);
            return View(chef);
        }

        // POST: Chef/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Birth,Image,Information,GenderId,WorldCuisinesId")] Chef chef)
        {
            if (id != chef.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chef);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChefExists(chef.Id))
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
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "GenderName", chef.GenderId);
            ViewData["WorldCuisinesId"] = new SelectList(_context.WorldCuisines, "Id", "Name", chef.WorldCuisinesId);
            return View(chef);
        }

        // GET: Chef/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chef
                .Include(c => c.Gender)
                .Include(c => c.WorldCuisines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        // POST: Chef/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chef = await _context.Chef.FindAsync(id);
            _context.Chef.Remove(chef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChefExists(int id)
        {
            return _context.Chef.Any(e => e.Id == id);
        }
    }
}
