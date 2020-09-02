using NUnit.Framework;
using Test_E_dostavka.Tests;
using Test_E_dostavka.WrapperFactory;

namespace SeleniumTest_1
{
    [TestFixture]
    public class Test
    {
        const string EDOSTAVKA_URL = "https://e-dostavka.by/";
        const string TEL = "375296502259";
        const string PASS = "123456Aa";
        const string FIO = "Юрий\r\nТеуш";
        const int TIME_WAIT = 10;

        [OneTimeSetUp] 
        public void OneTimeSetUp()
        {
            BrowserFactory.InitBrowser("Chrome");
            BrowserFactory.LoadApplication(EDOSTAVKA_URL);
            var mainPageTests = new MainPageTests(BrowserFactory.MyDriver);
            mainPageTests.ClickLoginButton(TIME_WAIT);
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
            MainPageTests.LoginCheckTest(BrowserFactory.MyDriver.Url);
        }

        [Test, Order(2)]
        public void AUTHENTICATION_TEST()
        {
            var loginTests = new LoginTests(BrowserFactory.MyDriver);
            loginTests.AuthentictationTest(EDOSTAVKA_URL, TEL, PASS, FIO, TIME_WAIT);
        }
    }
}