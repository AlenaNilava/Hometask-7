using OpenQA.Selenium;

namespace TestWebProject.forms
{
    using TestWebProject.wibdriver;
    class EmailSentPage : BaseForm
    {
        private static readonly By SentLbl = By.XPath("(//div[@class='message-sent__title'])[1]");
        public EmailSentPage() : base(SentLbl, "Email Sent Page")
        {
        }
    }
}
