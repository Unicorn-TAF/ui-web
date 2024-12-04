using System.Collections.Generic;
using Unicorn.Taf.Core.Testing.Attributes;
using Unicorn.Taf.Core.Verification;
using Unicorn.Taf.Core.Verification.Matchers;
using Unicorn.UI.Core.Controls;
using Unicorn.UI.Core.Driver;
using Unicorn.UI.Web.Controls;
using Unicorn.UI.Web.Driver;

namespace Unicorn.UnitTests.UI.Tests.Web
{
    [Suite]
    public class WebSearchContextTests : WebTestsBase
    {
        private WebDriver WebDriver => DriverManager.Instance;

        [BeforeSuite]
        public void Setup()
        {
            WebDriver.Get("https://jqueryui.com/resources/demos/datepicker/inline.html");
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextFirstChild()
        {
            WebControl firstChild = WebDriver.FirstChild<WebControl>();
            Assert.That(firstChild.Instance.TagName, Is.EqualTo("html"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextFindIControl()
        {
            IControl firstChild = WebDriver.Find(ByLocator.Id("datepicker"));
            Assert.That(firstChild.GetAttribute("class"), Is.EqualTo("hasDatepicker"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextTryGetChildIControlPositive() =>
            Assert.IsTrue(WebDriver.TryGetChild(ByLocator.Id("datepicker")));

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextTryGetChildIControlNegative() =>
            Assert.IsFalse(WebDriver.TryGetChild(ByLocator.Css(".sdfgkjhasdflkjhsdf")));

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextFindById()
        {
            WebControl firstChild = WebDriver.Find<WebControl>(ByLocator.Id("datepicker"));
            Assert.That(firstChild.GetAttribute("class"), Is.EqualTo("hasDatepicker"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextFindByClass()
        {
            WebControl firstChild = WebDriver.Find<WebControl>(ByLocator.Class("hasDatepicker"));
            Assert.That(firstChild.GetAttribute("id"), Is.EqualTo("datepicker"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextFindByCss()
        {
            WebControl firstChild = WebDriver.Find<WebControl>(ByLocator.Css(".hasDatepicker"));
            Assert.That(firstChild.GetAttribute("id"), Is.EqualTo("datepicker"));
        }


        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextFindListByCss()
        {
            WebControl firstChild = WebDriver.Find<WebControl>(ByLocator.Css(".hasDatepicker"));
            IList<WebControl> controls = firstChild.FindList<WebControl>(ByLocator.Css(".ui-datepicker-calendar > thead th"));
            Assert.That(controls.Count, Is.EqualTo(7));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextFindByXpath()
        {
            WebControl firstChild = WebDriver.Find<WebControl>(ByLocator.Xpath("//table"));
            Assert.That(firstChild.GetAttribute("class"), Is.EqualTo("ui-datepicker-calendar"));
        }

        [Test]
        [Author("Vitaliy Dobriyan")]
        public void TestSearchContextFindByTag()
        {
            WebControl firstChild = WebDriver.Find<WebControl>(ByLocator.Tag("table"));
            Assert.That(firstChild.GetAttribute("class"), Is.EqualTo("ui-datepicker-calendar"));
        }
    }
}
