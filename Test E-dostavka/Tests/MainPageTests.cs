using NUnit.Framework;
using SeleniumExtras.PageObjects;
using Test_E_dostavka.WrapperFactory;
using Test_E_dostavka.Pages;

namespace Test_E_dostavka.Tests
{
    class MainPageTests : ReadXMLValue
    {
        PageMain pageMain = new PageMain(BrowserFactory.MyDriver);

        public void LoginCheckTest(string driverURL)
        {
            PageFactory.InitElements(BrowserFactory.MyDriver, pageMain);
            Assert.AreEqual("https://e-account.by/login/", driverURL);
        }

        public void ClickLoginButton()
        {
            int timeWait = 0;
            GetValueFromXML();
            pageMain.LoginButton.Click();
            pageMain.WaitUntailTitleContains(timeWait);
        }
    }
}
