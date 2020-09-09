﻿using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Test_E_dostavka.WrapperFactory;
using Test_E_dostavka.Pages;

namespace Test_E_dostavka.Tests
{
    class LoginTests : ReadXMLValue
    {
        public void AuthentictationTest()
        {
            GetValueFromXML();
            IWebDriver driver = BrowserFactory.MyDriver;
            PageFactory.InitElements(BrowserFactory.MyDriver, this);
            var pageLogin = new PageLogin(BrowserFactory.MyDriver);
            Assert.IsNotNull(pageLogin.LoginPerson);
            Assert.AreEqual(pageLogin.SendLoginName(tel), "+375 (29) 650-22-59");
            Assert.AreEqual(pageLogin.SendLoginPassword(pass), "password");
            pageLogin.ClickLoginSubmit();
            pageLogin.WaitUntailToBeClickable(driver, timeWait);

            string myUrl = driver.Url;
            Assert.AreEqual(edostavkaURL, myUrl);

            Assert.AreEqual(pageLogin.CheckLoginFIO(), fio);
        }
    }
}
