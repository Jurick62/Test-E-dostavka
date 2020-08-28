using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Test_E_dostavka.Pages
{
    class PageLogin
    {
        [FindsBy(How = How.ClassName, Using = "tab-1-active")]
        public IWebElement LoginPerson { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[type = 'tel']")]
        public IWebElement LoginName { get; set; }

        [FindsBy(How = How.Name, Using = "Password")]
        public IWebElement LoginPassword { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "button[type = 'submit']")]
        public IWebElement LoginSubmit { get; set; }

        [FindsBy(How = How.ClassName, Using = "user_fio")]
        public IWebElement LoginCheckFIO { get; set; }
    }
}
