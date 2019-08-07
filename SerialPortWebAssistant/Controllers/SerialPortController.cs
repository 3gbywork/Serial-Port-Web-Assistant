using SerialPortWebAssistant.Attributes;
using SerialPortWebAssistant.Filters;
using SerialPortWebAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SerialPortWebAssistant.Controllers
{
    [Localization]
    public class SerialPortController : Controller
    {
        //static private Dictionary<string, SerialPort> serials = new Dictionary<string, SerialPort>();
        //static private IHubContext/*<ChatHub>*/ context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

        // GET: SerialPort
        public ActionResult SerialPort()
        {
            ViewBag.Title = Resources.SerialPort.Serial_Port_Web_Assistant;

            var model = new SerialPortModel
            {
                SelectBaudRate = 19200,
                QuickCommandList = GetQuickCommandList(),
            };
            return View(model);
        }

        private IEnumerable<SelectListItem> GetQuickCommandList()
        {
            var commands = new List<SelectListItem>();

            const string filename = "~/App_Data/Commands.txt";
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

        [WithoutLocalization]//这个函数不走Localization过滤器
        public ActionResult ChangeLanguage(string lang, string url)
        {
            if (!url.EndsWith("/"))
            {
                url += "/";
            }

            string[] langs = new string[] { "zh-CN", "en-US" };
            //use lang replace old lang,include input judgment
            if (!string.IsNullOrEmpty(url) &&
                url.Length > 3 &&
                url.StartsWith("/") &&
                url.IndexOf("/", 1) > 0 &&
                langs.Contains(lang) &&
                langs.Contains(url.Substring(1, url.IndexOf("/", 1) - 1)))
            {
                url = $"/{lang}{url.Substring(url.IndexOf("/", 1))}";
                return Redirect(url);//redirect to new url
            }

            return SerialPort();
        }

        //[HttpPost]
        ////[ActionName("open")]
        //[MultiButton(Name = "openAction")]
        //public ActionResult OpenSerialPort(SerialPortModel model)
        //{
        //    if (!serials.TryGetValue(model.SelectSerialPort, out SerialPort serialPort))
        //    {
        //        serialPort = CreateSerialPort(model);
        //        serials[model.SelectSerialPort] = serialPort;
        //    }
        //    model.IsOpened = serialPort.TryOpen();
        //    return View("index", model);
        //}

        //[HttpPost]
        ////[ActionName("close")]
        //[MultiButton(Name = "closeAction")]
        //public ActionResult CloseSerialPort(SerialPortModel model)
        //{
        //    if (serials.TryGetValue(model.SelectSerialPort, out SerialPort serialPort))
        //    {
        //        model.IsOpened = serialPort.TryClose();
        //    }
        //    return View("index", model);
        //}

        //[HttpPost]
        ////[ActionName("send")]
        //[MultiButton(Name = "sendAction")]
        //public ActionResult SendMessage(SerialPortModel model)
        //{
        //    if (!string.IsNullOrEmpty(model.Send))
        //    {
        //        if (!serials.TryGetValue(model.SelectSerialPort, out SerialPort serialPort))
        //        {
        //            serialPort = CreateSerialPort(model);
        //            serials[model.SelectSerialPort] = serialPort;
        //        }
        //        model.IsOpened = serialPort.TryOpen();

        //        if (model.IsOpened)
        //        {
        //            var msg = Encoding.Default.GetBytes(model.Send);
        //            serialPort.Write(msg, 0, msg.Length);
        //        }
        //    }
        //    return View("index", model);
        //}

        //private SerialPort CreateSerialPort(SerialPortModel model)
        //{
        //    var serialPort = new SerialPort
        //    {
        //        PortName = model.SelectSerialPort,
        //        BaudRate = model.SelectBaudRate,
        //        DataBits = model.SelectDataBits,
        //        Parity = model.GetParity(),
        //        StopBits = model.GetStopBits()
        //    };

        //    serialPort.DataReceived += SerialPort_DataReceived;

        //    return serialPort;
        //}

        //private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    if (sender is SerialPort serialPort)
        //    {
        //        var buffer = new byte[serialPort.BytesToRead];
        //        serialPort.Read(buffer, 0, buffer.Length);
        //        var msg = Encoding.Default.GetString(buffer);
        //        context.Clients.All.receivedMessage(serialPort.PortName, msg);
        //    }
        //}
    }
}