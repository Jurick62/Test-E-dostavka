using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Test_E_dostavka.Pages;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

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
            var pageMain = new PageMain();
            PageFactory.InitElements(driver, pageMain);
            pageMain.LoginButton.Click();
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
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIME_WAIT));
            wait.Until(ExpectedConditions.TitleContains("Единый аккаунт для всех проектов"));
            Assert.AreEqual("https://e-account.by/login/", driver.Url);
        }

        [Test, Order(2)]
        public void AUTHENTICATION_TEST() 
        {

            var pageLogin = new PageLogin();
            PageFactory.InitElements(driver, pageLogin);
            Assert.IsNotNull(pageLogin.LoginPerson);

            pageLogin.LoginName.Click();
            pageLogin.LoginName.SendKeys(TEL);
            Assert.AreEqual(pageLogin.LoginName.GetAttribute("value"), "+375 (29) 650-22-59");

            pageLogin.LoginPassword.Click();
            pageLogin.LoginPassword.SendKeys(PASS);
            Assert.AreEqual(pageLogin.LoginPassword.GetAttribute("type"), "password");

            pageLogin.LoginSubmit.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIME_WAIT));
            wait.Until(ExpectedConditions.ElementToBeClickable(pageLogin.LoginCheckFIO));

            string myUrl = driver.Url;
            Assert.AreEqual(EDOSTAVKA_URL, myUrl);

            Assert.AreEqual(pageLogin.LoginCheckFIO.Text, FIO);
        }
    }
}