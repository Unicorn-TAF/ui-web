using System;
using System.IO;
using Unicorn.Taf.Core.Logging;
using Unicorn.Taf.Core.Testing;
using Unicorn.UI.Web.Driver;

namespace Unicorn.UI.Web
{
    /// <summary>
    /// Provides ability to take web browser screenshots (works on both UI and headless modes).
    /// </summary>
    public class WebScreenshotTaker
    {
        private const string LogPrefix = nameof(WebScreenshotTaker);
        private const int MaxLength = 250;

        private readonly string _screenshotsDir;
        private readonly WebDriver _driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebScreenshotTaker"/> class with default directory.<para/>
        /// Default directory is ".\Screenshots" (created automatically if it does not exist).
        /// </summary>
        /// <param name="driver"><see cref="WebDriver"/> instance</param>
        public WebScreenshotTaker(WebDriver driver) : this(
            driver,
            Path.Combine(Path.GetDirectoryName(new Uri(typeof(WebScreenshotTaker).Assembly.Location).LocalPath), "Screenshots"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebScreenshotTaker"/> class with screenshots directory.
        /// </summary>
        /// <param name="driver"><see cref="WebDriver"/> instance</param>
        /// <param name="screenshotsDir">directory to save screenshots to</param>
        public WebScreenshotTaker(WebDriver driver, string screenshotsDir)
        {
            _driver = driver;
            _screenshotsDir = screenshotsDir;

            if (!Directory.Exists(screenshotsDir))
            {
                Directory.CreateDirectory(screenshotsDir);
            }
        }

        /// <summary>
        /// Takes screenshot and saves by specified path as png file.
        /// if path is longer than 255 symbols it's truncated with trailing '~'
        /// </summary>
        /// <param name="folder">folder to save screenshot to</param>
        /// <param name="fileName">screenshot file name without extension</param>
        /// <returns>path to the screenshot file</returns>
        public string TakeScreenshot(string folder, string fileName)
        {
            OpenQA.Selenium.Screenshot printScreen = GetScreenshot();

            if (printScreen == null)
            {
                return string.Empty;
            }

            try
            {
                Logger.Instance.Log(LogLevel.Debug, $"{LogPrefix}: Saving browser print screen...");

                string filePath = Path.Combine(folder, fileName);

                if (filePath.Length > MaxLength)
                {
                    filePath = filePath.Substring(0, MaxLength - 1) + "~";
                }

                filePath += ".png";
                printScreen.SaveAsFile(filePath);
                return filePath;
            }
            catch (Exception e)
            {
                Logger.Instance.Log(LogLevel.Warning, $"{LogPrefix}: Failed to save browser print screen: {e}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Take screenshot with specified name and save to screenshots directory.
        /// </summary>
        /// <param name="fileName">screenshot file name without extension</param>
        /// <returns>path to the screenshot file</returns>
        public string TakeScreenshot(string fileName) => TakeScreenshot(_screenshotsDir, fileName);

        /// <summary>
        /// Subscribe to Unicorn events.
        /// </summary>
        public void ScribeToTafEvents()
        {
            Test.OnTestFail += TakeScreenshot;
            SuiteMethod.OnSuiteMethodFail += TakeScreenshot;
        }

        /// <summary>
        /// Unsubscribe from Unicorn events.
        /// </summary>
        public void UnsubscribeFromTafEvents()
        {
            Test.OnTestFail -= TakeScreenshot;
            SuiteMethod.OnSuiteMethodFail -= TakeScreenshot;
        }

        private OpenQA.Selenium.Screenshot GetScreenshot()
        {
            try
            {
                Logger.Instance.Log(LogLevel.Debug, $"{LogPrefix}: Creating browser print screen...");

                return (_driver.SeleniumDriver as OpenQA.Selenium.ITakesScreenshot).GetScreenshot();
            }
            catch (Exception e)
            {
                Logger.Instance.Log(LogLevel.Warning, $"{LogPrefix}: Failed to get browser print screen: {e}");
                return null;
            }
        }

        private void TakeScreenshot(SuiteMethod suiteMethod)
        {
            var mime = "image/png";
            var screenshotPath = TakeScreenshot(suiteMethod.Outcome.FullMethodName);

            suiteMethod.Outcome.Attachments.Add(new Attachment("Screenshot", mime, screenshotPath));
        }
    }
}
