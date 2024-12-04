using Unicorn.Taf.Core.Logging;
using Unicorn.UI.Core.Controls.Interfaces.Typified;

namespace Unicorn.UI.Web.Controls.Typified
{
    /// <summary>
    /// Describes base checkbox control described by <code>&lt;input type="checkbox"/&gt;</code>
    /// </summary>
    public class Checkbox : WebControl, ICheckbox
    {
        /// <summary>
        /// Gets a value indicating whether checkbox is checked.
        /// </summary>
        public virtual bool Checked => Instance.Selected;

        /// <summary>
        /// Sets checkbox checked state
        /// </summary>
        /// <param name="isChecked">true - to check; false - to uncheck</param>
        /// <returns>true - if state was changed; false - if already in specified state</returns>
        public virtual bool SetCheckedState(bool isChecked) =>
            isChecked ? Check() : Uncheck();

        /// <summary>
        /// Checks the checkbox.
        /// </summary>
        /// <returns>true - if checkbox has been checked; false - if it was already in checked state</returns>
        public bool Check()
        {
            ULog.Debug("Check {0}", this);

            if (Checked)
            {
                ULog.Trace("No need to check (checked by default)");
                return false;
            }

            Click();

            ULog.Trace("Checkbox has been checked");

            return true;
        }

        /// <summary>
        /// Unchecks the checkbox.
        /// </summary>
        /// <returns>true - if checkbox has been unchecked; false - if it was already in unchecked state</returns>
        public bool Uncheck()
        {
            ULog.Debug("Uncheck {0}", this);

            if (!Checked)
            {
                ULog.Trace("No need to uncheck (unchecked by default)");
                return false;
            }

            Click();
            ULog.Trace("Checkbox has been unchecked");

            return true;
        }
    }
}
