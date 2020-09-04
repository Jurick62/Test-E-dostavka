using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Test_E_dostavka.WrapperFactory;
using Test_E_dostavka.Pages;

namespace Test_E_dostavka.Tests
{
    class MainPageTests
    {
        PageMain pageMain = new PageMain(BrowserFactory.MyDriver);
        IWebDriver driver = BrowserFactory.MyDriver;

        public void LoginCheckTest(string driverURL)
        {
            PageFactory.InitElements(BrowserFactory.MyDriver, pageMain);
            Assert.AreEqual("https://e-account.by/login/", driverURL);
        }

        public void ClickLoginButton()
        {
            pageMain.LoginButton.Click();
        }
    }
}
