using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using UdemyMVC.Models;
using UdemyMVC.Repositories;

namespace UdemyMVC.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserRepository context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserProfileController(IUserRepository context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Profile(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required");
            }

            var user = await context.GetByEmailWithCoursesAsync(email);

            if (user == null)
            {
                Console.WriteLine($"User with email {email} not found.");
                return NotFound("User not found");
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(User updatedUser, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                var user = await context.GetByEmailAsync(updatedUser.Email);

                if (user == null)
                {
                    return NotFound("User not found.");
                }
                
                // Update user details
                user.FullName = updatedUser.FullName;
                user.Address = updatedUser.Address;
                user.Email = updatedUser.Email;
                if (photo != null)
                {
                    var photoPath = GetPhotoPath(photo);
                    if (!string.IsNullOrEmpty(photoPath))
                    {
                        updatedUser.Image = photoPath;
                    }
                }
                user.Image = updatedUser.Image;
                await context.UpdateAsync(user);
                var userWithCourses = await context.GetByEmailWithCoursesAsync(updatedUser.Email);

                if (userWithCourses == null)
                {
                    return NotFound("User profile could not be reloaded.");
                }

                if (userWithCourses.Enrolement == null || !userWithCourses.Enrolement.Any())
                {
                    Console.WriteLine("No enrollments found for the user.");
                }
                else
                {
                    Console.WriteLine($"Loaded {userWithCourses.Enrolement.Count} enrollments.");
                }

                return View("Profile", userWithCourses);
            }
            var userWithCoursesInvalidState = await context.GetByEmailWithCoursesAsync(updatedUser.Email);

            if (userWithCoursesInvalidState == null)
            {
                return NotFound("User profile could not be reloaded after validation failure.");
            }

            return View("Profile", userWithCoursesInvalidState);
        }
        private string? GetPhotoPath(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var uniquePath = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var photoPath = Path.Combine(uploadPath, uniquePath);
                using (var filestream = new FileStream(photoPath, FileMode.Create))
                {
                    imageFile.CopyTo(filestream);
                    filestream.Close();
                }
                return $"/uploads/{uniquePath}";
            }
            return null;
        }

    }
}
