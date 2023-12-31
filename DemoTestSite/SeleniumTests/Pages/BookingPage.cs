using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestSite.SeleniumTests.Pages
{
    [TestFixture]
    public class BookingPage : _BasePage
    {
        public BookingPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }


    }
}
