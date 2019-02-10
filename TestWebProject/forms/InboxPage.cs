using OpenQA.Selenium;

namespace TestWebProject.forms
{
    using TestWebProject.wibdriver;
    public class InboxPage : BaseForm
    {
        private static readonly By InboxCreateBtn = By.XPath("(//span[text()='Написать письмо'])[1]");

        public InboxPage() : base(InboxCreateBtn, "Inbox Page")
        {
        }

        private readonly BaseElement createBtn = new BaseElement(InboxCreateBtn);

        public void ClickCreate()
        {
            createBtn.Click();
        }
    }
}
