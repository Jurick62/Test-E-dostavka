using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Test_E_dostavka.Pages
{
    class PageMain
    {
        private IWebDriver driver;

        [FindsBy(How = How.LinkText, Using = "Войти")]
        private IWebElement LoginButton { get; set; }

        public PageMain(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void ClickLoginButton()
        {
            LoginButton.Click();
        }

        public void WaitUntailTitleContains(int timeWait)
        {
            WebDriverWait wait = new WebDriverWait(this.driver, System.TimeSpan.FromSeconds(timeWait));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Единый аккаунт для всех проектов"));
        }
    }
}
