namespace TestWebProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestWebProject.Entities.Email;
    using TestWebProject.Entities.User;
    using TestWebProject.forms;
    using TestWebProject.Utils;

    [TestClass]
    public class HappyPathTest : BaseTest
    {
        //Test data
        const string login = "testmail.2020";
        const string password = "Asas432111";
        const string address = "elenasinevich91@gmail.com";
        static readonly string subject = $"Test Mail {TestUtils.GetRandomSubjectNumber()}";
        const string expectedTestBody = "Test Text";

        UserCreator usercreator = new ValidUserCreator();
        Email email = new EmptyEmail(address, subject, expectedTestBody);

        [TestMethod]
        public void TestSmokeEmail()
        {
            User user = usercreator.Create(login, password);

            //Login to the mail.ru
            HomePage homePage = new HomePage();
            InboxPage inboxPage = homePage.Login(user);

            //Assert a user is logged in
            Assert.IsTrue(inboxPage.IsSucessfullyLoggedIn(), "User is not logged in");

            //Create a new mail 
            EmailPage emailPage = inboxPage.ClickCreateNewMessageButton();
            email = new DraftEmail(email);

            //Navigate to DraftsPage
            NavigationMenu navigationMenu = new NavigationMenu();
            DraftsPage draftsPage = navigationMenu.NavigateToDrafts();

            //Open Draft Email on DraftsPage
            emailPage = draftsPage.ClickDraftEmail(email);

            //Verify the draft content (addressee, subject and body – should be the same) 
            Assert.IsTrue(emailPage.GetAddress().Equals(address), "Address is wrong.");
            Assert.IsTrue(emailPage.GetSubject().Equals(email.subject), "Message subject doesn't match");
            Assert.IsTrue(emailPage.GetMessage().Contains(expectedTestBody), "Message is incorrect.");

            //Send the mail
            emailPage.ClickSendEmailButton();

            // Verify the email is sent message
            //Assert.IsTrue(emailPage.GetVerificationMessage().Contains(ExpectedMessage));

            //Navigate to DraftsPage and verify, that the mail disappeared from ‘Drafts’ folder
            draftsPage = navigationMenu.NavigateToDrafts();
            draftsPage.WaitForEmailDisappearedBySubject(email.subject);
            Assert.IsFalse(draftsPage.IsEmailPresentBySubject(email.subject));

            //Navigate to SentPage
            SentPage sentPage = navigationMenu.NavigateToSent();

            //Verify, that the mail presents in ‘Sent’ folder. 
            sentPage.WaitForEmailinSentFolder(subject);
            Assert.IsTrue(sentPage.IsEmailPresentBySubject(email.subject));

            //Log out
            navigationMenu.LogOut();
        }

        [TestMethod]
        public void TestDeleteEmail()
        {
            User user = usercreator.Create(login, password);

            //Login to the mail.ru
            HomePage homePage = new HomePage();
            homePage.Login(user);

            //Assert, that the login is successful
            InboxPage inboxPage = new InboxPage();
            inboxPage.ClickCreateNewMessageButton();

            //Create a new mail 
            EmailPage emailPage = new EmailPage();
            emailPage.CreateDraftEmail(email);

            //Send the mail
            emailPage.ClickSendEmailButton();

            //Navigate to SentPage
            NavigationMenu navigationMenu = new NavigationMenu();
            SentPage sentPage = navigationMenu.NavigateToSent();

            //Verify, that the mail presents in ‘Sent’ folder. 
            sentPage.WaitForEmailinSentFolder(subject);

            //Delete the mail from Sent folder
            //sentPage.DeleteEmail(subject);

            //Delete email dragging to the trash bin
            sentPage.DragEmailToTrashBin(subject);

            //Navigate to recycle bin
            RecycleBinPage recyclePage = navigationMenu.NavigateToRecycle();

            //Verify, that the mail presents in ‘Recycle bin’ folder. 
            recyclePage.CheckDeletedEmail(subject);
        }

        [TestMethod]
        public void TestInvalidLogin()
        {
            User user = usercreator.Create(login, password);
            usercreator = new InvalidUserCreator();
            User invalidUser = usercreator.Create(login, password);

            string expectedValidationMessage = "Неверное имя или пароль";

            //Login to the mail.ru with invalid password
            HomePage homePage = new HomePage();
            homePage.Login(invalidUser);

            //Verify, that red text message appears
            homePage.CheckValidationMessage(expectedValidationMessage);

            //Login to the mail.ru 
            homePage.Login(user);
        }
    }
}
