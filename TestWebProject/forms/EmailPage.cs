using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace TestWebProject.forms
{
    using TestWebProject.wibdriver;
    public class EmailPage : BaseForm
    {
        private static readonly By EmailToField = By.XPath("//textarea[@data-original-name='To']");
        private readonly BaseElement addresseeField = new BaseElement(EmailToField);
        private readonly BaseElement subjectField = new BaseElement(By.XPath("//input[@name='Subject']"));
        private readonly BaseElement bodyTextFrame = new BaseElement(By.XPath("//iframe"));
        private readonly BaseElement bodyTextField = new BaseElement(By.XPath("//*[@id='tinymce']"));
        private readonly BaseElement saveBtn = new BaseElement(By.XPath("(//div[@data-name='saveDraft'])[1]"));
        private readonly BaseElement draftEmailSavedLbl = new BaseElement(By.XPath("(//div[text()='Сохранено в '])[1]"));
        private readonly BaseElement sendBtn = new BaseElement(By.XPath("(//div[@data-name='send'])[1]"));
        private readonly BaseElement sentEmailAdressLbl = new BaseElement(By.XPath("(//span[text()='elenasinevich91@gmail.com'])[2]"));
        private readonly BaseElement sentEmailBodyTextLbl = new BaseElement(By.XPath("//body[@id='tinymce']//div[text()[contains(.,'Test Text')]]"));

        public EmailPage() : base(EmailToField, "Email Page")
        {
        }
        
        public void CreateDraftEmail(string address, string subject, string expectedTestBody)
        {
            addresseeField.SendKeys(address);
            subjectField.SendKeys(subject);
            Browser.GetDriver().SwitchTo().Frame(this.bodyTextFrame.GetElement());
            bodyTextField.SendKeys(expectedTestBody);
            Browser.GetDriver().SwitchTo().DefaultContent();
            saveBtn.Click();
            draftEmailSavedLbl.CheckForIsVisible();
        }

        public void CheckEmailFields(string address, string subject, string expectedTestBody)
        {
            //Verify the draft addressee is still the same
            Assert.AreEqual(address, sentEmailAdressLbl.GetText(), "Email is not actual");

            //Verify the draft subject is still the same
            Assert.AreEqual(subject, subjectField.GetAttribute("value"), "Subject is not actual");

            //Verify the draft body text is still the same
            Browser.GetDriver().SwitchTo().Frame(this.bodyTextFrame.GetElement());
            Assert.IsTrue(sentEmailBodyTextLbl.GetText().Contains(expectedTestBody), "Body text is not as expected"); 
            Browser.GetDriver().SwitchTo().DefaultContent();
        }

        public void SendEmail()
        {
            sendBtn.Click();
        }
    }
}
