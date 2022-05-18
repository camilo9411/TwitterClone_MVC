using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone_MVC.Data;
using TwitterClone_MVC.Models;

namespace TwitterClone_MVC.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly TwitterContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public User currentUser = new User();

        public HomeController(TwitterContext context, ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

        }


        public IActionResult Index()
        {
            if (Request.Cookies["Auth"] != null) {

                return RedirectToAction("Index", "Tweets");

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(IFormCollection fc)
        {
            string email = fc["Email"];

            if (fc is null)
            {
                throw new ArgumentNullException(nameof(fc));
            }

            var user = _context.Users.FirstOrDefault<User>(m => m.Email == email);


            //Inputted username does not exist on the database
            if (user == null)
            {
                ViewBag.message = "User and password do not match";
                return View();
            }

            //User exists on the database
            else
            {
                //Check if the password matches for the selected user
                if (user.Password.Equals(fc["password"].ToString().Trim()))
                {
                    //Authenticated
                    Response.Cookies.Append("Auth", user.Fullname);
                    Response.Cookies.Append("ID", user.ID.ToString());
                    currentUser = user;
                    return RedirectToAction("Index","Tweets");
                }
                else
                {
                    ViewBag.message = "User and password do not match";
                    return View();
                }

            }
        }

        public ActionResult Logout()
        {
            Response.Cookies.Delete("Auth");
            Response.Cookies.Delete("ID");

            return RedirectToAction("Index");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
