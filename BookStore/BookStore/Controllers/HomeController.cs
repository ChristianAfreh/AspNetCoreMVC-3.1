using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        public async Task<ViewResult> Index()
        {
            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();
            return View();
        }
        public ViewResult AboutUs()
        {
            return View();
        }
        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
