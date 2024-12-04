using Unicorn.Taf.Core.Testing.Attributes;
using Unicorn.Taf.Core.Verification;
using Unicorn.Taf.Core.Verification.Matchers;
using Unicorn.UnitTests.UI.Gui.Web;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [Suite]
    public class WebPrimitiveControlsTests : WebTestsBase
    {
        private PrimitiveControlsPage page;

        [BeforeSuite]
        public void Setup()
        {
            page = NavigateToPage<PrimitiveControlsPage>();
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive Dropdown current selection")]
        public void TestPrimitiveDropdownCurrentValue()
        {
            Refresh();
            Assert.That(page.SimpleDropdown.SelectedValue, Is.EqualTo("Medium"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive Dropdown select already selected value")]
        public void TestPrimitiveDropdownSelectAlreadySelectedValue()
        {
            Refresh();
            Assert.IsFalse(page.SimpleDropdown.Select("Medium"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive Dropdown selection by text")]
        public void TestPrimitiveDropdownSelectionByText()
        {
            string valueToSelect = "Fast";
            Refresh();
            page.SimpleDropdown.Select(valueToSelect);
            Assert.That(page.SimpleDropdown.SelectedValue, Is.EqualTo(valueToSelect));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive Dropdown current selection")]
        public void TestPrimitiveDropdownSelectionByValue()
        {
            page.DropdownWithGroups.SelectByValue("option4");
            Assert.That(page.DropdownWithGroups.SelectedValue, Is.EqualTo("option 4"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive DataGrid rows count")]
        public void TestPrimitiveDataGridRowsCount() =>
            Assert.That(page.DataGrid.RowsCount, Is.EqualTo(7));

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive DataGrid HasColumn positive")]
        public void TestPrimitiveDataGridHasColumnPositive() =>
            Assert.IsTrue(page.DataGrid.HasColumn("Name"));

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive DataGrid HasColumn negative")]
        public void TestPrimitiveDataGridHasColumnNegative() =>
            Assert.IsFalse(page.DataGrid.HasColumn("Name1234"));

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive DataGrid HasRow positive")]
        public void TestPrimitiveDataGridHasRowPositive() =>
            Assert.IsTrue(page.DataGrid.HasRow("Name", "Argentina"));

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive DataGrid HasRow negative")]
        public void TestPrimitiveDataGridHasRowNegative() =>
            Assert.IsFalse(page.DataGrid.HasRow("Name", "weee"));

        [Author("Vitaliy Dobriyan")]
        [Test("Primitive DataGrid get cell data")]
        public void TestPrimitiveDataGridGetCellData() =>
            Assert.That(page.DataGrid.GetCell("Name", "Argentina", "Continent").Data, Is.EqualTo("South America"));
    }
}
