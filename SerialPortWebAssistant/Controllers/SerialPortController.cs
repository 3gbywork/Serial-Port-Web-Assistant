using SerialPortWebAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SerialPortWebAssistant.Controllers
{
    public class SerialPortController : Controller
    {
        // GET: SerialPort
        public ActionResult Index()
        {
            ViewBag.Title = Resources.SerialPort.Serial_Port_Web_Assistant;

            var model = new SerialPortModel
            {
                SelectBaudRate = 19200,
                QuickCommandList = GetQuickCommandList(),
            };
            return View("SerialPort", model);
        }

        private IEnumerable<SelectListItem> GetQuickCommandList()
        {
            var commands = new List<SelectListItem>();

            const string filename = "~/App_Data/CEVT串口命令.txt";
            var path = Server.MapPath(filename);
            if (System.IO.File.Exists(path))
            {
                var lines = System.IO.File.ReadAllLines(path, Encoding.GetEncoding("gb2312"));
                commands = lines.Select(line =>
                {
                    var strs = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strs.Length == 2)
                        return new SelectListItem { Text = strs[0], Value = strs[1] };
                    return null;
                }).Where(item => null != item).ToList();
            }

            return commands;
        }
    }
}