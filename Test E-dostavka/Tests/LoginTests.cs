using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Test_E_dostavka.WrapperFactory;
using Test_E_dostavka.Pages;

namespace Test_E_dostavka.Tests
{
    class LoginTests
    {
        public void AuthentictationTest()
        {
            ReadConfigValue readConfig = new ReadConfigValue();
            readConfig.ReadConfigFile();

            IWebDriver driver = BrowserFactory.MyDriver;
            PageFactory.InitElements(BrowserFactory.MyDriver, this);
            var pageLogin = new PageLogin(BrowserFactory.MyDriver);
            Assert.IsNotNull(pageLogin.LoginPerson);
            Assert.AreEqual(pageLogin.SendLoginName(readConfig.Tel), "+375 (29) 650-22-59");
            Assert.AreEqual(pageLogin.SendLoginPassword(readConfig.Pass), "password");
            pageLogin.ClickLoginSubmit();
            pageLogin.WaitUntailToBeClickable(driver, readConfig.TimeWait);

            string myUrl = driver.Url;
            Assert.AreEqual(readConfig.EdostavkaURL, myUrl);

            Assert.AreEqual(pageLogin.CheckLoginFIO(), readConfig.Fio);
        }
    }
}
