using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using Test_E_dostavka.WrapperFactory;

namespace Test_E_dostavka.Tests
{
    class MainPageTests
    {
        private IWebDriver driver;

        [FindsBy(How = How.LinkText, Using = "Войти")]
        private IWebElement LoginButton { get; set; }

        public MainPageTests(IWebDriver driver)
        {
            this.driver = BrowserFactory.MyDriver;
            PageFactory.InitElements(BrowserFactory.MyDriver, this);
        }

        public void ClickLoginButton(int timeWait)
        {
            LoginButton.Click();
            WebDriverWait wait = new WebDriverWait(this.driver, System.TimeSpan.FromSeconds(timeWait));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Единый аккаунт для всех проектов"));
        }

        public static void LoginCheckTest(string driverURL)
        {
            Assert.AreEqual("https://e-account.by/login/", driverURL);
        }
    }
}
