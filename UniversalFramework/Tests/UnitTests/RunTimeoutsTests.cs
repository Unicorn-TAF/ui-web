﻿using System;
using System.Reflection;
using NUnit.Framework;
using ProjectSpecific;
using Unicorn.Core.Testing.Tests.Adapter;

namespace Tests.UnitTests
{
    [TestFixture]
    public class RunTimeoutsTests : NUnitTestRunner
    {
        [Author("Vitaliy Dobriyan")]
        [TestCase(Description = "Check Test timeout")]
        public void TestTimeoutsTestTimeout()
        {
            Configuration.SetSuiteFeatures("timeouts");
            Configuration.TestTimeout = TimeSpan.FromSeconds(2);
            TestsRunner runner = new TestsRunner(Assembly.GetExecutingAssembly(), false);
            runner.RunTests();

            Assert.That(runner.ExecutedSuites[0].Outcome.FailedTests, Is.EqualTo(1));
        }
    }
}
