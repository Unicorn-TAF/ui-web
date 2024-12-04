using Unicorn.Taf.Core.Testing.Attributes;
using Unicorn.Taf.Core.Verification;
using Unicorn.Taf.Core.Verification.Matchers;
using Unicorn.UI.Core.PageObject;
using Unicorn.UI.Web.Controls.Typified;
using Unicorn.UnitTests.UI.Gui.Web;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [Suite]
    public class WebPageObjectTests : WebTestsBase
    {
        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestPageObjectControlsListProperty()
        {
            JquerySelectPage page = NavigateToPage<JquerySelectPage>();

            Assert.That(page.DropdownsList[0].GetAttribute("id"), Is.EqualTo("speed"));
            Assert.That(page.DropdownsList[1].GetAttribute("id"), Is.EqualTo("files"));
            Assert.That(page.DropdownsList[2].GetAttribute("id"), Is.EqualTo("number"));
            Assert.That(page.DropdownsList[3].GetAttribute("id"), Is.EqualTo("salutation"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestPageObjectControlsListField()
        {
            JquerySelectPage page = NavigateToPage<JquerySelectPage>();
            Assert.That(page.DropdownsListwithBackingField.Count, Is.EqualTo(4));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestPageObjectControlsListRefreshes()
        {
            JquerySelectPage page = NavigateToPage<JquerySelectPage>();
            Assert.That(page.DropdownsList.Count, Is.EqualTo(4));
            DriverManager.Instance.Get("https://jqueryui.com/resources/demos/selectmenu/custom_render.html");
            Assert.That(page.DropdownsList.Count, Is.EqualTo(3));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestPageObjectControlsSearch()
        {
            JqueryCheckboxRadioPage page = NavigateToPage<JqueryCheckboxRadioPage>();
            Assert.That(page.JqCheckbox, Is.OfType(typeof(Checkbox)));
            Assert.That(page.JqCheckbox.GetAttribute("name"), Is.EqualTo("checkbox-1"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestPageObjectNameAttributeExplicit()
        {
            JqueryCheckboxRadioPage page = NavigateToPage<JqueryCheckboxRadioPage>();
            Assert.That(page.JqRadio.Name, Is.EqualTo(JqueryCheckboxRadioPage.RadioButtonName));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestPageObjectImplicitName()
        {
            JqueryCheckboxRadioPage page = NavigateToPage<JqueryCheckboxRadioPage>();
            Assert.That(page.CheckboxCustom.Name, Is.EqualTo(CustomCheckbox.ControlName));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestPageObjectImplicitFindAndNestedControls()
        {
            JqueryCheckboxRadioPage page = NavigateToPage<JqueryCheckboxRadioPage>();
            Assert.IsTrue(page.CheckboxCustom.GetAttribute("class").Contains("ui-checkboxradio-label"));
            Assert.IsTrue(page.CheckboxCustom.Label.GetAttribute("class").Contains("ui-checkboxradio-icon"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestPageObjectImplicitFindAndNestedControlsInList()
        {
            JqueryCheckboxRadioPage page = NavigateToPage<JqueryCheckboxRadioPage>();
            Assert.IsTrue(page.CheckboxesCustomList[0].GetAttribute("class").Contains("ui-checkboxradio-label"));
            Assert.IsTrue(page.CheckboxesCustomList[0].Label.GetAttribute("class").Contains("ui-checkboxradio-icon"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestPageObjectExistsExtension()
        {
            JqueryCheckboxRadioPage page = NavigateToPage<JqueryCheckboxRadioPage>();
            Assert.IsTrue(page.JqRadio.ExistsInPageObject());
            Assert.IsFalse(page.NotExistingRadio.ExistsInPageObject());
        }
    }
}
