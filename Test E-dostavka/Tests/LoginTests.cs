using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using Test_E_dostavka.WrapperFactory;

namespace Test_E_dostavka.Tests
{
    class LoginTests
    {
        public IWebDriver driver;

        [FindsBy(How = How.ClassName, Using = "tab-1-active")]
        public IWebElement LoginPerson { get; private set; }

        [FindsBy(How = How.CssSelector, Using = "input[type = 'tel']")]
        private IWebElement LoginName { get; set; }

        [FindsBy(How = How.Name, Using = "Password")]
        private IWebElement LoginPassword { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[type = 'submit']")]
        private IWebElement LoginSubmit { get; set; }

        [FindsBy(How = How.ClassName, Using = "user_fio")]
        private IWebElement LoginCheckFIO { get; set; }

        public LoginTests(IWebDriver driver)
        {
            this.driver = BrowserFactory.MyDriver;
            PageFactory.InitElements(BrowserFactory.MyDriver, this);
        }
        public void AuthentictationTest(string url, string tel, string pass, string fio, int timeWait)
        {
            Assert.IsNotNull(LoginPerson);
            LoginName.Click();
            LoginName.SendKeys(tel);
            Assert.AreEqual(LoginName.GetAttribute("value"), "+375 (29) 650-22-59");
            LoginPassword.Click();
            LoginPassword.SendKeys(pass);
            Assert.AreEqual(LoginPassword.GetAttribute("type"), "password");
            LoginSubmit.Click();
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(timeWait));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(LoginCheckFIO));

            string myUrl = driver.Url;
            Assert.AreEqual(url, myUrl);

            Assert.AreEqual(LoginCheckFIO.Text, fio);
        }
    }
}
