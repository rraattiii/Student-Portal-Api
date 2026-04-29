using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_portal.Web.Data;
using Student_portal.Web.Models.Entities;

namespace Student_portal.Controllers;

public class StudentsController : Controller
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _context.Students
            .OrderBy(student => student.Name)
            .ToListAsync();

        return View(students);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var student = await _context.Students.FirstOrDefaultAsync(student => student.Id == id);
        if (student is null)
        {
            return NotFound();
        }

        return View(student);
    }

    public IActionResult Create()
    {
        return View(new Student());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid)
        {
            return View(student);
        }

        student.Id = Guid.NewGuid();
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Student created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student is null)
        {
            return NotFound();
        }

        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(student);
        }

        var existingStudent = await _context.Students.FindAsync(id);
        if (existingStudent is null)
        {
            return NotFound();
        }

        existingStudent.Name = student.Name;
        existingStudent.Email = student.Email;
        existingStudent.Phone = student.Phone;
        existingStudent.Subscription = student.Subscription;

        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Student updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var student = await _context.Students.FirstOrDefaultAsync(student => student.Id == id);
        if (student is null)
        {
            return NotFound();
        }

        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student is not null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "Student deleted successfully.";
        }

        return RedirectToAction(nameof(Index));
    }
}
