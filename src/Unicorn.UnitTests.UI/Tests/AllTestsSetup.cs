using NUnit.Framework;

namespace Unicorn.UnitTests.UI.Tests
{
    [SetUpFixture]
    internal class AllTestsSetup
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            DriverManager.Instance.Close();
        }
    }
}
