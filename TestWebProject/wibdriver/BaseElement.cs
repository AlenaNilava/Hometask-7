using System;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestWebProject.wibdriver
{
	public class BaseElement:IWebElement
	{
		protected string Name;
		protected By Locator;
		protected IWebElement Element;

		public BaseElement(By locator, string name)
		{
			this.Locator = locator;
			this.Name = name == "" ? this.GetText() : name;
		}

		public BaseElement(By locator)
		{
			this.Locator = locator;
		}

		public string GetText()
		{
			this.WaitForIsVisible();
			return Browser.GetDriver().FindElement(this.Locator).Text;
		}

		public IWebElement GetElement()
		{
			try
			{
				this.Element = Browser.GetDriver().FindElement(this.Locator);
			}
			catch (Exception)
			{
				
				throw;
			}
			return this.Element;
		}

		public void WaitForIsVisible()
		{
			new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElement)).Until(ExpectedConditions.ElementIsVisible(this.Locator));
            JSHighlight();
		}

        public void WaitForNotVisible()
        {
            new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(Browser.TimeoutForElement)).Until(ExpectedConditions.InvisibilityOfElementLocated(this.Locator));
        }

        public IWebElement FindElement(By @by)
		{
			throw new System.NotImplementedException();
		}

		public ReadOnlyCollection<IWebElement> FindElements(By @by)
		{
			throw new System.NotImplementedException();
		}

		public void Clear()
		{
            WaitForIsVisible();
            Browser.GetDriver().FindElement(this.Locator).Clear();
        }

		public void SendKeys(string text)
		{
            WaitForIsVisible();
            Browser.GetDriver().FindElement(this.Locator).SendKeys(text);
        }

		public void Submit()
		{
			throw new System.NotImplementedException();
		}

		public void Click()
		{
			this.WaitForIsVisible();
            TestUtils.WaitElementAvailable(this.Locator);
			Browser.GetDriver().FindElement(this.Locator).Click();
		}

		public void JsClick()
		{
			this.WaitForIsVisible();
			IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
			executor.ExecuteScript("arguments[0].click();", this.GetElement());
		}

        //Added JSHighlight method for web elements
        public void JSHighlight()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
            try
            {
                executor.ExecuteScript(highlightJavascript, this.GetElement());
            }
            catch
            {
            }
        }

		public string GetAttribute(string attributeName)
		{
            return Browser.GetDriver().FindElement(this.Locator).GetAttribute(attributeName);
        }

        public void DragAndDrop(IWebElement target)
        {
            Actions actions = new Actions(Browser.GetDriver());
            actions.DragAndDrop(Browser.GetDriver().FindElement(this.Locator), target);
            actions.Build().Perform();
        }

        public void ContextClick()
        {
            Actions actions = new Actions(Browser.GetDriver());
            actions.ContextClick(Browser.GetDriver().FindElement(this.Locator));
            actions.Build().Perform();
        }

		public string GetCssValue(string propertyName)
		{
			throw new System.NotImplementedException();
		}

		public string GetProperty(string propertyName)
		{
			throw new NotImplementedException();
		}

		public string TagName { get; }
		public string Text { get; }
		public bool Enabled { get; }
		public bool Selected { get; }
		public Point Location { get; }
		public Size Size { get; }
		public bool Displayed { get; }
	}
}