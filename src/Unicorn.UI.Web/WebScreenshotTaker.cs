using System;
using Unicorn.Taf.Core.Logging;
using Unicorn.UI.Core;
using Unicorn.UI.Web.Driver;

namespace Unicorn.UI.Web
{
    /// <summary>
    /// Provides ability to take web browser screenshots (works on both UI and headless modes).
    /// </summary>
    public class WebScreenshotTaker : ScreenshotTakerBase, IDisposable
    {
        private const string LogPrefix = nameof(WebScreenshotTaker);
        private readonly WebDriver _driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebScreenshotTaker"/> class with default directory.<para/>
        /// Default directory is ".\Screenshots" (created automatically if it does not exist).
        /// </summary>
        /// <param name="driver"><see cref="WebDriver"/> instance</param>
        public WebScreenshotTaker(WebDriver driver) : this(driver, DefaultDirectory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebScreenshotTaker"/> class with screenshots directory.
        /// </summary>
        /// <param name="driver"><see cref="WebDriver"/> instance</param>
        /// <param name="screenshotsDir">directory to save screenshots to</param>
        public WebScreenshotTaker(WebDriver driver, string screenshotsDir) : base(screenshotsDir)
        {
            _driver = driver;
        }

        /// <summary>
        /// Gets image format string.
        /// </summary>
        protected override string ImageFormat { get; } = "png";

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
                ULog.Debug("{0}: Saving browser print screen...", LogPrefix);

                string filePath = BuildFileName(folder, fileName);
                printScreen.SaveAsFile(filePath);
                return filePath;
            }
            catch (Exception e)
            {
                ULog.Warn("{0}: Failed to save browser print screen: \n{1}", LogPrefix, e);
                return string.Empty;
            }
        }

        /// <summary>
        /// Take screenshot with specified name and save to screenshots directory.
        /// </summary>
        /// <param name="fileName">screenshot file name without extension</param>
        /// <returns>path to the screenshot file</returns>
        public override string TakeScreenshot(string fileName) => TakeScreenshot(ScreenshotsDir, fileName);

        /// <summary>
        /// Unsubscribes screenshotter from taf events if was subscribed.
        /// </summary>
        public void Dispose() =>
            UnsubscribeFromTafEvents();

        private OpenQA.Selenium.Screenshot GetScreenshot()
        {
            try
            {
                ULog.Debug("{0}: Creating browser print screen...", LogPrefix);

                return (_driver.SeleniumDriver as OpenQA.Selenium.ITakesScreenshot).GetScreenshot();
            }
            catch (Exception e)
            {
                ULog.Warn("{0}: Failed to get browser print screen: \n{1}", LogPrefix, e);
                return null;
            }
        }
    }
}
