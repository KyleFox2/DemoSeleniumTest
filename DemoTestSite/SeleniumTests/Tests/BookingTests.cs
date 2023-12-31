using DemoTestSite.SeleniumTests.Pages;
using DemoTestSite.SeleniumTests.SeleniumTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestSite.SeleniumTests.Tests
{
    [TestFixture]
    public class BookingTests : _BaseTest
    {
        [SetUp]
        public void SetUpBookingPage()
        {
            BookingPage bookingPage = new BookingPage(driver, wait);
        }

        [Test]
        public void Booking()
        {

        }
    }
}
