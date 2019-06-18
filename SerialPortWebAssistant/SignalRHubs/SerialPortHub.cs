using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SerialPortWebAssistant.Managers;
using SerialPortWebAssistant.Models;

namespace SerialPortWebAssistant.SignalRHubs
{
    [HubName("serialPortHub")]
    public class SerialPortHub : Hub
    {
        public SerialPortHub() : this(SerialPortManager.Instance) { }

        public SerialPortHub(SerialPortManager manager)
        {
            SerialPort = manager;
        }

        private SerialPortManager SerialPort { get; set; }

        public bool Open(SerialPortModel model)
        {
            return SerialPort.Open(model);
        }

        public bool Close(SerialPortModel model)
        {
            return SerialPort.Close(model);
        }

        public void SendMessage(SerialPortModel model)
        {
            SerialPort.SendMessage(model);
        }
    }
}