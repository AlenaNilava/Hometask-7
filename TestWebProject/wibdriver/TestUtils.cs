using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestWebProject.wibdriver
{
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
            catch (StaleElementReferenceException e)
            {
                new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(timeoutSecs)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
                new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(timeoutSecs)).Until(ExpectedConditions.ElementToBeClickable(element));
            }
        }
    }
}
