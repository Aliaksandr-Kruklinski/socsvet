using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcUI.HtmlHelpers
{
    public static class LayoutHelpers
    {
        #region LogButtons

        public static MvcHtmlString LogButtons(this HtmlHelper html, bool isAuthenticated, bool isAdmin, string actionName)
        {
            StringBuilder result = new StringBuilder();
            if (isAuthenticated)
            {
                AuthenticatedButtons(html, isAdmin, ref result);
            }
            else
            {
                NoAuthenticatedButtons(html, actionName, ref result);
            }
            return MvcHtmlString.Create(result.ToString());
        }

        private static void AuthenticatedButtons(HtmlHelper html, bool isAdmin, ref StringBuilder result)
        {
            RolesButtons(html, isAdmin, result);
            result.Append(html.RouteLink("Результаты | ", new { controller = "Result", action = "Index" }, new { @id = "Button" }));
            result.Append(html.RouteLink("Выйти", new { controller = "Account", action = "LogOut" }, new { @id = "Button" }));
        }

        private static void NoAuthenticatedButtons(HtmlHelper html, string actionName, ref StringBuilder result)
        {
            if (actionName != "SignUp")
            {
                result.Append(html.RouteLink("Зарегистрироваться", new { controller = "Account", action = "SignUp" }, new { @id = "Button" }));
            }
            else
            {
                result.Append(html.RouteLink("Войти", new { controller = "Account", action = "LogIn" }, new { @id = "Button" }));
            }
        }

        private static void RolesButtons(HtmlHelper html, bool isAdmin, StringBuilder result)
        {
            if (isAdmin)
            {
                result.Append(html.RouteLink("Тесты | ", new { controller = "Admin", action = "Index" }, new { @id = "Button" }));
            }
            else
            {
               // result.Append(html.RouteLink("Настройки | ", new { controller = "Options", action = "Index" }, new { @id = "Button" }));
            }
        }

        #endregion
    }
}