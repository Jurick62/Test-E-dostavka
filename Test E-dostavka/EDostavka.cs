using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Test_E_dostavka.Pages;

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

        IWebDriver driver = new ChromeDriver();

        [OneTimeSetUp] 
        public void OneTimeSetUp()
        {
            driver.Url = EDOSTAVKA_URL;
            var pageMain = new PageMain(driver);
            pageMain.ClickLoginButton();
            pageMain.WaitUntailTitleContains(TIME_WAIT);
        }

        [OneTimeTearDown] 
        public void OneTimeTearDown()
        {
            driver.Quit();
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
            Assert.AreEqual("https://e-account.by/login/", driver.Url);
        }

        [Test, Order(2)]
        public void AUTHENTICATION_TEST()
        {

            var pageLogin = new PageLogin(driver);
            Assert.IsNotNull(pageLogin.LoginPerson);

            Assert.AreEqual(pageLogin.SendLoginName(TEL), "+375 (29) 650-22-59");

            Assert.AreEqual(pageLogin.SendLoginPassword(PASS), "password");

            pageLogin.ClickLoginSubmit();

            pageLogin.WaitUntailToBeClickable(driver, TIME_WAIT);

            string myUrl = driver.Url;
            Assert.AreEqual(EDOSTAVKA_URL, myUrl);

            Assert.AreEqual(pageLogin.CheckLoginFIO(), FIO);
        }
    }
}