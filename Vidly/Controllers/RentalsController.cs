using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        public ViewResult New()
        {
            return View();
        }
    }
}