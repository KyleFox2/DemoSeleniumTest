using DemoTestSite.SeleniumTests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestSite.SeleniumTests
{
    namespace SeleniumTests
    {
        public class _BaseTest
        {
            protected IWebDriver driver;
            protected WebDriverWait wait;

            [SetUp]
            public void Setup()
            {
                driver = WebDriverFactory.GetDriver();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                driver.Navigate().GoToUrl("https://automationintesting.online/");
                Assert.That(driver.Url, Is.EqualTo("https://automationintesting.online/"));
            }

            [TearDown]
            public void TearDown()
            {
                driver.Quit();
            }
        }
    }
}
