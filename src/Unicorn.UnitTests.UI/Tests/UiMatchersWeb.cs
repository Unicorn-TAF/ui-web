using Unicorn.UnitTests.UI.Gui.Web;
using Unicorn.Taf.Core.Testing.Attributes;
using Unicorn.Taf.Core.Verification;
using Ui = Unicorn.UI.Core.Matchers.UI;
using System;

namespace Unicorn.UnitTests.UI.Tests
{
    [Suite]
    public class UiMatchersWeb : WebTestsBase
    {
        [Author("Vitaliy Dobriyan")]
        [Test("Check WindowHasTitleMatcher")]
        public void TestWindowHasTitleMatcherMatcher()
        {
            JqueryDialogPage dialogPage = NavigateToPage<JqueryDialogPage>();
            Assert.That(dialogPage.Dialog, Ui.Window.HasTitle("Empty the recycle bin?"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check WindowHasTitleMatcher Negative")]
        public void TestWindowHasTitleMatcherMatcherNegative()
        {
            JqueryDialogPage dialogPage = NavigateToPage<JqueryDialogPage>();
            AssertThrows(() => Assert.That(dialogPage.Dialog, Ui.Window.HasTitle("weeee")));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check ModalWindowHasTextMatcher")]
        public void TestModalWindowHasTextMatcher()
        {
            JqueryDialogPage dialogPage = NavigateToPage<JqueryDialogPage>();
            Assert.That(dialogPage.Dialog, Ui.Window.HasText(
                "These items will be permanently deleted and cannot be recovered. Are you sure?"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check ModalWindowHasTextMatcher Negative")]
        public void TestModalWindowHasTextMatcherNegative()
        {
            JqueryDialogPage dialogPage = NavigateToPage<JqueryDialogPage>();
            AssertThrows(() => Assert.That(dialogPage.Dialog, Ui.Window.HasText("weee")));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasRowsCountMatcher")]
        public void TestDataGridHasRowsCountMatcher()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            Assert.That(gridPage.DataGrid, Ui.DataGrid.HasRowsCount(20));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasRowsCountMatcher Negative")]
        public void TestDataGridHasRowsCountMatcherNegative()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            AssertThrows(() => Assert.That(gridPage.DataGrid, Ui.DataGrid.HasRowsCount(10)));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasRowMatcher")]
        public void TestDataGridHasRowMatcher()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            Assert.That(gridPage.DataGrid, Ui.DataGrid.HasRow("Continent", "Asia"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasRowMatcher Negative")]
        public void TestDataGridHasRowMatcherNegative()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            AssertThrows(() => Assert.That(gridPage.DataGrid, Ui.DataGrid.HasRow("Continent", "weee")));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasColumnMatcher")]
        public void TestDataGridHasColumnMatcher()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            Assert.That(gridPage.DataGrid, Ui.DataGrid.HasColumn("Name"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasColumnMatcher Negative")]
        public void TestDataGridHasColumnMatcherNegative()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            AssertThrows(() => Assert.That(gridPage.DataGrid, Ui.DataGrid.HasColumn("weee")));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasCellWithTextMatcher1")]
        public void TestDataGridHasCellWithTextMatcher1()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            Assert.That(gridPage.DataGrid, Ui.DataGrid.HasCellWithText("Continent", "Europe", "Name", "Albania"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasCellWithTextMatcher1 Negative")]
        public void TestDataGridHasCellWithTextMatcher1Negative()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            AssertThrows(() => Assert.That(gridPage.DataGrid, Ui.DataGrid.HasCellWithText("Continent", "Europe", "Name", "weee")));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasCellWithTextMatcher2")]
        public void TestDataGridHasCellWithTextMatcher2()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            Assert.That(gridPage.DataGrid, Ui.DataGrid.HasCellWithText(0, 1, "Aruba"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DataGridHasCellWithTextMatcher2 Negative")]
        public void TestDataGridHasCellWithTextMatcher2Negative()
        {
            JqueryDataGridPage gridPage = NavigateToPage<JqueryDataGridPage>();
            AssertThrows(() => Assert.That(gridPage.DataGrid, Ui.DataGrid.HasCellWithText(0, 1, "weee")));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DropdownHasSelectedValueMatcher")]
        public void TestDropdownHasSelectedValueMatcher()
        {
            JquerySelectPage selectPage = NavigateToPage<JquerySelectPage>();
            Assert.That(selectPage.Dropdown, Ui.Dropdown.HasSelectedValue("Medium"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check DropdownHasSelectedValueMatcher Negative")]
        public void TestDropdownHasSelectedValueMatcherNegative()
        {
            JquerySelectPage selectPage = NavigateToPage<JquerySelectPage>();
            AssertThrows(() => Assert.That(selectPage.Dropdown, Ui.Dropdown.HasSelectedValue("weee")));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check CheckboxCheckedMatcher")]
        public void TestCheckboxCheckedMatcher()
        {
            JqueryCheckboxRadioPage cboxPage = NavigateToPage<JqueryCheckboxRadioPage>();
            cboxPage.JqCheckboxToCheck1.JsClick();
            Assert.That(cboxPage.JqCheckboxToCheck1, Ui.Checkbox.Checked());
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check CheckboxCheckedMatcher Negative")]
        public void TestCheckboxCheckedMatcherNegative()
        {
            JqueryCheckboxRadioPage cboxPage = NavigateToPage<JqueryCheckboxRadioPage>();
            AssertThrows(() => Assert.That(cboxPage.JqCheckbox, Ui.Checkbox.Checked()));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check CheckboxHasCheckStateMatcher")]
        public void TestCheckboxHasCheckStateMatcher()
        {
            JqueryCheckboxRadioPage cboxPage = NavigateToPage<JqueryCheckboxRadioPage>();
            Assert.That(cboxPage.JqCheckbox, Ui.Checkbox.HasCheckState(false));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check CheckboxHasCheckStateMatcher Negative")]
        public void TestCheckboxHasCheckStateMatcherNegative()
        {
            JqueryCheckboxRadioPage cboxPage = NavigateToPage<JqueryCheckboxRadioPage>();
            cboxPage.JqCheckboxToCheck2.JsClick();
            AssertThrows(() => Assert.That(cboxPage.JqCheckboxToCheck2, Ui.Checkbox.HasCheckState(false)));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check SelectedMatcher")]
        public void TestSelectedMatcher()
        {
            JqueryCheckboxRadioPage cboxPage = NavigateToPage<JqueryCheckboxRadioPage>();
            cboxPage.JqRadioToSelect.JsClick();
            Assert.That(cboxPage.JqRadioToSelect, Ui.Control.Selected());
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check SelectedMatcher Negative")]
        public void TestSelectedMatcherNegative()
        {
            JqueryCheckboxRadioPage cboxPage = NavigateToPage<JqueryCheckboxRadioPage>();
            AssertThrows(() => Assert.That(cboxPage.JqRadio, Ui.Control.Selected()));
        }

        private void AssertThrows(Action action)
        {
            try
            {
                action();
                throw new Exception("Test should fail with AssertionException");
            }
            catch (AssertionException)
            {
            }
        }
    }
}
