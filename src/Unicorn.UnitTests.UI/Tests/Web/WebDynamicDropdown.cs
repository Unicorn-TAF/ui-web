using Unicorn.Taf.Core.Testing.Attributes;
using Unicorn.Taf.Core.Verification;
using Unicorn.Taf.Core.Verification.Matchers;
using Unicorn.UnitTests.UI.Gui.Web;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [Suite]
    public class WebDynamicDropdown : WebTestsBase
    {
        private JquerySelectPage page;

        [BeforeSuite]
        public void Setup()
        {
            page = NavigateToPage<JquerySelectPage>();
        }

        [Author("Vitaliy Dobriyan")]
        [Test("No selection if value was already selected")]
        public void TestNoSelectionIfValueWasAlreadySelected()
        {
            page.Dropdown.Select("Medium");
            var isSelectionWasMade = page.Dropdown.Select("Medium");
            Assert.IsFalse(isSelectionWasMade);
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Option selection")]
        public void TestOptionSelection()
        {
            var newValue = "Faster";
            var isSelectionWasMade = page.Dropdown.Select(newValue);
            Assert.IsTrue(isSelectionWasMade);
            Assert.That(page.Dropdown.SelectedValue, Is.EqualTo(newValue));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check collapse default state")]
        public void TestCollapseDefaultState() =>
            Assert.IsFalse(page.Dropdown.Expanded);

        [Author("Vitaliy Dobriyan")]
        [Test("Check expanding")]
        public void TestExpanding()
        {
            page.Dropdown.Expand();
            Assert.IsTrue(page.Dropdown.Expanded);
            Assert.That(page.Dropdown.GetOptions().Count, Is.EqualTo(5));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Dropdown with no input")]
        public void TestDropdownWithNoInput()
        {
            var newValue = "Faster";
            var isSelectionWasMade = page.DropdownNoInput.Select(newValue);
            Assert.IsTrue(isSelectionWasMade);
            isSelectionWasMade = page.DropdownNoInput.Select(newValue);
            Assert.IsTrue(isSelectionWasMade);
        }
    }
}
