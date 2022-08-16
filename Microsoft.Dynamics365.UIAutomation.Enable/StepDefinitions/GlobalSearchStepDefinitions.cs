using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.Security;

using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;

namespace Microsoft.Dynamics365.UIAutomation.Enable.StepDefinitions
{
    [Binding]
    public class GlobalSearchStepDefinitions : BaseStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        public GlobalSearchStepDefinitions(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [StepDefinition(@"user searches for '([^']*)'")]
        public void GivenUserSearchesFor(string searchValue)
        {
            XrmApp.GlobalSearch.BC_SearchForPage(searchValue);
        }
    }
}
