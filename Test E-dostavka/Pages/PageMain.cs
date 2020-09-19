using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Test_E_dostavka.Pages
{
    class PageMain
    {
        readonly private IWebDriver driver;

        [FindsBy(How = How.LinkText, Using = "Войти")]
        public IWebElement LoginButton { get; private set; }

        public PageMain(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void WaitUntailTitleContains(int timeWait)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(timeWait));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Единый аккаунт для всех проектов"));
        }
    }
}
