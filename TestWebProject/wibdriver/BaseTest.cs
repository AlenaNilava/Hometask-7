namespace TestWebProject.Utils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;

    [Binding]
    public class BaseTest
	{
		protected static Browser Browser = Browser.GetInstance();

		[BeforeFeature]
		public static void InitTest()
		{
			Browser = Browser.GetInstance();
			Browser.WindowMaximise();
			Browser.NavigateTo(Configuration.StartUrl);
		}

		[AfterFeature]
		public static void CleanTest()
		{
			Browser.Quit();
		}
	}
}