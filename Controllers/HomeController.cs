using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSBelt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSBelt.Controllers
{
    public class HomeController : Controller
    {
        private MyContext context;
        public HomeController(MyContext mc)
        {
            context = mc;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                User userInDb = context.Users.FirstOrDefault(u => u.Email == user.Email);
                if(userInDb != null)
                {
                    ModelState.AddModelError("Email", "Email is taken, sorry, try again, thank you so much");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();user.Password = Hasher.HashPassword(user, user.Password);
                    context.Users.Add(user);
                    context.SaveChanges();
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    
                    return RedirectToAction("Home");
                }
            }

            return View("Index");
        } 

        [HttpPost("login")]
        public IActionResult Login(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                User userInDb = context.Users.FirstOrDefault(u => u.Email == user.LoginEmail);
                if(userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "That's an invalid email/password, sorry, try again, thank you so much");
                }
                else
                {
                    PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
                    var result = Hasher.VerifyHashedPassword(user, userInDb.Password, user.LoginPassword);
                    if(result == 0)
                    {
                        ModelState.AddModelError("LoginEmail", "That's an invalid email/password, sorry, try again, thank you so much");
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("UserId", userInDb.UserId);

                        return Redirect("Home");
                    }
                }
            }
  
            return View("Index");
        }

        [HttpGet("home")]
        public IActionResult Home()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            User userInDb = context.Users.FirstOrDefault(u => u.UserId == userid);
            if (userid == null)
            {
                return Redirect("/");
            }
            ViewBag.Plans = context.Plans.Include(p => p.Participants).OrderBy(d => d.Date).ToList();
            ViewBag.User = userInDb;

            return View("Home");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return Redirect("/");
        }

        [HttpGet("newplan")]
        public IActionResult NewPlan()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if(userid == null)
            {
                return Redirect("/");
            }

            return View("NewPlan");
        }

        [HttpPost("addplan")]
        public IActionResult AddPlan(Plan plan)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            User userInDb = context.Users.FirstOrDefault(u => u.UserId == userid);
            if(plan.Date < DateTime.Now)
            {
                ModelState.AddModelError("Date", "This date and time already happened, try again, thank you so much");
            }
            if(ModelState.IsValid)
            {
                string Name = $"{userInDb.FirstName} {userInDb.LastName}";
                plan.PlannerName = Name;
                plan.PlannerId = (int) userid;
                context.Create(plan);

                return Redirect("Home");
            }

            return View("NewPlan");
        }

        [HttpGet("details/{PlanId}")]
        public IActionResult PlanDetails(int PlanId)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            User userInDb = context.Users.FirstOrDefault(u => u.UserId == userid);
            if(userid == null)
            {
                return Redirect("/");
            }
            ViewBag.Participants = context.Plans.Include(u => u.Participants).ThenInclude(p => p.Planner).FirstOrDefault(pl => pl.PlanId == PlanId);
            ViewBag.Plan = context.Plans.FirstOrDefault(pln => pln.PlanId == PlanId);
            ViewBag.User = userInDb;

            // ViewBag.Plans = context.Plans.Include(p => p.Participants).OrderBy(d => d.Date).ToList();

            return View("PlanDetails");
        }

        [HttpGet("delete/{PlanId}")]
        public IActionResult Delete(int PlanId)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            User userInDb = context.Users.FirstOrDefault(u => u.UserId == userid);
            if(userid == null)
            {
                return Redirect("/");
            }
            context.Delete(PlanId);

            return RedirectToAction("Home");
        }

        [HttpGet("join/{PlanId}/{UserId}")]
        public IActionResult Join(int PlanId)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            User userInDb = context.Users.FirstOrDefault(u => u.UserId == userid);
            if(userid == null)
            {
                return Redirect("/");
            }
            Association a = new Association();
            a.PlanId = PlanId;
            a.UserId = (int) userid;
            context.Join(a);

            return RedirectToAction("Home");
        }

        [HttpGet("leave/{PlanId}/{UserId}")]
        public IActionResult Leave(int PlanId)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            User userInDb = context.Users.FirstOrDefault(u => u.UserId == userid);
            if(userid == null)
            {
                return Redirect("/");
            }
            Association NotGoing = context.Associations.Where(w => w.PlanId == PlanId).FirstOrDefault(u => u.UserId == userid);
            context.Leave(NotGoing);

            return RedirectToAction("Home");
        }
    }
}
