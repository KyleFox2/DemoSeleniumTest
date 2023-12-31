using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestSite.SeleniumTests.Utilities
{
    public class WebDriverFactory
    {
        public static IWebDriver GetDriver()
        {
            // Customise this method to support different browsers, configurations, etc.
            return new ChromeDriver();
        }
    }
}
