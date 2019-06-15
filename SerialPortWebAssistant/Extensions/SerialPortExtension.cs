using System;
using System.IO.Ports;

namespace SerialPortWebAssistant.Extensions
{
    static public class SerialPortExtension
    {
        public static bool TryOpen(this SerialPort serialPort)
        {
            if (null == serialPort)
                return false;
            if (serialPort.IsOpen)
                return true;

            try
            {
                serialPort.Open();
            }
            catch (Exception)
            {
            }

            return serialPort.IsOpen;
        }

        public static bool TryClose(this SerialPort serialPort)
        {
            if (null == serialPort)
                return false;
            if (!serialPort.IsOpen)
                return true;

            try
            {
                serialPort.DiscardOutBuffer();
                serialPort.DiscardInBuffer();
                serialPort.Close();
            }
            catch (Exception)
            {
            }

            return serialPort.IsOpen;
        }
    }
}