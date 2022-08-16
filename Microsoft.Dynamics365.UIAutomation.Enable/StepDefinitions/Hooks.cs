using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using TechTalk.SpecFlow.Infrastructure;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using System.Security;
using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Microsoft.Dynamics365.UIAutomation.Enable.StepDefinitions
{
    [Binding]
    public class Hooks : BaseStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public Hooks(ScenarioContext scenarioContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}] SCENARIO: '{_scenarioContext.ScenarioInfo.Title}' started.");
            WebClient client = new WebClient(TestSettings.Options);
            XrmApp = new XrmApp(client);
        }

        [BeforeStep]
        public void BeforeStep()
        {
        }

        [AfterStep]
        public void AfterStep()
        {
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TestError != null || TestSettings.TakeScreenshotIfTestPassed)
            {
                string ScreenshotFullPath = XrmApp.TakeScreenshot(_scenarioContext.ScenarioInfo.Title);
                _specFlowOutputHelper.AddAttachment(ScreenshotFullPath);
            }

            if (_scenarioContext.TestError != null)
            {
                Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}] SCENARIO: '{_scenarioContext.ScenarioInfo.Title}' ended with  error: {_scenarioContext.TestError.Message}");
                Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}] SCENARIO: '{_scenarioContext.ScenarioInfo.Title}' FAILED.\n");
            }
            else
            {
                Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}] SCENARIO: '{_scenarioContext.ScenarioInfo.Title}' PASSED.\n");
            }
            XrmApp.Dispose();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            XrmApp.Kill();
        }
    }
}
