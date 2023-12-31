using DemoTestSite.SeleniumTests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestSite.SeleniumTests.Pages
{
    public class _BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        public _BasePage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                // Attempt to find the element
                var element = wait.Until(c => c.FindElement(by));
                driver.FindElement(by);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                // Element was not found
                return false;
            }
        }       
    }
}
