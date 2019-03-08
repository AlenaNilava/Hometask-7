namespace TestWebProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestWebProject.Entities.Email;
    using TestWebProject.Entities.User;
    using TestWebProject.forms;
    using TestWebProject.Utils;
    public class BasicSteps
    {
        public string login;
        public string password;
        public string address;
        public string expectedTestBody;
        public string subject;
        public string expectedValidationMessage;

        public UserCreator usercreator;
        public Email email;
        public User user;
        public User invalidUser;

        public HomePage homePage;
        public InboxPage inboxPage;

        [Given("Set test data")]
        public void SetTestDataForTest()
        {
            login = "testmail.2020";
            password = "Asas432111";
            address = "elenasinevich91@gmail.com";
            subject = $"Test Mail {TestUtils.GetRandomSubjectNumber()}";
            expectedTestBody = "Test Text";

            usercreator = new ValidUserCreator();
            email = new EmptyEmail(address, subject, expectedTestBody);

            SetInvlidTestData();
        }

        [Given(@"Login to the mail\.ru as a valid user")]
        public void LoginToTheMailRu()
        {
            homePage = new HomePage();
            inboxPage = homePage.Login(user);
        }

        [Then(@"I verify that user is signed in")]
        public void VerifySignedUser()
        {
            Assert.IsTrue(inboxPage.IsSucessfullyLoggedIn(), "User is not logged in");
        }

        [Given("Set login test data")]
        public void SetInvlidTestData()
        {
            user = usercreator.Create(login, password);
            usercreator = new InvalidUserCreator();
            invalidUser = usercreator.Create(login, password);

            expectedValidationMessage = "Неверное имя или пароль";
        }
    }
}
