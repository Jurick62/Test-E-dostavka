using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace Test_E_dostavka.Pages
{
    class PageMain
    {
        [FindsBy(How = How.LinkText, Using = "Войти")]
        public IWebElement LoginButton { get; set; }
    }
}
