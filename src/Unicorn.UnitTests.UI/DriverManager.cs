using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Unicorn.UI.Web.Driver;

namespace Unicorn.UnitTests.UI
{
    internal static class DriverManager
    {
        public static DesktopWebDriver Instance { get; } = GetDriverInstance();

        private static DesktopWebDriver GetDriverInstance()
        {
            IWebDriver driver = new ChromeDriver(GetChromeOptions());

            return new DesktopWebDriver(driver);
        }

        private static ChromeOptions GetChromeOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments(
                "allow-insecure-localhost",
                "ignore-certificate-errors",
                "disable-extensions",
                "disable-infobars",
#if DEBUG
#else
                "headless",
#endif
                "--window-size=1920x1080");

            return options;
        }
    }
}
