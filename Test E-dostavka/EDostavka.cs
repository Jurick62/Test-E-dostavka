using NUnit.Framework;
using Test_E_dostavka.Tests;
using Test_E_dostavka.WrapperFactory;

namespace SeleniumTest_1
{
    [TestFixture]
    public class Test
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ReadConfigValue readConfig = new ReadConfigValue();
            readConfig.FindeConfigFile();
            BrowserFactory.InitBrowser(readConfig.BrowserName);
            BrowserFactory.LoadApplication(readConfig.EdostavkaURL);
            MainPageTests mainPageTests = new MainPageTests();
            mainPageTests.ClickLoginButton();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            BrowserFactory.CloseAllDrivers();
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test, Order(1)]
        public void LOGIN_CHECK_TEST()
        {
            var mainPageTests = new MainPageTests();
            mainPageTests.LoginCheckTest(BrowserFactory.MyDriver.Url);
        }

        [Test, Order(2)]
        public void AUTHENTICATION_TEST()
        {
            var loginTests = new LoginTests();
            loginTests.AuthentictationTest();
        }
    }
}