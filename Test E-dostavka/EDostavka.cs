using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest_1
{
    [TestFixture]
    public class Test
    {
        IWebDriver driver = new ChromeDriver();
        [OneTimeSetUp] // вызывается перед началом запуска всех тестов
        public void OneTimeSetUp()
        {
            driver.Url = "https://e-dostavka.by/";
            IWebElement loginButton = driver.FindElement(By.LinkText("Войти"));
            loginButton.Click();
        }

        [OneTimeTearDown] //вызывается после завершения всех тестов
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        [SetUp] // вызывается перед каждым тестом
        public void SetUp()
        {
            // Code here
        }

        [TearDown] // вызывается после каждого теста
        public void TearDown()
        {
            // Code here
        }

        [Test]
        public void TEST_1()    //Проверяем переход на страницу авторизации
        {
            Assert.AreEqual("https://e-account.by/login/", driver.Url);
        }

        [Test]
        public void TEST_2()    //Проверка страницы авторизации для физ. лиц. Позитивный тест.
        {
            //Проверяем, что активна ссылка "Физическое лицо"
            IWebElement person = driver.FindElement(By.ClassName("tab-1-active"));
            Assert.IsNotNull(person);

            //Заполняем поле "Номер телефона". проверяем, что приведено к виду +ХХХ (ХХ) ХХХ-ХХ-ХХ и отображается верно
            IWebElement loginName = driver.FindElement(By.CssSelector("input[type = 'tel']"));
            loginName.Click();
            loginName.SendKeys("375296502259");
            Assert.True(loginName.GetAttribute("value") == "+375 (29) 650-22-59");

            //Заполняем поле "Пароль". проверяем, что введенные символы не отображаются
            IWebElement password = driver.FindElement(By.Name("Password"));
            password.Click();
            password.SendKeys("123456Aa");
            Assert.True(password.GetAttribute("type") == "password");

            //Кликаем кнопку "Войти"
            IWebElement submit = driver.FindElement(By.CssSelector("button[type = 'submit']"));
            submit.Click();

            //Ждем открытия страницы
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("user_fio")));

            //Проверяем, что открылась старница https://e-dostavka.by/
            string myUrl = driver.Url;
            Assert.AreEqual("https://e-dostavka.by/", myUrl);

            //И появилось ФИО пользователя
            IWebElement userFIO = driver.FindElement(By.ClassName("user_fio"));
            bool checkFIO = userFIO.Displayed;
            Assert.True(userFIO.Displayed);


        }
    }
}