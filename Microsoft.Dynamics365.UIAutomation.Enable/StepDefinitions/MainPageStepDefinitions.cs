using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.Security;

using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;

namespace Microsoft.Dynamics365.UIAutomation.Enable.StepDefinitions
{
    [Binding]
    public class MainPageStepDefinitions : BaseStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        // private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        // private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        // private readonly SecureString _username2 = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername2"].ToSecureString();
        // private readonly SecureString _password2 = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword2"].ToSecureString();
        // private readonly SecureString _mfaSecretKey = System.Configuration.ConfigurationManager.AppSettings["MfaSecretKey"].ToSecureString();
        private readonly Uri _xrmUri =new Uri("https://enanave04.ad.enable.net.nz/BC_TEST/");
        private readonly SecureString _username = "ENABLE\\ServiceTest01".ToSecureString();
        private readonly SecureString _password = "most baker ravel".ToSecureString();
        private readonly SecureString _username2 = "ENABLE\\ServiceTest02".ToSecureString();
        private readonly SecureString _password2 = "most baker rave".ToSecureString();
        private readonly SecureString _mfaSecretKey = "most baker rave".ToSecureString();



        //new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        public MainPageStepDefinitions(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"user is logged in")]
        [When(@"user logs in")]
        public void GivenUserIsLoggedIn()
        {
            XrmApp.OnlineLogin.Login(_xrmUri, _username, _password, _mfaSecretKey);
        }

        [Given(@"approver is logged in")]
        [When(@"approver logs in")]
        public void GivenApproverIsLoggedIn()
        {
            XrmApp.OnlineLogin.Login(_xrmUri, _username2, _password2, _mfaSecretKey);
        }

        [When(@"approver signs out")]
        [When(@"user signs out")]
        public void WhenUserSignsOut()
        {
            XrmApp.Navigation.BC_SignOut();
        }

        [StepDefinition(@"developer has time to see the result")]
        public void ThenDeveloperHasTimeToSeeTheResult()
        {
            XrmApp.ThinkTime(10000);
        }

        [StepDefinition(@"user gives time to the Business Central to process the last request")]
        public void WhenUserGivesTimeToTheBusinessCentralToProcessTheLastRequest()
        {
            XrmApp.ThinkTime(3000);
        }

        [Then(@"user sees a page error message")]
        public void ThenUserSeesAPageErrorMessage()
        {
            var PageErrorMessage = XrmApp.Navigation.BC_GetPageErrorMessage();
            PageErrorMessage.Should().MatchRegex("The page has .*? error.*");
        }

        [Then(@"user sees an incorrect status information")]
        public void ThenUserSeesAnIncorrectStatusInformation()
        {
            var DialogMessage = XrmApp.Entity.BC_GetDialogMessage();
            DialogMessage.Should().Contain("Status must be equal to 'Open'");
            XrmApp.Entity.BC_Dialog_ClickButton("OK");
        }

        [When(@"user refreshes the page")]
        public void WhenUserRefreshesThePage()
        {
            XrmApp.Navigation.BC_ClickRefreshLink();
        }

    }
}
