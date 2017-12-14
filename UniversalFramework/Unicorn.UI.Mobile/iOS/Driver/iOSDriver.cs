﻿using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using Unicorn.Core.Logging;
using Unicorn.UI.Core.Driver;

namespace Unicorn.UI.Mobile.iOS.Driver
{
    public class iOSDriver : iOSSearchContext, IDriver
    {
        public static AppiumDriver<IOSElement> Driver;
        private static DesiredCapabilities capabilities = null;
        private static Uri uri = null;
        private static bool needInit = false;
        private static iOSDriver instance = null;

        private iOSDriver()
        {
            Driver = new IOSDriver<IOSElement>(uri, capabilities, TimeSpan.FromSeconds(120));
            ImplicitlyWait = timeoutDefault;
        }

        public static iOSDriver Instance
        {
            get
            {
                if (instance == null || needInit)
                {
                    instance = new iOSDriver();
                    instance.SearchContext = Driver.FindElementByXPath(".//*");
                    needInit = false;
                    Logger.Instance.Info(instance.SearchContext.TagName);
                    Logger.Instance.Debug($"iOSDriver initialized");
                }

                return instance;
            }
        }

        public TimeSpan ImplicitlyWait
        {
            get
            {
                return ImplicitlyWaitTimeout;
            }

            set
            {
                Driver.Manage().Timeouts().ImplicitWait = value;
                ImplicitlyWaitTimeout = value;
            }
        }

        public static void Init(string url, Dictionary<string, string> capabilitiesList = null)
        {
            needInit = true;
            uri = new Uri(url);

            capabilities = null;
            if (capabilitiesList != null)
            {
                capabilities = new DesiredCapabilities();

                foreach (string key in capabilitiesList.Keys)
                {
                    capabilities.SetCapability(key, capabilitiesList[key]);
                }
            }
        }

        public void Get(string path)
        {
            Driver.Navigate().GoToUrl(path);
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
