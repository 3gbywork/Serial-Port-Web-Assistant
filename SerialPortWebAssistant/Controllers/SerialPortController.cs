using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerialPortWebAssistant.Controllers
{
    public class SerialPortController : Controller
    {
        // GET: SerialPort
        public ActionResult Index()
        {
            return View();
        }
    }
}