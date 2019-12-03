using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EntityFrameworkWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkWeb.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _db;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationContext db, ILogger<HomeController> logger)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            User user1 = new User { Login = "Login1", Password = "pass1" };
            User user2 = new User { Login = "Login2", Password = "pass2" };
            User user3 = new User { Login = "Login3", Password = "pass3" };
            _db.Users.AddRange(user1, user2, user3);
            _db.SaveChanges();

            UserProfile usProf1 = new UserProfile { Name = "Greg", Age = 25, UserId = user1.Id };
            UserProfile usProf2 = new UserProfile { Name = "Mary", Age = 27, UserId = user2.Id };
            UserProfile usProf3 = new UserProfile { Name = "Ury", Age = 30, UserId = user3.Id };
            UserProfile usProf4 = new UserProfile { Name = "Ivan", Age = 28, UserId = user3.Id };
            _db.UsersProfile.AddRange(usProf1, usProf2, usProf3, usProf4);
            _db.SaveChanges();

            List<string> result = new List<string>();

            foreach (User u in _db.Users.Include(up => up.Profile).ToList())
            {
                //Console.WriteLine("Name: {0} Age: {1} Login: {2} Password: {3}", u?.Profile.Name, u?.Profile.Age, u.Login.Length, u.Password);
                result.Add("Name: " + u?.Profile.Name + "; Age: " + u?.Profile.Age + "; Login: " + u.Login.Length + "; Password: " + u.Password);
            }

            //Console.ReadLine();

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
