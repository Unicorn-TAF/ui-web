﻿using System.Windows.Automation;

namespace Unicorn.UI.Desktop.Controls.Typified
{
    /// <summary>
    /// Describes base pane control.
    /// </summary>
    public class Pane : GuiContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pane"/> class.
        /// </summary>
        public Pane()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pane"/> class with wraps specific <see cref="AutomationElement"/>
        /// </summary>
        /// <param name="instance"><see cref="AutomationElement"/> instance to wrap</param>
        public Pane(AutomationElement instance)
            : base(instance)
        {
        }

        /// <summary>
        /// Gets UIA pane control type.
        /// </summary>
        public override ControlType UiaType => ControlType.Pane;
    }
}