using OpenQA.Selenium;
using System;
using System.IO;
using System.Reflection;
using Unicorn.UI.Web.PageObject;

namespace Unicorn.UnitTests.UI
{
    public class WebTestsBase
    {
        protected static T NavigateToPage<T>(IWebDriver driver, bool forceNavigation) where T : WebPage
        {
            T page = (T)Activator.CreateInstance(typeof(T), new object[] { driver });

            string fullUrl = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName +
                "\\TestPages\\" + page.Url;

            if (forceNavigation || driver.Url != fullUrl)
            {
                driver.Url = fullUrl;
            }

            return page;
        }

        protected static T NavigateToPage<T>(IWebDriver driver) where T : WebPage =>
            NavigateToPage<T>(driver, false);
    }
}
