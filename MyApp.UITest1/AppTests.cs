using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MyApp.UITest1
{
    [TestFixture(Platform.Android)]
   // [TestFixture(Platform.iOS)]
    public class AppTests
    {
        public AppTests(Platform platform)
        {
            SetupHooks.Platform = platform;
        }
        [SetUp]
        public void BeforeEachTest()
        {
            SetupHooks.App =
            AppInitializer.StartApp(SetupHooks.Platform, false);
        }

        //[Test]
        //public void CreditCardNumber_TooLong_DisplayErrorMessage()
        //{
        //    SetupHooks.App.Repl();
        //}

        [Test]
        public void StoresPageIsDisplayedTest()
        {
            AppResult[] storesPageExists = SetupHooks.App.WaitForElement(c => c.Marked("StoresPage"));
            Assert.IsTrue(storesPageExists.Any());
        }

        [Test]
        public void OrderValidateTest()
        {
            var amount = "100";
            SetupHooks.App.EnterText(c => c.Marked("entryOrderAmount"), amount);

            SetupHooks.App.Tap(c => c.Marked("Button_SubmitOrder"));
            AppResult[] results = SetupHooks.App.WaitForElement(c => c.Marked("Button_CompleteOrder"));
            Assert.IsTrue(results.Any());
        }


    }
}
