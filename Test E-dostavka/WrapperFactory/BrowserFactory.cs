using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using Test_E_dostavka.Tests;

namespace Test_E_dostavka.WrapperFactory
{
    class BrowserFactory : ReadConfigValue
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver;

        public static IWebDriver MyDriver
        {
            get
            {
                if (driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized.\n" +
                        " You should first call the method InitBrowser.");
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public static void InitBrowser(string browserName, string driverPath)
        {
            switch (browserName)
            {
                case "firefox":
                    if (driver == null)
                    {
                        driver = new FirefoxDriver(@driverPath);
                        Drivers.Add("Firefox", MyDriver);
                    }
                    break;

                case "opera":
                    if (driver == null)
                    {
                        driver = new OperaDriver(@driverPath);
                        Drivers.Add("Opera", MyDriver);
                    }
                    break;

                case "chrome":
                    if (driver == null)
                    {
                        driver = new ChromeDriver(@driverPath);
                        Drivers.Add("Chrome", MyDriver);
                    }
                    break;
            }
        }

        public static void LoadApplication(string url)
        {
           driver.Manage().Window.Maximize(); 
           MyDriver.Url = url;
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
        }
    }
}
