﻿using System.Drawing;
using OpenQA.Selenium.Interactions;
using Unicorn.Taf.Core.Logging;
using Unicorn.UI.Core.Controls;
using Unicorn.UI.Core.Driver;
using Unicorn.UI.Core.PageObject;
using Unicorn.UI.Web.Driver;
using Selenium = OpenQA.Selenium;

namespace Unicorn.UI.Web.Controls
{
    /// <summary>
    /// Represents basic web control. Contains number of main properties and action under the control.
    /// </summary>
    public class WebControl : WebSearchContext, IControl
    {
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebControl"/> class.
        /// </summary>
        public WebControl()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebControl"/> class with wraps specific <see cref="Selenium.IWebElement"/>
        /// </summary>
        /// <param name="instance"><see cref="Selenium.IWebElement"/> instance to wrap</param>
        public WebControl(Selenium.IWebElement instance)
        {
            Instance = instance;
        }

        /// <summary>
        /// Gets or sets a value indicating whether need to cache the control.
        /// Cached control is not searched for on each next call. Not cached control is searched each time (as PageObject control).
        /// </summary>
        public bool Cached { get; set; } = true;

        /// <summary>
        /// Gets or sets locator to find control by.
        /// </summary>
        public ByLocator Locator { get; set; }

        /// <summary>
        /// Gets or sets control name.
        /// </summary>
        public string Name 
        { 
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = $"{GetType().Name} [{Locator?.ToString()}]";
                }

                return name;
            }

            set => name = value;
        }

        /// <summary>
        /// Gets or sets control wrapped instance as <see cref="Selenium.IWebElement"/> which is also current search context.
        /// </summary>
        public Selenium.IWebElement Instance
        {
            get
            {
                return (Selenium.IWebElement)SearchContext;
            }

            set
            {
                SearchContext = value;
                ContainerFactory.InitContainer(this);
            }
        }

        /// <summary>
        /// Gets control text.
        /// </summary>
        public string Text => Instance.Text;

        /// <summary>
        /// Gets a value indicating whether control is enabled in UI.
        /// </summary>
        public bool Enabled => Instance.Enabled;

        /// <summary>
        /// Gets a value indicating whether control is visible (not is Off-screen)
        /// </summary>
        public bool Visible => Instance.Displayed;

        /// <summary>
        /// Gets control location as <see cref="Point"/>
        /// </summary>
        public Point Location => Instance.Location;

        /// <summary>
        /// Gets control bounding rectangle as <see cref="Rectangle"/>
        /// </summary>
        public Rectangle BoundingRectangle => new Rectangle(Location, Instance.Size);

        /// <summary>
        /// Gets or sets control search context. 
        /// If control is not cached current context is searched from parent context by this control locator.
        /// </summary>
        protected override Selenium.ISearchContext SearchContext
        {
            get
            {
                if (!Cached)
                {
                    try
                    {
                        base.SearchContext = GetNativeControlFromParentContext(Locator);
                    } 
                    catch (Selenium.StaleElementReferenceException)
                    {
                        ULog.Warn("Got StaleElementReferenceException, retrying control search...");
                        base.SearchContext = GetNativeControlFromParentContext(Locator);
                    }
                }

                return base.SearchContext;
            }

            set
            {
                base.SearchContext = value;
            }
        }

        /// <summary>
        /// Gets control attribute value as <see cref="string"/>
        /// </summary>
        /// <param name="attribute">attribute name</param>
        /// <returns>control attribute value as string</returns>
        public string GetAttribute(string attribute) =>
            Instance.GetAttribute(attribute);

        /// <summary>
        /// Perform click on control.
        /// </summary>
        public virtual void Click()
        {
            ULog.Debug("Click {0}", this);
            Instance.Click();
        }

        /// <summary>
        /// Perform JavaScript click on control.
        /// </summary>
        public virtual void JsClick()
        {
            ULog.Debug("JavaScript click {0}", this);

            Selenium.IJavaScriptExecutor js = (Selenium.IJavaScriptExecutor)
                ((Selenium.IWrapsDriver)Instance).WrappedDriver;

            js.ExecuteScript("arguments[0].click()", Instance);
        }

        /// <summary>
        /// Perform right click on control.
        /// </summary>
        public virtual void RightClick()
        {
            ULog.Debug("Right click {0}", this);
            var actions = new Actions(((Selenium.IWrapsDriver)Instance).WrappedDriver);
            actions.MoveToElement(Instance);
            actions.ContextClick();
            actions.Release().Perform();
        }

        /// <summary>
        /// Gets string description of the control.
        /// </summary>
        /// <returns>control description as string</returns>
        public override string ToString() => Name;
    }
}
