using Unicorn.Taf.Core.Testing.Attributes;
using Unicorn.Taf.Core.Verification;
using Unicorn.Taf.Core.Verification.Matchers;
using Unicorn.UnitTests.UI.Gui.Web;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [Suite]
    public class WebDynamicGrid : WebTestsBase
    {
        private JqueryDataGridPage page;

        [BeforeSuite]
        public void Setup()
        {
            page = NavigateToPage<JqueryDataGridPage>();
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Get header by name")]
        public void TestGetHeaderByName() =>
            Assert.That(page.DataGrid.GetColumnHeader("Continent").Text, Is.EqualTo("Continent"));

        [Author("Vitaliy Dobriyan")]
        [Test("Get rows count")]
        public void TestGetRowsCount() =>
            Assert.That(page.DataGrid.RowsCount, Is.EqualTo(20));

        [Author("Vitaliy Dobriyan")]
        [Test("Get specific row")]
        public void TestGetSpecificRow() =>
            Assert.That(page.DataGrid.GetRow("Name", "Argentina").GetCell(0).Data, Is.EqualTo("South America"));

        [Author("Vitaliy Dobriyan")]
        [Test("Get row by index")]
        public void TestGetRowByIndex() =>
            Assert.That(page.DataGrid.GetRow(2).GetCell(1).Data, Is.EqualTo("Angola"));

        [Author("Vitaliy Dobriyan")]
        [Test("Get specific cell")]
        public void TestGetSpecificCell() =>
            Assert.That(page.DataGrid.GetCell("Name", "Andorra", "Population").Data, Is.EqualTo("78000"));

        [Author("Vitaliy Dobriyan")]
        [Test("Get cell by indexes")]
        public void TestGetCellByIndexes() =>
            Assert.That(page.DataGrid.GetCell(7, 3).Data, Is.EqualTo("83600.00"));

        [Author("Vitaliy Dobriyan")]
        [Test("Has column (positive)")]
        public void TestHasColumnPositive() =>
            Assert.IsTrue(page.DataGrid.HasColumn("Surface"));

        [Author("Vitaliy Dobriyan")]
        [Test("Has column (negative)")]
        public void TestHasColumnNegative() =>
            Assert.IsFalse(page.DataGrid.HasColumn("PopulationWee"));

        [Author("Vitaliy Dobriyan")]
        [Test("Has row (positive)")]
        public void TestHasRowPositive() =>
            Assert.IsTrue(page.DataGrid.HasRow("Population", "0"));

        [Author("Vitaliy Dobriyan")]
        [Test("Has row (negative)")]
        public void TestHasRowNegative() =>
            Assert.IsFalse(page.DataGrid.HasRow("Population", "-900"));

        [Author("Vitaliy Dobriyan")]
        [Test("Has cell (3 params positive)")]
        public void TestHasCell3ParamsPositive() =>
            Assert.IsTrue(page.DataGrid.HasCell("Population", "0", "Continent"));

        [Author("Vitaliy Dobriyan")]
        [Test("Has cell (3 params negative)")]
        public void TestHasCell3ParamsNegative() =>
            Assert.IsFalse(page.DataGrid.HasCell("Population", "0", "Contsadsainent"));

        [Author("Vitaliy Dobriyan")]
        [Test("Has cell (4 params positive)")]
        public void TestHasCell4ParamsPositive() =>
            Assert.IsTrue(page.DataGrid.HasCell("Population", "0", "Continent", "Antarctica"));

        [Author("Vitaliy Dobriyan")]
        [Test("Has cell (4 params negative)")]
        public void TestHasCell4ParamsNegative() =>
            Assert.IsFalse(page.DataGrid.HasCell("Population", "0", "Continent", "Weee"));
    }
}
