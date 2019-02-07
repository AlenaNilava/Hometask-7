﻿using OpenQA.Selenium;
using System;

namespace TestWebProject.forms
{
    using TestWebProject.wibdriver;
    public class DraftsPage : BaseForm
    {
        private static readonly By DraftsLbl = By.XPath("//a[@data-mnemo='drafts']");

        public DraftsPage() : base(DraftsLbl, "Drafts Page")
        {
        }

        string draftEmailLblTemplate = "(//div[text()='{0}'])[1]";
        string sentEmailLblTemplate = "(//a[@data-subject='{0}'])[1]";

        public void ClickDraftEmail(string subject)
        {
            new BaseElement(By.XPath(String.Format(draftEmailLblTemplate, subject))).Click();
        }

        public void CheckDisappearedEmail(string subject)
        {
            new BaseElement(By.XPath(String.Format(sentEmailLblTemplate, subject))).WaitForNotVisible();
        }

    }
}
