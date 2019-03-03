namespace TestWebProject.Utils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    public class BaseTest
	{
		protected static Browser Browser = Browser.GetInstance();

		[TestInitialize]
		public virtual void InitTest()
		{
			Browser = Browser.GetInstance();
			Browser.WindowMaximise();
			Browser.NavigateTo(Configuration.StartUrl);
		}

		[TestCleanup]
		public virtual void CleanTest()
		{
			Browser.Quit();
		}
	}
}