using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace TestWebProject.forms
{
    using TestWebProject.wibdriver;
    public class HomePage : BaseForm
    {
        private static readonly By HomeBtn = By.XPath("//*[@id='mailbox:submit']");

        public HomePage() : base(HomeBtn, JsReturnTitle()) //JSReturnTitle method was added
        {
        }

        private readonly BaseElement submitBtn = new BaseElement(HomeBtn);
        private readonly BaseElement loginField = new BaseElement(By.Id("mailbox:login"));
        private readonly BaseElement passwordField = new BaseElement(By.Id("mailbox:password"));
        private readonly BaseElement validationMessage = new BaseElement(By.Id("mailbox:error"));

        public void Login(string login, string password)
        {
            loginField.Clear();
            loginField.SendKeys(login);
            passwordField.Clear();
            passwordField.SendKeys(password);
            submitBtn.Click();
        }

        public void CheckValidationMessage(string expectedValidationMessage)
        {
            Assert.IsTrue(validationMessage.GetText().Contains(expectedValidationMessage), "Validation text is not as expected");
        }

    }
}

