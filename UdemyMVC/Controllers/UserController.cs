using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyMVC.Models;
using UdemyMVC.Repositories;

namespace UdemyMVC.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly IUserRepository context;

		public UserController(IUserRepository context)
        {
			this.context = context;
		}
        public async Task<IActionResult> GetUser()
		{
			IEnumerable<User>? users =  await context.GetAllAsync();
			return View("getUser",users);
		}
	}
}
