using System;
using TechTalk.SpecFlow;
using TestWebProject.Entities.Email;
using TestWebProject.Entities.User;
using TestWebProject.forms;
using TestWebProject.Utils;

namespace TestWebProject
{
    [Binding]
    [Scope(Feature = "TestInvalidPassword")]
    public class TestInvalidPasswordSteps : BasicSteps
    {
        [Then(@"Verify, that red text message appears for invalid login")]
        public void WhenVerifyThatRedTextMessageAppears()
        {
            homePage.WaitForValidationMessage(expectedValidationMessage);
        }

        [Given(@"Login to the mail\.ru as an invalid user")]
        public void InvalidLoginToTheMailRu()
        {
            homePage = new HomePage();
            homePage.Login(invalidUser);
        }
    }
}
