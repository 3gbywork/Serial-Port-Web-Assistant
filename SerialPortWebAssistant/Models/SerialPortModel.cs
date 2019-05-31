using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using System.Linq;
using System.Web.Mvc;

namespace SerialPortWebAssistant.Models
{
    public class SerialPortModel
    {
        public IEnumerable<SelectListItem> SerialPortList { get; set; }
        public IEnumerable<SelectListItem> BaudRateList { get; set; }
        public IEnumerable<SelectListItem> DataBitsList { get; set; }
        public IEnumerable<SelectListItem> ParityList { get; set; }
        public IEnumerable<SelectListItem> StopBitsList { get; set; }
        public IEnumerable<SelectListItem> QuickCommandList { get; set; }

        [Display(Name = "串口号")]
        public string SelectSerialPort { get; set; }
        [Display(Name = "波特率")]
        public int SelectBaudRate { get; set; }
        [Display(Name = "数据位")]
        public int SelectDataBits { get; set; }
        [Display(Name = "校验位")]
        public string SelectParity { get; set; }
        [Display(Name = "停止位")]
        public string SelectStopBits { get; set; }
        [Display(Name = "接收区")]
        public string Receive { get; set; }
        [Display(Name = "发送区")]
        public string Send { get; set; }

        public bool IsOpened { get; set; }

        public SerialPortModel()
        {
            SerialPortList = SerialPort.GetPortNames().Select(name => new SelectListItem { Text = name, Value = name });
            BaudRateList = new List<int> { 115200, 76800, 57600, 43000, 38400, 19200, 9600, 4800, 2400, 1200 }.Select(baud => new SelectListItem { Text = baud.ToString(), Value = baud.ToString() });
            DataBitsList = new List<int> { 8, 7, 6, 5 }.Select(bits => new SelectListItem { Text = bits.ToString(), Value = bits.ToString() });
            ParityList = new List<string> { "无", "奇校验", "偶校验" }.Select(parity => new SelectListItem { Text = parity, Value = parity });
            StopBitsList = new List<string> { "0", "1", "1.5", "2" }.Select(bits => new SelectListItem { Text = bits, Value = bits });
            QuickCommandList = new List<SelectListItem>();

            IsOpened = false;
            Receive = string.Empty;
            Send = string.Empty;
        }

        public Parity GetParity()
        {
            if ("奇校验" == SelectParity)
                return Parity.Odd;
            else if ("偶校验" == SelectParity)
                return Parity.Even;
            else
                return Parity.None;
        }

        public StopBits GetStopBits()
        {
            switch (SelectStopBits)
            {
                case "0":
                    return StopBits.None;
                case "1.5":
                    return StopBits.OnePointFive;
                case "2":
                    return StopBits.Two;
                default:
                    return StopBits.One;
            }
        }
    }
}