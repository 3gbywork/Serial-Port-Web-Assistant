using Resources;
using System.Web.Mvc;

namespace SerialPortWebAssistant.Extensions
{
    public static class HtmlHelperExtension
    {
        public static string LocalizationFor(this HtmlHelper htmlHelper, string key)
        {
            SerialPort.ResourceManager.TryGetString(key, out string value);
            return value;
        }
    }
}