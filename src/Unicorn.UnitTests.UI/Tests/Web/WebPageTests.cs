using Unicorn.Taf.Core.Testing.Attributes;
using Unicorn.Taf.Core.Verification;
using Unicorn.UnitTests.UI.Gui.Web;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [Suite]
    public class WebPageTests : WebTestsBase
    {
        [Author("Vitaliy Dobriyan")]
        [Test("WebPage.Opened works for page with relative url only")]
        public void TestWebPageOpenedWorksForPageWithRelativeUrlOnly()
        {
            JqueryDataGridPage page = NavigateToPage<JqueryDataGridPage>();
            Assert.IsTrue(page.Opened);
        }

        [Author("Vitaliy Dobriyan")]
        [Test("WebPage.Opened works for page with relative url and title")]
        public void TestWebPageOpenedWorksForPageWithRelativeUrlAndTitle()
        {
            JquerySelectPage page = NavigateToPage<JquerySelectPage>();
            Assert.IsTrue(page.Opened);
        }

        [Author("Vitaliy Dobriyan")]
        [Test("WebPage.Opened fails if page is not opened")]
        public void TestWebPageOpenedFailsIfPageIsNotOpened()
        {
            NavigateToPage<JquerySelectPage>();
            Assert.IsFalse(new JqueryDialogPage(DriverManager.Instance).Opened);
        }
    }
}
