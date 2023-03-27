using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MpdaTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MpdaTest.Controllers
{
    public class HomeController : Controller
    {
        

        public HomeController()
        {
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
