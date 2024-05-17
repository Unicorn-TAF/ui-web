using OpenQA.Selenium;
using Unicorn.UI.Core.PageObject.By;
using Unicorn.UI.Web.Controls.Typified;
using Unicorn.UI.Web.PageObject;
using Unicorn.UI.Web.PageObject.Attributes;

namespace Unicorn.UnitTests.UI.Gui.Web
{
    [PageInfo("primitive-controls.html", "Primitive html controls")]
    public class PrimitiveControlsPage : WebPage
    {
        public PrimitiveControlsPage(IWebDriver driver) : base(driver)
        {
        }

        [ById("simple-dd")]
        public Dropdown SimpleDropdown { get; set; }

        [ById("groups-dd")]
        public Dropdown DropdownWithGroups { get; set; }

        [ById("table")]
        public DataGrid DataGrid { get; set; }
    }
}
