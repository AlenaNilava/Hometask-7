using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestWebProject.forms;
using TestWebProject.wibdriver;

namespace TestWebProject
{
	[TestClass]
    public class HappyPathTest:BaseTest
	{
        //Test data
        readonly string login = "testmail.2020";
        readonly string password = "Asas432111";
        readonly string invalidPassword = "asas432111";
        readonly string address = "elenasinevich91@gmail.com";
        readonly string subject = $"Test Mail {TestUtils.GetRandomSubjectNumber()}";
        readonly string expectedTestBody = "Test Text";

        [TestMethod]
		public void TestSmokeEmail()
		{
            //Login to the mail.ru
            HomePage homePage = new HomePage();
            homePage.Login(login, password);

            //Assert, that the login is successful
            InboxPage inboxPage = new InboxPage();
            inboxPage.ClickCreate();

            //Create a new mail 
            EmailPage emailPage = new EmailPage();
            emailPage.CreateDraftEmail(address, subject, expectedTestBody);

            //Navigate to DraftsPage
            NavigationMenu navigationMenu = new NavigationMenu();
            DraftsPage draftsPage = navigationMenu.NavigateToDrafts();

            //Verify, that the mail presents in ‘Drafts’ folder. 
            draftsPage.ClickDraftEmail(subject);

            //Verify the draft content (addressee, subject and body – should be the same) 
            emailPage = new EmailPage();
            emailPage.CheckEmailFields(address, subject, expectedTestBody);

            //Send the mail
            emailPage.SendEmail();

            //Navigate to DraftsPage and verify, that the mail disappeared from ‘Drafts’ folder
            draftsPage = navigationMenu.NavigateToDrafts();
            draftsPage.CheckDisappearedEmail(subject);

            //Navigate to SentPage
            SentPage sentPage = navigationMenu.NavigateToSent();

            //Verify, that the mail presents in ‘Sent’ folder. 
            sentPage.CheckSentEmail(subject);

            //Click Context menu of the first email
            sentPage.EmailContextClick(subject);

            //Log out
            homePage = navigationMenu.LogOut();
        }

        [TestMethod]
        public void TestDeleteEmail()
        {
            //Login to the mail.ru
            HomePage homePage = new HomePage();
            homePage.Login(login, password);

            //Assert, that the login is successful
            InboxPage inboxPage = new InboxPage();
            inboxPage.ClickCreate();

            //Create a new mail 
            EmailPage emailPage = new EmailPage();
            emailPage.CreateDraftEmail(address, subject, expectedTestBody);

            //Send the mail
            emailPage.SendEmail();

            //Navigate to SentPage
            NavigationMenu navigationMenu = new NavigationMenu();
            SentPage sentPage = navigationMenu.NavigateToSent();

            //Verify, that the mail presents in ‘Sent’ folder. 
            sentPage.CheckSentEmail(subject);

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
            string expectedValidationMessage = "Неверное имя или пароль";

            //Login to the mail.ru with invalid password
            HomePage homePage = new HomePage();
            homePage.Login(login, invalidPassword);

            //Verify, that red text message appears
            homePage.CheckValidationMessage(expectedValidationMessage);

            //Login to the mail.ru 
            homePage.Login(login, password);
            InboxPage inboxPage = new InboxPage();
        }
    }
}
