namespace TestWebProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestWebProject.Entities.Email;
    using TestWebProject.forms;

    [Binding]
    class EmailSteps: BasicSteps
    {
        EmailPage emailPage;
        NavigationMenu navigationMenu;
        SentPage sentPage;
        RecycleBinPage recyclePage;
        DraftsPage draftsPage;

       [When(@"I create a mail")]
        public void CreateMail()
        {
            emailPage = inboxPage.ClickCreateNewMessageButton();
            email = new DraftEmail(email);
        }

        [When(@"I send the mail")]
        public void SendMail() => emailPage.ClickSendEmailButton();

        [When(@"I navigate to SentPage")]
        public void NavigateToSentPage()
        {
            navigationMenu = new NavigationMenu();
            sentPage = navigationMenu.NavigateToSent();
        }

        [When(@"I verify, that the mail presents in ‘Sent’ folder")]
        public void MailPresentsInSent()
        {
            sentPage.WaitForEmailinSentFolder(subject);
            Assert.IsTrue(sentPage.IsEmailPresentBySubject(email.subject));
        }

        [When(@"I delete email dragging to the trash bin")]
        public void DeleteEmail() => sentPage.DragEmailToTrashBin(subject);

        [When(@"I navigate to recycle bin")]
        public void NavigateToRecycleBin() => recyclePage = navigationMenu.NavigateToRecycle();

        [Then(@"Verify, that the mail presents in ‘Recycle bin’ folder")]
        public void MailPresentsInRecycleBin() => recyclePage.WaitForDeletedEmail(subject);

        [When(@"I navigate to DraftsPage")]
        public void NavigateToDraftPage()
        {
            navigationMenu = new NavigationMenu();
            draftsPage = navigationMenu.NavigateToDrafts();
        }

        [When(@"I open Draft Email on DraftsPage")]
        public void OpenDraftEmail() => emailPage = draftsPage.ClickDraftEmail(email);

        [Then(@"Verify the draft content addressee, subject and body – should be the same")]
        public void VerifyDraftContent()
        {
            Assert.IsTrue(emailPage.GetAddress().Equals(address), "Address is wrong.");
            Assert.IsTrue(emailPage.GetSubject().Equals(email.subject), "Message subject doesn't match");
            Assert.IsTrue(emailPage.GetMessage().Contains(expectedTestBody), "Message is incorrect.");
        }

        [When(@"I navigate to DraftsPage and verify, that the mail disappeared from ‘Drafts’ folder")]
        public void VerifyMailIsDissapeared()
        {
            draftsPage = navigationMenu.NavigateToDrafts();
            draftsPage.WaitForEmailDisappearedBySubject(email.subject);
            Assert.IsFalse(draftsPage.IsEmailPresentBySubject(email.subject));
        }

        [Then(@"I log out")]
        public void LogOutFromApp() => navigationMenu.LogOut();


    }
}
