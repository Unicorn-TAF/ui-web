using NUnit.Framework;
using Unicorn.UI.Web.Driver;
using Unicorn.UnitTests.UI.Gui.Web;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [TestFixture]
    public class WebPrimitiveControlsTests : WebTestsBase
    {
        private static PrimitiveControlsPage page;
        private static WebDriver webdriver;

        [OneTimeSetUp]
        public static void Setup()
        {
            webdriver = DriverManager.GetDriverInstance();
            page = NavigateToPage<PrimitiveControlsPage>(webdriver.SeleniumDriver);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            webdriver.Close();
            webdriver = null;
            page = null;
        }

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive Dropdown current selection")]
        public void TestPrimitiveDropdownCurrentValue()
        {
            webdriver.SeleniumDriver.Navigate().Refresh();
            Assert.That(page.SimpleDropdown.SelectedValue, Is.EqualTo("Medium"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive Dropdown select already selected value")]
        public void TestPrimitiveDropdownSelectAlreadySelectedValue()
        {
            webdriver.SeleniumDriver.Navigate().Refresh();
            Assert.IsFalse(page.SimpleDropdown.Select("Medium"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive Dropdown selection by text")]
        public void TestPrimitiveDropdownSelectionByText()
        {
            string valueToSelect = "Fast";
            webdriver.SeleniumDriver.Navigate().Refresh();
            page.SimpleDropdown.Select(valueToSelect);
            Assert.That(page.SimpleDropdown.SelectedValue, Is.EqualTo(valueToSelect));
        }

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive Dropdown current selection")]
        public void TestPrimitiveDropdownSelectionByValue()
        {
            page.DropdownWithGroups.SelectByValue("option4");
            Assert.That(page.DropdownWithGroups.SelectedValue, Is.EqualTo("option 4"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive DataGrid rows count")]
        public void TestPrimitiveDataGridRowsCount() =>
            Assert.That(page.DataGrid.RowsCount, Is.EqualTo(7));

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive DataGrid HasColumn positive")]
        public void TestPrimitiveDataGridHasColumnPositive() =>
            Assert.IsTrue(page.DataGrid.HasColumn("Name"));

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive DataGrid HasColumn negative")]
        public void TestPrimitiveDataGridHasColumnNegative() =>
            Assert.IsFalse(page.DataGrid.HasColumn("Name1234"));

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive DataGrid HasRow positive")]
        public void TestPrimitiveDataGridHasRowPositive() =>
            Assert.IsTrue(page.DataGrid.HasRow("Name", "Argentina"));

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive DataGrid HasRow negative")]
        public void TestPrimitiveDataGridHasRowNegative() =>
            Assert.IsFalse(page.DataGrid.HasRow("Name", "weee"));

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "Primitive DataGrid get cell data")]
        public void TestPrimitiveDataGridGetCellData() =>
            Assert.That(page.DataGrid.GetCell("Name", "Argentina", "Continent").Data, Is.EqualTo("South America"));
    }
}
