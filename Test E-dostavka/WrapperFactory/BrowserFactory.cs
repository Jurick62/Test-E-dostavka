using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;

namespace Test_E_dostavka.WrapperFactory
{
    class BrowserFactory
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

        public static void InitBrowser(string browserName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string driverPath = path.Substring(0, path.IndexOf("Test E-dostavka\\bin")) + "packages\\";
            switch (browserName)
            {
                case "firefox":
                    if (driver == null)
                    {
                        driverPath += "Selenium.Firefox.WebDriver.0.27.0\\driver\\";
                        driver = new FirefoxDriver(driverPath);
                        Drivers.Add("Firefox", MyDriver);
                    }
                    break;

                case "opera":
                    if (driver == null)
                    {
                        driverPath += "Selenium.Opera.WebDriver.2.30.0\\driver\\";
                        driver = new OperaDriver(@driverPath);
                        Drivers.Add("Opera", MyDriver);
                    }
                    break;

                case "chrome":
                    if (driver == null)
                    {
                        driverPath += "Selenium.Chrome.WebDriver.85.0.0\\driver\\";
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
