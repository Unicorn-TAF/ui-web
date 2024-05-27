using NUnit.Framework;
using Unicorn.UnitTests.UI.Gui.Web;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [TestFixture]
    public class WebPageTests : WebTestsBase
    {
        [Author("Vitaliy Dobriyan")]
        [Test(Description = "WebPage.Opened works for page with relative url only")]
        public void TestWebPageOpenedWorksForPageWithRelativeUrlOnly()
        {
            JqueryDataGridPage page = NavigateToPage<JqueryDataGridPage>();
            Assert.IsTrue(page.Opened);
        }

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "WebPage.Opened works for page with relative url and title")]
        public void TestWebPageOpenedWorksForPageWithRelativeUrlAndTitle()
        {
            JquerySelectPage page = NavigateToPage<JquerySelectPage>();
            Assert.IsTrue(page.Opened);
        }

        [Author("Vitaliy Dobriyan")]
        [Test(Description = "WebPage.Opened fails if page is not opened")]
        public void TestWebPageOpenedFailsIfPageIsNotOpened()
        {
            NavigateToPage<JquerySelectPage>();
            Assert.IsFalse(new JqueryDialogPage(DriverManager.Instance).Opened);
        }
    }
}
