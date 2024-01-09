using DemoTestSite.SeleniumTests.Pages;
using DemoTestSite.SeleniumTests.SeleniumTests;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoTestSite.SeleniumTests.Tests
{
    [TestFixture]
    public class BookingTests : BaseTest
    {
        private BookingPage bookingPage;

        [SetUp]
        public void SetUpBookingPage()
        {
            bookingPage = new BookingPage(driver, wait);
        }

        [TestCase(true)]
        public void FormTest(bool test)
        {

        }

        [TestCase("current")]
        [TestCase("next")]
        [TestCase("previous")]
        public void CalendarButtonTest(string monthTest)
        {
            DateTime currentDate = DateTime.Now;

            bookingPage.BookThisRoom();

            if (monthTest == "current") 
            {              
                string currentFormattedDate = currentDate.ToString("MMMM yyyy");
                bookingPage.PressCalendarButtons("Today");

                Assert.That(bookingPage.GetCalendarMonth(), Is.EqualTo(currentFormattedDate), "The dates aren't matching");
            } 
            else if(monthTest == "next")
            {
                DateTime nextMonth = currentDate.AddMonths(1);
                string formattedNextMonth = nextMonth.ToString("MMMM yyyy");
                bookingPage.PressCalendarButtons("Next");

                Assert.That(bookingPage.GetCalendarMonth(), Is.EqualTo(formattedNextMonth), "The dates aren't matching");
            } 
            else if(monthTest == "previous")
            {
                DateTime previousMonth = currentDate.AddMonths(-1);
                string formattedPreviousMonth = previousMonth.ToString("MMMM yyyy");
                bookingPage.PressCalendarButtons("Back");

                Assert.That(bookingPage.GetCalendarMonth(), Is.EqualTo(formattedPreviousMonth), "The dates aren't matching");
            }       
        }

        [TestCase(1, 2)]
        [TestCase(4, 9)]
        [TestCase(21, 2)]
        [TestCase(34, 0)]
        public void CalendarDragAndDropTest(int num1, int num2)
        {
            bookingPage.BookThisRoom();
            bookingPage.CalendarTest(num1, num2);

            Assert.That(bookingPage.IsElementPresent(By.ClassName("rbc-event-content")), Is.EqualTo(true), "Event text is not present");
        }

        [Test]
        public void PlaceholderTextTest()
        {
            bookingPage.BookThisRoom();

            Assert.That(bookingPage.IsPlaceHolderTextCorrect("Firstname", "Lastname", "Email", "Phone"), Is.EqualTo(true), "Placeholder text doesn't match");
        }

        [Test]
        public void CancelButtonTest()
        {
            bookingPage.BookThisRoom();
            bookingPage.CancelForm();
            
            Assert.That(bookingPage.IsElementPresent(By.XPath("/html/body/div/div[2]/div/div[4]/div/div/div[3]/button")), Is.EqualTo(true), "Element is present after cancel button has been clicked");
        }

        [TestCase("Steve", "Surlen", "steve.surlen@gmail.com", "07123456789", true)]
        public void PositiveTest(string firstName, string lastName, string email, string phone, bool expectSuccessMessage)
        {
            RunTest(firstName, lastName, email, phone, expectSuccessMessage);
        }

        [TestCase("", "", "", "", false)]
        public void NegativeTest(string firstName, string lastName, string email, string phone, bool expectSuccessMessage)
        {
            RunTest(firstName, lastName, email, phone, expectSuccessMessage);
        }
        
        private  void RunTest(string firstName, string lastName, string email, string phone, bool expectSuccessMessage)
        {
            bookingPage.BookThisRoom();
            bookingPage.SendKeys(firstName, lastName, email, phone);                       
            bookingPage.SubmitForm();
            
            if (expectSuccessMessage)
            {
                AssertSuccessMessage();
            }
            else
            {
                AssertErrorMessage(firstName, lastName, email, phone);
            }
        }

        private void AssertSuccessMessage()
        {
            Assert.That(bookingPage.IsElementPresent(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]")), Is.EqualTo(true), "Success message is not present");
            Assert.That(bookingPage.IsSuccessMessageCorrect(), Is.EqualTo(true), "The success message is incorrect");
            Assert.That(bookingPage.IsElementPresent(By.XPath("/html/body/div/div[2]/div/div[4]/div/div[2]/div[3]/div[5]/p[3]")), Is.EqualTo(false), "Error messages are displaying");
        }

        private void AssertErrorMessage(string firstName, string lastName, string email, string phone)
        {
            Assert.That(bookingPage.IsElementPresent(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]")), Is.EqualTo(false), "Success message is present");
            Assert.That(bookingPage.IsElementPresent(By.XPath("/html/body/div/div[2]/div/div[4]/div/div[2]/div[3]/div[5]")), Is.EqualTo(true), "Error messages aren't displaying");
            Assert.That(bookingPage.ErrorMessageCheck(firstName, lastName, email, phone), Is.EqualTo(true), "Incorrect error messages");
        }
    }
}
