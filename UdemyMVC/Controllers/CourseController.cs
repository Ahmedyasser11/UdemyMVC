using Microsoft.AspNetCore.Mvc;
using UdemyMVC.Models;
using UdemyMVC.Repositories;
using UdemyMVC.ViewModels;

namespace UdemyMVC.Controllers
{
    public class CourseController : Controller
    {
        ICourseRepository courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        //async methoud should be awaited =>  use async Task 

        public async Task<IActionResult> CourseDetailes(int id)
        {
            Course course = await courseRepository.GetCoursesByCourseIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }
            decimal sumRate = course.CourseRates.Select(c => c.Rate).Sum();
            int countRate = course.CourseRates.Select(c => c.Rate).Count();
            ViewBag.title = "Test Test Test";
            CourseDetailsVM courseVM = new CourseDetailsVM();
            courseVM.Id = course.ID;
            courseVM.CourseName = course.CourseName;
            courseVM.Title = course.Title ?? ViewBag.title;
            courseVM.Description = course.Description;
            courseVM.CourseImg = course.CourseImage;
            courseVM.Price = course.Price;
            courseVM.chapters = course.Chapters.Where(c => c.CourseId == id).ToList();
            courseVM.Rating = (sumRate / countRate).ToString() ?? "1";
            courseVM.NumberOfStudent = course.Enrollment.Count();
            courseVM.Instructor = course.Instructor.User.FullName;
            courseVM.Category = course.CategoryCourses.Select(c => c.Category.CategoryName).ToList();

            return View("CourseDetailes", courseVM);
        }
    }
}
