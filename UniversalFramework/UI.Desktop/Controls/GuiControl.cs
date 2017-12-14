﻿using System;
using System.Windows;
using System.Windows.Automation;
using Unicorn.UI.Core.Controls;
using Unicorn.UI.Core.Driver;
using Unicorn.UI.Desktop.Driver;
using Unicorn.UI.Desktop.Input;

namespace Unicorn.UI.Desktop.Controls
{
    public abstract class GuiControl : GuiSearchContext, IControl
    {
        public bool Cached = true;

        public GuiControl()
        {
        }

        public GuiControl(AutomationElement instance)
        {
            this.Instance = instance;
        }

        public ByLocator Locator
        {
            get;

            set;
        }

        public virtual string ClassName => null;

        public abstract ControlType Type { get; }

        public virtual AutomationElement Instance
        {
            get
            {
                return SearchContext;
            }

            set
            {
                SearchContext = value;
            }
        }

        public string Text
        {
            get
            {
                var name = this.Instance.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;
                var id = this.Instance.GetCurrentPropertyValue(AutomationElement.AutomationIdProperty) as string;
                return !string.IsNullOrEmpty(name) ? name : id;
            }
        }

        public bool Enabled
        {
            get
            {
                return (bool)this.Instance.GetCurrentPropertyValue(AutomationElement.IsEnabledProperty);
            }
        }

        public bool Visible
        {
            get
            {
                bool isVisible;
                try
                {
                    isVisible = !this.Instance.Current.IsOffscreen;
                }
                catch (ElementNotAvailableException)
                {
                    isVisible = false;
                }

                return isVisible;
            }
        }

        public System.Drawing.Point Location
        {
            get
            {
                return new System.Drawing.Point(this.BoundingRectangle.Location.X, this.BoundingRectangle.Location.Y);
            }
        }

        public System.Drawing.Rectangle BoundingRectangle
        {
            get
            {
                return (System.Drawing.Rectangle)this.Instance.GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
            }
        }

        protected override AutomationElement SearchContext
        {
            get
            {
                if (!this.Cached)
                {
                    base.SearchContext = GetNativeControlFromParentContext(this.Locator, GetType());
                }

                return base.SearchContext;
            }

            set
            {
                base.SearchContext = value;
            }
        }

        public string GetAttribute(string attribute)
        {
            AutomationProperty ap;

            switch (attribute.ToLower())
            {
                case "class":
                    ap = AutomationElement.ClassNameProperty;
                    break;
                case "text":
                    ap = AutomationElement.NameProperty;
                    break;
                case "enabled":
                    return this.Enabled.ToString();
                case "visible":
                    return this.Visible.ToString();
                default:
                    throw new ArgumentException($"No such property as {attribute}");
            }

            return (string)Instance.GetCurrentPropertyValue(ap);
        }

        public void Click()
        {
            this.WaitForEnabled();

            object pattern = null;

            try
            {
                if (Instance.TryGetCurrentPattern(InvokePattern.Pattern, out pattern))
                {
                    ((InvokePattern)pattern).Invoke();
                }
                else
                {
                    ((TogglePattern)Instance.GetCurrentPattern(TogglePattern.Pattern)).Toggle();
                }
            }
            catch
            {
                MouseClick();
            }
        }

        public void MouseClick()
        {
            Instance.SetFocus();
            Point point;
            if (!Instance.TryGetClickablePoint(out point))
            {
                Point pt = new Point(3, 3);
                var rect = (Rect)Instance.GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
                point = rect.TopLeft;
                point.Offset(pt.X, pt.Y);
            }

            Mouse.Instance.Click(point);
        }

        public void RightClick()
        {
            Instance.SetFocus();

            Point point;
            if (!Instance.TryGetClickablePoint(out point))
            {
                Point pt = new Point(3, 3);
                var rect = (Rect)Instance.GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
                point = rect.TopLeft;
                point.Offset(pt.X, pt.Y);
            }

            Mouse.Instance.RightClick(point);
        }

        public AutomationElement GetParent()
        {
            TreeWalker treeWalker = TreeWalker.ControlViewWalker;
            return treeWalker.GetParent(Instance);
        }

        #region "Helpers"

        protected T GetPattern<T>() where T : BasePattern
        {
            var pattern = (AutomationPattern)typeof(T).GetField("Pattern").GetValue(null);
            object patternObject;
            Instance.TryGetCurrentPattern(pattern, out patternObject);
            return (T)patternObject;
        }

        #endregion
    }
}
