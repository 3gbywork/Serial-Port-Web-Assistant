using System.Globalization;
using System.Resources;
using System.Threading;

namespace SerialPortWebAssistant.Extensions
{
    public static class ResourceManagerExtension
    {
        public static bool TryGetString(this ResourceManager resourceManager, string key, CultureInfo culture, string defaultValue, out string value)
        {
            value = defaultValue;
            try
            {
                value = resourceManager.GetString(key, culture);
            }
            catch
            {
                //ignore exception and return the defaultValue
                return false;
            }

            return true;
        }

        public static bool TryGetString(this ResourceManager resourceManager, string key, out string value)
        {
            return resourceManager.TryGetString(key, Thread.CurrentThread.CurrentUICulture, key, out value);
        }
    }
}