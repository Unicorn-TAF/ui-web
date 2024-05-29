using OpenQA.Selenium;
using System;
using System.IO;
using System.Reflection;
using Unicorn.UI.Web.PageObject;

namespace Unicorn.UnitTests.UI.Tests
{
    public class WebTestsBase
    {
        protected static T NavigateToPage<T>(bool forceNavigation) where T : WebPage
        {
            IWebDriver driver = DriverManager.Instance.SeleniumDriver;
            T page = (T)Activator.CreateInstance(typeof(T), new object[] { DriverManager.Instance });

            string fullUrl = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "TestPages",
                page.Url);

            if (forceNavigation || driver.Url != fullUrl)
            {
                driver.Url = fullUrl;
            }

            return page;
        }

        protected static T NavigateToPage<T>() where T : WebPage =>
            NavigateToPage<T>(false);

        public void Refresh() => 
            DriverManager.Instance.SeleniumDriver.Navigate().Refresh();
    }
}
