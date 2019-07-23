using System;
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

        [Display(Name = "Select_Serial_Port", ResourceType = typeof(Resources.SerialPort))]
        public string SelectSerialPort { get; set; }
        [Display(Name = "Select_Baud_Rate", ResourceType = typeof(Resources.SerialPort))]
        public int SelectBaudRate { get; set; }
        [Display(Name = "Select_Data_Bits", ResourceType = typeof(Resources.SerialPort))]
        public int SelectDataBits { get; set; }
        [Display(Name = "Select_Parity", ResourceType = typeof(Resources.SerialPort))]
        public string SelectParity { get; set; }
        [Display(Name = "Select_Stop_Bits", ResourceType = typeof(Resources.SerialPort))]
        public string SelectStopBits { get; set; }
        [Display(Name = "Receive_Area", ResourceType = typeof(Resources.SerialPort))]
        public string Receive { get; set; }
        [Display(Name = "Send_Area", ResourceType = typeof(Resources.SerialPort))]
        public string Send { get; set; }

        public bool IsOpened { get; set; }

        public SerialPortModel()
        {
            SerialPortList = SerialPort.GetPortNames().Select(name => new SelectListItem { Text = name, Value = name });
            BaudRateList = new List<int> { 115200, 76800, 57600, 43000, 38400, 19200, 9600, 4800, 2400, 1200 }.Select(baud => new SelectListItem { Text = baud.ToString(), Value = baud.ToString() });
            DataBitsList = new List<int> { 8, 7, 6, 5 }.Select(bits => new SelectListItem { Text = bits.ToString(), Value = bits.ToString() });
            ParityList = new Dictionary<string, Parity>
            {
                { Resources.SerialPort.None_Parity, Parity.None },
                { Resources.SerialPort.Odd_Parity, Parity.Odd },
                { Resources.SerialPort.Even_Parity, Parity.Even }
            }.Select(parity => new SelectListItem { Text = parity.Key, Value = parity.Value.ToString() });
            StopBitsList = new Dictionary<string, StopBits>
            {
                { "1", StopBits.One },
                { "1.5", StopBits.OnePointFive },
                { "2", StopBits.Two }
            }.Select(bits => new SelectListItem { Text = bits.Key, Value = bits.Value.ToString() });
            QuickCommandList = new List<SelectListItem>();

            IsOpened = false;
            Receive = string.Empty;
            Send = string.Empty;
        }

        public Parity GetParity()
        {
            if (Enum.TryParse(SelectParity, out Parity parity))
                return parity;
            return Parity.None;
        }

        public StopBits GetStopBits()
        {
            if (Enum.TryParse(SelectStopBits, out StopBits stopBits))
                return stopBits;
            return StopBits.One;
        }
    }
}