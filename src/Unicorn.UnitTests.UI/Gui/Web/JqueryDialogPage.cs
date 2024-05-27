using Unicorn.UI.Core.Controls.Dynamic;
using Unicorn.UI.Core.Driver;
using Unicorn.UI.Core.PageObject;
using Unicorn.UI.Web.Controls.Dynamic;
using Unicorn.UI.Web.Driver;
using Unicorn.UI.Web.PageObject;
using Unicorn.UI.Web.PageObject.Attributes;

namespace Unicorn.UnitTests.UI.Gui.Web
{
    [PageInfo("dialog-modal-confirmation.html")]
    public class JqueryDialogPage : WebPage
    {
        public JqueryDialogPage(WebDriver driver) : base(driver)
        {
        }

        [Find(Using.WebCss, "[aria-describedby = 'dialog-confirm']")]
        [DefineDialog(DialogElement.Title, Using.WebCss, ".ui-dialog-title")]
        [DefineDialog(DialogElement.Content, Using.Id, "dialog-confirm")]
        [DefineDialog(DialogElement.Accept, Using.WebXpath, ".//button[. = 'Delete all items']")]
        [DefineDialog(DialogElement.Decline, Using.WebXpath, ".//button[. = 'Cancel']")]
        [DefineDialog(DialogElement.Close, Using.WebCss, ".ui-dialog-titlebar-close")]
        public DynamicDialog Dialog { get; set; }

        [Find(Using.WebCss, ".demo-frame")]
        public DynamicDialog Frame { get; set; }
    }
}
