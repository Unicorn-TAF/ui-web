﻿using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using Unicorn.UI.Core.Controls;
using Unicorn.UI.Core.Driver;

namespace Unicorn.UI.Mobile.Base.Driver
{
    public abstract class MobileSearchContext : UISearchContext
    {
        public AppiumWebElement ParentContext { get; set; }

        protected virtual AppiumWebElement SearchContext { get; set; }

        protected override T WaitForWrappedControl<T>(ByLocator locator)
        {
            AppiumWebElement elementToWrap = GetNativeControl(locator);
            return Wrap<T>(elementToWrap);
        }

        protected override IList<T> GetWrappedControlsList<T>(ByLocator locator)
        {
            var elementsToWrap = GetNativeControlsList(locator);
            List<T> controlsList = new List<T>();

            foreach (var elementToWrap in elementsToWrap)
            {
                controlsList.Add(Wrap<T>(elementToWrap));
            }

            return controlsList;
        }

        protected override T GetFirstChildWrappedControl<T>()
        {
            var elementToWrap = GetNativeControlsList(new ByLocator(Using.Web_Xpath, "./*"))[0];
            return Wrap<T>(elementToWrap);
        }

        protected AppiumWebElement GetNativeControl(ByLocator locator)
        {
            return GetNativeControlFromContext(locator, this.SearchContext);
        }

        protected AppiumWebElement GetNativeControlFromParentContext(ByLocator locator)
        {
            return GetNativeControlFromContext(locator, this.ParentContext);
        }

        protected IList<AppiumWebElement> GetNativeControlsList(ByLocator locator)
        {
            By by = GetNativeLocator(locator);

            try
            {
                IList<AppiumWebElement> nativeControls = this.SearchContext.FindElements(by);
                return nativeControls;
            }
            catch (NoSuchElementException)
            {
                return new List<AppiumWebElement>();
            }
        }

        private AppiumWebElement GetNativeControlFromContext(ByLocator locator, AppiumWebElement context)
        {
            By by = GetNativeLocator(locator);
            try
            {
                AppiumWebElement nativeControl = context.FindElement(by);
                return nativeControl;
            }
            catch (NoSuchElementException)
            {
                throw new ControlNotFoundException($"Unable to find control by {locator}");
            }
        }

        protected By GetNativeLocator(ByLocator locator)
        {
            switch (locator.How)
            {
                case Using.Web_Xpath:
                    return By.XPath(locator.Locator);
                case Using.Web_Css:
                    return By.CssSelector(locator.Locator);
                case Using.Id:
                    return By.Id(locator.Locator);
                case Using.Name:
                    return By.Name(locator.Locator);
                case Using.Class:
                    return By.ClassName(locator.Locator);
                case Using.Web_Tag:
                    return By.TagName(locator.Locator);
                default:
                    throw new ArgumentException($"Incorrect locator type specified:  {locator.How}");
            }
        }

        protected abstract T Wrap<T>(AppiumWebElement elementToWrap);
    }
}