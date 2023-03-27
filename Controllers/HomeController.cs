using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MpdaTest.BdModels;
using MpdaTest.Models;
using MpdaTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MpdaTest.Controllers
{


    public class HomeController : Controller
    {
        string loginMain = null;

        Model1 BD = new Model1();
        CookieOptions cookieOptions = new CookieOptions();
        public HomeController()
        {
            
        }



        public string CoockiesChek()
        {
            if (Request.Cookies.ContainsKey("Login"))
            {
                var login = Request.Cookies["Login"];

                if (BD.UserSelectTest.Where(x => x.Login.ToLower() == login.ToLower()).Any())
                {
                    return login;
                }
                else
                {
                    Response.Cookies.Delete("Login");
                    return null;
                }
            }
            return null;

        }


        public IActionResult vhod()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                if (BD.UserSelectTest.Where(x => x.Login.ToLower() == model.Login.ToLower()).Any())
                {
                    var login = BD.UserSelectTest.Where(x => x.Login.ToLower() == model.Login.ToLower()).FirstOrDefault().Login;

                    var result = BD.UserSelectTest.Where(x => x.Login.ToLower() == login && x.Password == model.Password).Any();
                    if (result)
                    {
                        cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
                        Response.Cookies.Append("Login", login, cookieOptions);
                        loginMain = login;

                        return RedirectToAction("AllCourse");//Переход на нужную страницу

                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }


            }
            return View(model);

        }

        public IActionResult Exit()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Response.Cookies.Delete("Login");
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Index()
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
