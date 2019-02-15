using System;
using OpenQA.Selenium;

namespace TestWebProject.forms
{
    using TestWebProject.wibdriver;
    using OpenQA.Selenium.Interactions;
    public class RecycleBinPage : BaseForm
    {
        private static readonly By RecycleLbl = By.XPath("//div[@data-cache-key='500002_undefined_false']//div[@class='b-informer__text']");
        static string deletedEmailLblTemplate = "(//div[@data-cache-key='500002_undefined_false']//a[@data-subject='{0}'])[1]";

        public RecycleBinPage() : base(RecycleLbl, "Recycle Bin Page")
        {
        }
        
        public void CheckDeletedEmail(string subject)
        {
            new BaseElement(By.XPath(String.Format(deletedEmailLblTemplate, subject))).WaitForElementIsVisible();
        }
    }
}
