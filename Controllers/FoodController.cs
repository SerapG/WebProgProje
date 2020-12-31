using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Models;

namespace Recipes.Controllers
{
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;
<<<<<<< HEAD
        private readonly IWebHostEnvironment _hostingEnvironment;
=======
       
>>>>>>> 94aeb9dc47cc6814ee5baf745b7d48968de2a8f6

        public FoodController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
<<<<<<< HEAD
            _hostingEnvironment = hostingEnvironment;
=======

>>>>>>> 94aeb9dc47cc6814ee5baf745b7d48968de2a8f6
        }

        // GET: Food
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Food.Include(f => f.Category).Include(f => f.Chef).Include(f => f.Language).Include(f => f.Region);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Food/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .Include(f => f.Category)
                .Include(f => f.Chef)
                .Include(f => f.Language)
                .Include(f => f.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Food/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Catergory, "Id", "CategoryName");
            ViewData["ChefId"] = new SelectList(_context.Chef, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "LanguageName");
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "RegionName");
            return View();
        }

        // POST: Food/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,Recipe,Banner,Score,CategoryId,LanguageId,RegionId,ChefId,CommentId")] Food food)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;


                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\food");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                food.Banner = @"\images\food\" + fileName + extension;
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Catergory, "Id", "CategoryName", food.CategoryId);
            ViewData["ChefId"] = new SelectList(_context.Chef, "Id", "Name", food.ChefId);
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "LanguageName", food.LanguageId);
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "RegionName", food.RegionId);
            return View(food);
        }

        // GET: Food/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Catergory, "Id", "Id", food.CategoryId);
            ViewData["ChefId"] = new SelectList(_context.Chef, "Id", "Id", food.ChefId);
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Id", food.LanguageId);
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "Id", food.RegionId);
            return View(food);
        }

        // POST: Food/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodName,Recipe,Banner,Score,CategoryId,LanguageId,RegionId,ChefId,CommentId")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Catergory, "Id", "Id", food.CategoryId);
            ViewData["ChefId"] = new SelectList(_context.Chef, "Id", "Id", food.ChefId);
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Id", food.LanguageId);
            ViewData["RegionId"] = new SelectList(_context.Region, "Id", "Id", food.RegionId);
            return View(food);
        }

        // GET: Food/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .Include(f => f.Category)
                .Include(f => f.Chef)
                .Include(f => f.Language)
                .Include(f => f.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Food.FindAsync(id);
            _context.Food.Remove(food);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Food.Any(e => e.Id == id);
        }
    }
}
