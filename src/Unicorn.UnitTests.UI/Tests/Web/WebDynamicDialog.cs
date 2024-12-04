using Unicorn.Taf.Core.Testing.Attributes;
using Unicorn.Taf.Core.Verification;
using Unicorn.Taf.Core.Verification.Matchers;
using Unicorn.UnitTests.UI.Gui.Web;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [Suite]
    public class WebDynamicDialog : WebTestsBase
    {
        private JqueryDialogPage page;

        [BeforeTest]
        public void PreparePage()
        {
            page = NavigateToPage<JqueryDialogPage>(true);
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check dialog title")]
        public void TestDialogTitle() =>
            Assert.That(page.Dialog.Title, Is.EqualTo("Empty the recycle bin?"));
        
        [Author("Vitaliy Dobriyan")]
        [Test("Check dialog content")]
        public void TestDialogContent() =>
            Assert.That(page.Dialog.TextContent, Is.EqualTo("These items will be permanently deleted and cannot be recovered. Are you sure?"));

        [Author("Vitaliy Dobriyan")]
        [Test("Check dialog close")]
        public void TestDialogClose()
        {
            page.Dialog.Close();
            Assert.IsTrue(page.Dialog.GetAttribute("style").Contains("display: none;"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check dialog acceptance")]
        public void TestDialogAcceptance()
        {
            page.Dialog.Accept();
            Assert.IsTrue(page.Dialog.GetAttribute("style").Contains("display: none;"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check dialog declining")]
        public void TestDialogDeclining()
        {
            page.Dialog.Decline();
            Assert.IsTrue(page.Dialog.GetAttribute("style").Contains("display: none;"));
        }

        [Author("Vitaliy Dobriyan")]
        [Test("Check dialog click button by name")]
        public void TestDialogClickButtonByName()
        {
            page.Dialog.ClickButton("Delete all items");
            Assert.IsTrue(page.Dialog.GetAttribute("style").Contains("display: none;"));
        }
    }
}
