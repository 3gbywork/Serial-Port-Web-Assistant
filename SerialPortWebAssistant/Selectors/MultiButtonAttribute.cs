using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace SerialPortWebAssistant.Selectors
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public string Argument { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            var key = ButtonKeyFrom(controllerContext);
            var keyIsValid = IsValid(key);

            if (keyIsValid)
            {
                UpdateValueProviderIn(controllerContext, ValueFrom(key));
            }

            return keyIsValid;
        }

        private string ButtonKeyFrom(ControllerContext controllerContext)
        {
            var keys = controllerContext.HttpContext.Request.Params.AllKeys;
            return keys.FirstOrDefault(KeyStartsWithButtonName);
        }

        private static bool IsValid(string key)
        {
            return key != null;
        }

        private static string ValueFrom(string key)
        {
            var parts = key.Split(":".ToCharArray());
            return parts.Length < 2 ? null : parts[1];
        }

        private void UpdateValueProviderIn(ControllerContext controllerContext, string value)
        {
            if (string.IsNullOrEmpty(Argument)) return;
            //controllerContext.Controller.ValueProvider[Argument] = new ValueProviderResult(value, value, null);
            controllerContext.RouteData.Values[Argument] = value;
        }

        private bool KeyStartsWithButtonName(string key)
        {
            return key.StartsWith(Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}