using Microsoft.AspNetCore.Mvc;
using StudentMVC.Helpers;
using Students.Application.Common;
using Students.Application.DTO;
using Students.Application.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Students.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        // GET: Students
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int? pageNumber, string searchString)
        {
            var result = await _studentsService.GetAllWithDateRangeAsync(startDate, endDate, searchString);

            if (!result.Ok)
            {
                ViewBag.ErrorMessage = result.ExceptionMessage;
                return View("Error");
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;

            int pageSize = 3;
            return View(await PaginatedList<StudentDTO>.CreateAsync(result.Response, pageNumber ?? 1, pageSize));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _studentsService.GetByIDAsync(id.Value);

            if (!result.Ok)
            {
                ViewBag.ErrorMessage = result.ExceptionMessage;
                return View("Error");
            }

            return View(result.Response);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FirstName,LastName,PersonalNumber,DateOfBirth,Gender")] StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _studentsService.AddAsync(studentDTO);
                if (!result.Ok)
                {
                    ViewBag.ErrorMessage = result.ExceptionMessage;
                    return View("Error");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(studentDTO);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _studentsService.GetByIDAsync(id.Value);
            if (!result.Ok)
            {
                ViewBag.ErrorMessage = result.ExceptionMessage;
                return View("Error");
            }

            return View(result.Response);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,FirstName,LastName,PersonalNumber,DateOfBirth,Gender")] StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                studentDTO.StudentID = id;
                var result = await _studentsService.UpdateAsync(studentDTO);

                if (!result.Ok)
                {
                    ViewBag.ErrorMessage = result.ExceptionMessage;
                    return View("Error");
                }

                return RedirectToAction(nameof(Index));


            }

            return View(studentDTO);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _studentsService.GetByIDAsync(id.Value);

            if (!result.Ok)
            {
                ViewBag.ErrorMessage = result.ExceptionMessage;
                return View("Error");
            }

            return View(result.Response);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _studentsService.DeleteAsync(id);

            if (!result.Ok)
            {
                ViewBag.ErrorMessage = result.ExceptionMessage;
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
