﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UdemyMVC.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
