namespace TestWebProject.Utils
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;
    using OpenQA.Selenium.Support.UI;
    using System;

    public class TestUtils
    {
        public static string GetRandomSubjectNumber()
        {
            return Guid.NewGuid().ToString("N");
        }

        //catch Stale exception due elements loading
        public static void WaitElementAvailable(By element, int timeoutSecs = 10)
        {
            try
            {
                new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(timeoutSecs)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
                new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(timeoutSecs)).Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (StaleElementReferenceException)
            {
                new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(timeoutSecs)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
                new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(timeoutSecs)).Until(ExpectedConditions.ElementToBeClickable(element));
            }
        }

        public static void TakeScreenshot()
        {
            Browser.GetDriver().TakeScreenshot().SaveAsFile(GetRandomSubjectNumber() + ".Jpeg", ScreenshotImageFormat.Jpeg);
        }
    }
}
