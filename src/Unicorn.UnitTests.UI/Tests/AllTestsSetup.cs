using Unicorn.Taf.Core.Testing.Attributes;

namespace Unicorn.UnitTests.UI.Tests
{
    [TestAssembly]
    internal static class AllTestsSetup
    {
        [RunFinalize]
        public static void GlobalTeardown() =>
            DriverManager.Instance.Close();
    }
}
