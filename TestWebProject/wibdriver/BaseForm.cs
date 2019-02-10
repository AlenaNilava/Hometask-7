using OpenQA.Selenium;

namespace TestWebProject.wibdriver
{
	public class BaseForm
	{
		protected By TitleLocator;
		protected string title;
		public static string titleForm;

		protected BaseForm(By TitleLocator, string title)
		{
			this.TitleLocator = TitleLocator;
			this.title = titleForm = title;
			AssertIsOpen();
		}

		public void AssertIsOpen()
		{
			var label = new BaseElement(this.TitleLocator, this.title);
			label.CheckForIsVisible();
		}

        public static string JsReturnTitle()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
            return (string)executor.ExecuteScript("return document.title");
        }
    }
}