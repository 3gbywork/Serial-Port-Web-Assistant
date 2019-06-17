using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SerialPortWebAssistant.Extensions;
using SerialPortWebAssistant.Models;
using SerialPortWebAssistant.SignalRHubs;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace SerialPortWebAssistant.Managers
{
    public class SerialPortManager
    {
        private readonly static Lazy<SerialPortManager> _instance = new Lazy<SerialPortManager>(() => new SerialPortManager(GlobalHost.ConnectionManager.GetHubContext<SerialPortHub>().Clients));
        private static readonly Dictionary<string, SerialPort> _serials = new Dictionary<string, SerialPort>();

        private SerialPortManager(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        internal bool Close(SerialPortModel model)
        {
            if (_serials.TryGetValue(model.SelectSerialPort, out SerialPort serialPort))
            {
                model.IsOpened = serialPort.TryClose();
                return serialPort.IsOpen;
            }
            return false;
        }

        internal bool IsOpen(string name)
        {
            if (_serials.TryGetValue(name, out SerialPort serialPort))
            {
                return serialPort.IsOpen;
            }
            return false;
        }

        internal void SendMessage(SerialPortModel model)
        {
            if (string.IsNullOrEmpty(model.Send))
                return;

            if (_serials.TryGetValue(model.SelectSerialPort, out SerialPort serialPort))
            {
                if (serialPort.IsOpen)
                {
                    var msg = Encoding.Default.GetBytes(model.Send);
                    serialPort.Write(msg, 0, msg.Length);
                }
            }
        }

        internal bool Open(SerialPortModel model)
        {
            if (string.IsNullOrEmpty(model.SelectSerialPort))
                return false;

            if (!_serials.TryGetValue(model.SelectSerialPort, out SerialPort serialPort))
            {
                serialPort = CreateSerialPort(model);
                _serials[model.SelectSerialPort] = serialPort;
            }
            model.IsOpened = serialPort.TryOpen();
            return serialPort.IsOpen;
        }

        private SerialPort CreateSerialPort(SerialPortModel model)
        {
            var serialPort = new SerialPort
            {
                PortName = model.SelectSerialPort,
                BaudRate = model.SelectBaudRate,
                DataBits = model.SelectDataBits,
                Parity = model.GetParity(),
                StopBits = model.GetStopBits()
            };

            serialPort.DataReceived += SerialPort_DataReceived;

            return serialPort;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sender is SerialPort serialPort)
            {
                var buffer = new byte[serialPort.BytesToRead];
                serialPort.Read(buffer, 0, buffer.Length);
                var msg = Encoding.Default.GetString(buffer);
                Clients.All.receivedMessage(serialPort.PortName, msg);
            }
        }

        public static SerialPortManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients { get; set; }
    }
}