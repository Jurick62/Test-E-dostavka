using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest_1
{
    [TestFixture]
    public class Test
    {
        const string edostavkaURL = "https://e-dostavka.by/";
        const string tel = "375296502259";
        const string pass = "123456Aa";
        const int timeWait = 5;

        IWebDriver driver = new ChromeDriver();
        [OneTimeSetUp] 
        public void OneTimeSetUp()
        {
            driver.Url = edostavkaURL;
            IWebElement loginButton = driver.FindElement(By.LinkText("Войти"));
            loginButton.Click();
        }

        [OneTimeTearDown] 
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [SetUp] 
        public void SetUp()
        {
        }

        [TearDown] 
        public void TearDown()
        {
        }

        [Test]
        public void LOGIN_CHECK_TEST()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeWait));
            wait.Until(ExpectedConditions.TitleContains("Единый аккаунт для всех проектов"));
            Assert.AreEqual("https://e-account.by/login/", driver.Url);
        }

        [Test]
        public void AUTHENTICATION_TEST() 
        {
            //Проверяем, что активна ссылка "Физическое лицо"
            IWebElement person = driver.FindElement(By.ClassName("tab-1-active"));
            Assert.IsNotNull(person);

            //Ввод логина. Проверка корректности отображения
            IWebElement loginName = driver.FindElement(By.CssSelector("input[type = 'tel']"));
            loginName.Click();
            loginName.SendKeys(tel);
            Assert.AreEqual(loginName.GetAttribute("value"), "+375 (29) 650-22-59");

            //Ввод пароля. Проверка того, что введенные символы не отображаются
            IWebElement password = driver.FindElement(By.Name("Password"));
            password.Click();
            password.SendKeys(pass);
            Assert.AreEqual(password.GetAttribute("type"), "password");

            //Кликаем кнопку "Войти"
            IWebElement submit = driver.FindElement(By.CssSelector("button[type = 'submit']"));
            submit.Click();

            //Ожидание открытия страницы
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeWait));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("user_fio")));

            //Проверка перехода на страницу https://e-dostavka.by/
            string myUrl = driver.Url;
            Assert.AreEqual(edostavkaURL, myUrl);

            //Проверка корректности авторизации
            IWebElement userFIO = driver.FindElement(By.ClassName("user_fio"));
            string checkFIO = userFIO.Text;
            Assert.True(checkFIO == "Юрий\r\nТеуш");
        }
    }
}