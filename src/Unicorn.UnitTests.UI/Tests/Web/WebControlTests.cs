using NUnit.Framework;
using Unicorn.UnitTests.UI.Gui.Web;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [TestFixture]
    public class WebControlTests : WebTestsBase
    {
        private PrimitiveControlsPage page;

        [OneTimeSetUp]
        public void Setup() =>
            page = NavigateToPage<PrimitiveControlsPage>(true);

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestRightClick()
        {
            page.Button.RightClick();
            Assert.That(page.ButtonState.Text, Is.EqualTo("right"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestJsClick()
        {
            Assert.IsFalse(page.Checkbox1.Checked);
            page.Checkbox1.JsClick();
            Assert.IsTrue(page.Checkbox1.Checked);
        }
    }
}
