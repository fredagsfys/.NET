using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

using Raven.Client;
using Easyfy.CHS.Data.Raven;

namespace Easyfy.CHS.Infrastructure
{
    /// <summary>
    /// Controller extension class that adds controller methods
    /// to render a partial view and return the result as string.
    /// 
    /// Based on http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
    /// </summary>
    public static class ControllerExtension
    {

        /// <summary>
        /// Renders a (partial) view to string.
        /// </summary>
        /// <param name="controller">Controller to extend</param>
        /// <param name="viewName">(Partial) view to render</param>
        /// <returns>Rendered (partial) view as string</returns>
        public static string RenderPartialViewToString(this Controller controller, string viewName)
        {
            return controller.RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        /// Renders a (partial) view to string.
        /// </summary>
        /// <param name="controller">Controller to extend</param>
        /// <param name="viewName">(Partial) view to render</param>
        /// <param name="model">Model</param>
        /// <returns>Rendered (partial) view as string</returns>
        public static string RenderPartialViewToString(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData,
                                                  controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Renders a view to string.
        /// </summary>
        /// <param name="controller">Controller to extend</param>
        /// <param name="viewName">view to render</param>
        /// <returns>Rendered view as string</returns>
        public static string RenderViewToString(this Controller controller, string viewName)
        {
            return controller.RenderViewToString(viewName, null, null);
        }

        /// <summary>
        /// Renders a view to sting
        /// </summary>
        /// <param name="controller">Controller to extend</param>
        /// <param name="viewName">view to render</param>
        /// <param name="masterName">masterLayout to render</param>
        /// <returns></returns>
        public static string RenderViewToString(this Controller controller, string viewName, string masterName)
        {
            return controller.RenderViewToString(viewName, masterName, null);
        }

        /// <summary>
        /// Renders a view to string.
        /// </summary>
        /// <param name="controller">Controller to extend</param>
        /// <param name="viewName">view to render</param>
        /// <param name="masterName">masterLayout to render</param>
        /// <param name="model">Model</param>
        /// <returns>Rendered view as string</returns>
        public static string RenderViewToString(this Controller controller, string viewName, string masterName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, masterName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData,
                                                  controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

    }
}