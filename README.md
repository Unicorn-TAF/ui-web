![Nuget](https://img.shields.io/nuget/v/Unicorn.UI.Web?style=plastic) ![Nuget](https://img.shields.io/nuget/dt/Unicorn.UI.Web?style=plastic)

# Unicorn.UI.Web

Implementation of browser interaction based on Selenium.

* Web Driver implementation
* Typified controls implementations
* PageObject implementation
* Abstract WebSite and pages pool

PageObject example
```csharp
// Example of base web page.
// Page object supports inheritance, so all derived classes initialize controls described in base class also.
// Any page should be derived from WebPage
// Page properties like relative url and title could be specified using PageInfoAttribute
// Url property is implicitly used to navigate to the page from WebSite instance.
// Title property is implicitly used in page Opened property indicating 
[PageInfo("test-ui-apps.html", "Test UI apps | Unicorn.TAF")]
public class ExamplePage : WebPage
{
    // Creating page instance with <see cref="WebDriver"/> context.
    public ExamplePage(WebDriver driver) : base(driver)
    {
    }

    // Page object controls could either class properties or class fields (properties should have a setter).
    [Name("Page title")]
    [Find(Using.WebCss, "section[style *= block] h1.heading-separator")]
    public WebControl MainTitle { get; set; }

    // Each page object control could have readable name specified through NameAttribute.
    // This generate human friendly ToString for the control and makes reports and logs more readable.
    // Besides generic FindAttribute there are simlified shortcuts for locators:
    // ByIdAttribute, ByClassAttribute, ByNameAttribute
    [Name("Page header")]
    [ById("hero")]
    public WebControl Header { get; set; }

    // If a control has the same locator across all the places, then the locator and the name could be 
    // specified for the control type using the same FindAttribute and NameAttribute.
    // In such case the control also will be initialized with page object.
    public ModalWindow Modal { get; set; }

    public void WaitForLoading() =>
        Header.Wait(Until.Visible, Timeouts.PageLoadTimeout);
}
```

Custom website implementation example
```csharp
// Describes specific website implementation.
// The base website gives access to underlying WebDriver instance, provides with pages cache 
// mechanism (to avoid page creation each time) and eases pages navigation. 
// Some site specific actions and helpers could be placed here.
// The website should inherit WebSite
public class TestWebsite : WebSite
{
    // Website creation based on existing explicitly created WebDriver instance.
    // Should call base constructor.
    public TestWebsite(WebDriver driver, string siteUrl) : base(driver, siteUrl)
    {
    }

    // Website creation based on retrieved type of browser (WebDriver in this case is created automatically).
    // Should call base constructor.
    public TestWebsite(BrowserType browser, string siteUrl) : base(browser, siteUrl)
    {
    }
}
```

Page Pool use example
```csharp
TestWebsite website = new TestWebsite(....);

// All you need to create a new website page is to call 
ExamplePage page = website.GetPage<ExamplePage>();
// if the page was already created for current webdriver, existing instance will be returned
// otherwise new instance will be created

// To navigate directly to the page by it's url it's enough to call
ExamplePage page = website.NavigateTo<ExamplePage>();
```

Built-in matchers
```csharp
ExamplePage page = website.GetPage<ExamplePage>();
Assert.That(page.MainTitle, UI.Control.HasText("'Hello World' app"))
Assert.That(page.TitleDropdown, UI.Dropdown.HasSelectedValue("Nothing selected"))
Assert.That(page.NameInput, UI.TextInput.HasValue(string.Empty))
```