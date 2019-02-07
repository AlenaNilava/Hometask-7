using OpenQA.Selenium;
using System;

namespace TestWebProject.forms
{
    using TestWebProject.wibdriver;
    class SentPage : BaseForm
    {
        private static readonly By SentLbl = By.XPath("//span[text()='Отправленные']");

        public SentPage() : base(SentLbl, "Sent Page")
        {
        }

        static string sentEmailLblTemplate = "(//a[@data-subject='{0}'])[1]";
        readonly string sentEmailCheckboxTemplate = $"{sentEmailLblTemplate}//div[@class='b-checkbox__box']";
        private readonly BaseElement DeleteBtn = new BaseElement(By.XPath("(//div[@data-cache-key='500000_undefined_false']//span[text()='Удалить'])[1]"));
        private readonly BaseElement recycleBinMenuItem = new BaseElement(By.XPath("//span[text()='Корзина']"));

        public void CheckSentEmail(string subject)
        {
            new BaseElement(By.XPath(String.Format(sentEmailLblTemplate, subject))).WaitForIsVisible();
        }

        public void DeleteEmail(string subject)
        {
            new BaseElement(By.XPath(String.Format(sentEmailCheckboxTemplate, subject))).Click();
            DeleteBtn.JsClick();
        }

        public void DragEmailToTrashBin(string subject)
        {
            new BaseElement(By.XPath(String.Format(sentEmailLblTemplate, subject))).DragAndDrop(recycleBinMenuItem.GetElement());
        }

        public void EmailContextClick(string subject)
        {
            new BaseElement(By.XPath(String.Format(sentEmailLblTemplate, subject))).ContextClick();
        }

    }
}
