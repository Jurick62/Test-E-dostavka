using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Test_E_dostavka.Pages
{
    class PageLogin
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

        public PageLogin(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public string SendLoginName(string tel)
        {
            LoginName.Click();
            LoginName.SendKeys(tel);
            return LoginName.GetAttribute("value");
        }

        public string SendLoginPassword(string pass)
        {
            LoginPassword.Click();
            LoginPassword.SendKeys(pass);
            return LoginPassword.GetAttribute("type");
        }

        public void ClickLoginSubmit()
        {
            LoginSubmit.Click();
        }

        public string CheckLoginFIO()
        {
            string replaceSpecialSymbol = LoginCheckFIO.Text;
            replaceSpecialSymbol = replaceSpecialSymbol.Insert(replaceSpecialSymbol.IndexOf("\r"), " ");
            replaceSpecialSymbol = replaceSpecialSymbol.Remove(replaceSpecialSymbol.IndexOf("\r"), 2);
            return replaceSpecialSymbol;
        }

        public void WaitUntailToBeClickable(IWebDriver driver, int timeWait)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(timeWait));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(LoginCheckFIO));
        }

    }
}
