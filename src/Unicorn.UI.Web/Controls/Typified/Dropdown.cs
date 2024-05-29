using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using Unicorn.Taf.Core.Logging;
using Unicorn.UI.Core.Controls;
using Unicorn.UI.Core.Controls.Interfaces;
using Unicorn.UI.Core.Controls.Interfaces.Typified;

namespace Unicorn.UI.Web.Controls.Typified
{
    /// <summary>
    /// Default implementation for dropdown described by next structure:<br/>
    /// 
    /// <code>
    /// &lt;select&gt;
    ///   &lt;option/&gt; 
    ///   &lt;option/&gt; 
    ///   &lt;option/&gt; 
    /// &lt;/select&gt;
    /// </code>
    /// Has definitions of of basic methods and properties.
    /// </summary>
    public class Dropdown : WebControl, IDropdown
    {
        /// <summary>
        /// Gets currently selected value.
        /// </summary>
        public string SelectedValue =>
            Options.FirstOrDefault(o => o.Selected)?.Text 
                ?? throw new ControlNotFoundException("No option is selected");

        bool IExpandable.Expanded => false;

        bool IExpandable.Collapse() => false;

        bool IExpandable.Expand() => false;

        private IList<IWebElement> Options => Instance.FindElements(By.TagName("option"));

        /// <summary>
        /// Selects dropdown option by displayed text.
        /// </summary>
        /// <param name="itemName">item text</param>
        /// <returns>true - if selection was made, false - if the item is already selected</returns>
        /// <exception cref="ControlNotFoundException">if option with specified text was not found</exception>
        public bool Select(string itemName)
        {
            if (itemName == null)
            {
                throw new ArgumentNullException(nameof(itemName), "Item name must not be null");
            }

            ULog.Debug("Select '{0}' item from {1}", itemName, this);

            IWebElement optionToSelect = Options.FirstOrDefault(option => option.Text.Equals(itemName)) ?? 
                throw new ControlNotFoundException($"Item '{itemName}' was not found in dropdown");

            return MakeSelection(optionToSelect);
        }

        /// <summary>
        /// Selects dropdown option by value.
        /// </summary>
        /// <param name="itemValue">item value</param>
        /// <returns>true - if selection was made, false - if the item is already selected</returns>
        /// <exception cref="ControlNotFoundException">if option with specified value was not found</exception>
        public bool SelectByValue(string itemValue)
        {
            if (itemValue == null)
            {
                throw new ArgumentNullException(nameof(itemValue), "Item value must not be null");
            }

            ULog.Debug("Select '{0}' value from {1}", itemValue, this);

            IList<IWebElement> options = Instance.FindElements(By.CssSelector($"option[value = '{itemValue}']"));

            if (options.Count > 0)
            {
                return MakeSelection(options[0]);
            }

            throw new ControlNotFoundException($"Item with value '{itemValue}' was not found in dropdown");
        }

        /// <summary>
        /// Gets all dropdown options.
        /// </summary>
        /// <returns>string array with options</returns>
        public List<string> GetOptions() => 
            Options.Select(o => o.Text).ToList();

        private static bool MakeSelection(IWebElement optionToSelect)
        {
            if (optionToSelect.Selected)
            {
                ULog.Trace("No need to select (the item is selected by default)");
                return false;
            }
            else
            {
                optionToSelect.Click();
                ULog.Trace("Item was selected");
                return true;
            }
        }
    }
}
