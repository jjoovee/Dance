using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
  [Authorize]
  public class DancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dances
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string searchString,
    int? page)
        {
      ViewData["CurrentSort"] = sortOrder;
      ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
      ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
     
      if (searchString != null)
      {
        page = 1;
      }
      else
      {
        searchString = currentFilter;
      }

      ViewData["CurrentFilter"] = searchString;
      var students = from s in _context.Dances
                     select s;
      if (!String.IsNullOrEmpty(searchString))
      {
        students = students.Where(s => s.Title.Contains(searchString));
      }
      switch (sortOrder)
      {
        case "name_desc":
          students = students.OrderByDescending(s => s.Title);
          break;

        default:
          students = students.OrderBy(s => s.Title);
          break;
      }
      int pageSize = 3;
      return View(await PaginatedList<Dance>.CreateAsync(students.AsNoTracking(), page ?? 1, pageSize));
    }

    // GET: Dances/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dance = await _context.Dances
                .SingleOrDefaultAsync(m => m.DanceId == id);
            if (dance == null)
            {
                return NotFound();
            }

            return View(dance);
        }

    // GET: Dances/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
        {
            return View();
        }

        // POST: Dances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DanceId,Title,Price")] Dance dance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dance);
        }

    // GET: Dances/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dance = await _context.Dances.SingleOrDefaultAsync(m => m.DanceId == id);
            if (dance == null)
            {
                return NotFound();
            }
            return View(dance);
        }

        // POST: Dances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DanceId,Title,Price")] Dance dance)
        {
            if (id != dance.DanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanceExists(dance.DanceId))
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
            return View(dance);
        }

    // GET: Dances/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dance = await _context.Dances
                .SingleOrDefaultAsync(m => m.DanceId == id);
            if (dance == null)
            {
                return NotFound();
            }

            return View(dance);
        }

        // POST: Dances/Delete/5
        [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dance = await _context.Dances.SingleOrDefaultAsync(m => m.DanceId == id);
            _context.Dances.Remove(dance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanceExists(int id)
        {
            return _context.Dances.Any(e => e.DanceId == id);
        }
    }
}
