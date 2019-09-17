using System.Collections.Generic;

namespace System.Web.Mvc
{
    public class BaseController : Controller
    {
        public void MessageSuccess(string message, bool dismissable = true)
        {
            AddAlert(AlertStyles.Success, message, dismissable, "fas fa-check");
        }

        public void MessageInformation(string message, bool dismissable = true)
        {
            AddAlert(AlertStyles.Information, message, dismissable, "fas fa-info");
        }

        public void MessageWarning(string message, bool dismissable = true)
        {
            AddAlert(AlertStyles.Warning, message, dismissable, "fas fa-exclamation-circle");
        }

        public void MessageDanger(string message, bool dismissable = true)
        {
            AddAlert(AlertStyles.Danger, message, dismissable, "fas fa-exclamation-triangle");
        }

        private void AddAlert(string alertStyle, string message, bool dismissable, string iconClass)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable,
                IconClass = iconClass
            });

            TempData[Alert.TempDataKey] = alerts;
        }

    }
}