using NUnit.Framework;
using Test_E_dostavka.Tests;
using Test_E_dostavka.WrapperFactory;
using System.Collections.Generic;
using System.Xml;

namespace SeleniumTest_1
{
    [TestFixture]
    public class Test
    {
        private readonly IDictionary<string, string> browserConfig = new Dictionary<string, string>();
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            XMLBrowserConfig();
            BrowserFactory.InitBrowser(browserConfig["select"], browserConfig[browserConfig["select"]]);
            BrowserFactory.LoadApplication();
            var mainPageTests = new MainPageTests();
            mainPageTests.ClickLoginButton();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            BrowserFactory.CloseAllDrivers();
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test, Order(1)]
        public void LOGIN_CHECK_TEST()
        {
            var mainPageTests = new MainPageTests();
            mainPageTests.LoginCheckTest(BrowserFactory.MyDriver.Url);
        }

        [Test, Order(2)]
        public void AUTHENTICATION_TEST()
        {
            var loginTests = new LoginTests();
            loginTests.AuthentictationTest();
        }

        public void XMLBrowserConfig()
        {
            const string SEARCH_PATH = "D:/Jurick/Coding/TestProject/Test E-dostavka/Test E-dostavka/Tests/Constants.xml";
            XmlDocument xConfig = new XmlDocument();
            xConfig.Load(SEARCH_PATH);
            foreach (XmlNode configPair in xConfig.DocumentElement.ChildNodes)
            {
                switch (configPair.Name)
                {
                    case "select":
                        browserConfig.Add("select", configPair.InnerText);
                        break;

                    case "firefox":
                        browserConfig.Add("firefox", configPair.InnerText);
                        break;

                    case "opera":
                        browserConfig.Add("opera", configPair.InnerText);
                        break;

                    case "chrome":
                        browserConfig.Add("chrome", configPair.InnerText);
                        break;
                }
            }
        }
    }
}