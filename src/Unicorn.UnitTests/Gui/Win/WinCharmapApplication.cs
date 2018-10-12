﻿using Unicorn.UI.Core.Driver;
using Unicorn.UI.Core.PageObject;
using Unicorn.UI.Win.PageObject;

namespace Unicorn.UnitTests.Gui
{
    public class WinCharmapApplication : Application
    {
        [Find(Using.Name, "Character Map")]
        public WinWindowCharMap Window { get; set; }

        public WinCharmapApplication(string path, string exeName) : base(path, exeName)
        {
        }
    }
}