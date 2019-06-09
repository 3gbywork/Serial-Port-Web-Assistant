using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerialPortWebAssistant.SignalRHubs
{
    [HubName("serialPortHub")]
    public class SerialPortHub : Hub
    {

    }
}