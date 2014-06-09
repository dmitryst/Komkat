using HEP.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace HEP
{
    public class LocalizedRazor : RazorViewEngine
    {
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            partialPath = GlobalizeViewPath(controllerContext, partialPath);
            IEnumerable<string> fileExtensions = base.FileExtensions;
			IViewPageActivator viewPageActivator = base.ViewPageActivator; // ?

            return new RazorView(controllerContext, partialPath, null, false, fileExtensions);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            viewPath = GlobalizeViewPath(controllerContext, viewPath);
            return base.CreateView(controllerContext, viewPath, masterPath);
        }

        //public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        //{
        //    return base.FindView(controllerContext, viewName, masterName, useCache);
        //}

        private static string GlobalizeViewPath(ControllerContext controllerContext, string viewPath)
        {
            var request = controllerContext.HttpContext.Request;
            var culture = CultureHelper.GetImplementedCulture(request.UserLanguages != null && request.UserLanguages.Length > 0 ? request.UserLanguages[0] : null);
            if (culture != null &&
                !string.IsNullOrEmpty(culture) &&
                !string.Equals(culture, "en", StringComparison.InvariantCultureIgnoreCase))
            {
                string localizedViewPath = Regex.Replace(
                    viewPath,
                    "^~/Views/",
                    string.Format("~/Views/Globalization/{0}/",
                    culture.ToString()
                    ));
                if (File.Exists(request.MapPath(localizedViewPath)))
                { viewPath = localizedViewPath; }
            }
            return viewPath;
        }

    }
}