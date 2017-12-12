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

namespace WebApplication1.Controllers
{
  [Authorize]
  public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
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
      var students = from s in _context.Teachers
                     select s;

      if (!String.IsNullOrEmpty(searchString))
      {
        students = students.Where(s => s.Surname.Contains(searchString)
                               || s.Name.Contains(searchString));
      }
      switch (sortOrder)
      {
        case "name_desc":
          students = students.OrderByDescending(s => s.Surname);
          break;

        default:
          students = students.OrderBy(s => s.Surname);
          break;
      }
      int pageSize = 3;
      return View(await PaginatedList<Teacher>.CreateAsync(students.AsNoTracking(), page ?? 1, pageSize));
    }

    // GET: Teachers/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

      var teacher = await _context.Teachers
  .Include(s => s.Enrollments)
      .ThenInclude(e => e.Client)

  .AsNoTracking()
  .SingleOrDefaultAsync(m => m.Id == id);

      if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

    // GET: Teachers/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("Id,Name,Surname,Phone")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

    // GET: Teachers/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Phone")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            return View(teacher);
        }

    // GET: Teachers/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
