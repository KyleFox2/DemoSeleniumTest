using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;
using DemoTestSite.SeleniumTests.Pages;
using DemoTestSite.SeleniumTests.SeleniumTests;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Numerics;

namespace DemoTestSite.SeleniumTests.Tests
{
    [TestFixture]
    public class FormTests : _BaseTest
    {
        private FormPage formPage;

        [SetUp] 
        public void SetUpFormPage() 
        {
            formPage = new FormPage(driver, wait);
        }       
        
        [Test]
        public void PlaceholderTextTest()
        {
            //Checks the form placeholder text is correct          
            Assert.That(formPage.IsPlaceHolderTextCorrect("Name", "Email", "Phone", "Subject", ""), Is.EqualTo(true), "Placeholder text doesn't match");
        }

        [Test]
        public void AddressInformationTest()
        {
            //Checks the form Address information text is correct          
            Assert.That(formPage.IsAddressInformationCorrect("Shady Meadows B&B", "The Old Farmhouse, Shady Street, Newfordburyshire, NE1 410S", "012345678901", "fake@fakeemail.com"), Is.EqualTo(true), "The information text is incorrect");
        }
     
        [TestCase("Bob", "fake@fakeemail.com", "012345678901", "Interest in this room", "Hi, I'm looking to book a room here.", true)]
        [TestCase("fred", "fake2@fakeeemail.co.uk", "012365678901", "Interest in a few rooms", "Hi, how is it going?", true)]
        [TestCase("joHn", "fake3@fakeem22ail.co.us", "012345678121", "I'm intertested in booking here", "Hello! I'm interested in this rooom", true)]
        public void PositiveTests(string name, string email, string phone, string subject, string message, bool expectSuccessMessage)
        {
            RunTest(name, email, phone, subject, message, expectSuccessMessage);
        }

        [TestCase("", "", "", "", "", false)]
        [TestCase("", "fake@fakeemail.com", "012345678901", "Interest in this room", "Hi, I'm looking to book a room here.", false)]
        [TestCase("Bob", "", "012345678901", "Interest in this room", "Hi, I'm looking to book a room here.", false)]
        [TestCase("Bob", "fake@fakeemail.com", "", "Interest in this room", "Hi, I'm looking to book a room here.", false)]
        [TestCase("Bob", "fake@fakeemail.com", "012345678901", "", "Hi, I'm looking to book a room here.", false)]
        [TestCase("Bob", "fake@fakeemail.com", "012345678901", "Interest in this room", "", false)]
        [TestCase("Bob", "fake@fakeemail.com", "012345678901", "Interest in this room", "test", false)]
        [TestCase("Bob", "fake@fakeemail.com", "012345678901", "test", "Hi, I'm looking to book a room here.", false)]
        [TestCase("Bob", "fake@fakeemail.com", "0123901", "Interest in this room", "Hi, I'm looking to book a room here.", false)]
        public void NegativeTests(string name, string email, string phone, string subject, string message, bool expectSuccessMessage)
        {
            RunTest(name, email, phone, subject, message, expectSuccessMessage);
        }
       
        public void RunTest(string name, string email, string phone, string subject, string message, bool expectSuccessMessage)
        {
            formPage.FillOutForm(name, email, phone, subject, message);
            formPage.SubmitForm();

            if (expectSuccessMessage)
            {
                AssertSuccessMessage(formPage, name, subject);
            }
            else
            {
                AssertErrorMessage(formPage, name, email, phone, subject, message);
            }
        }

        private void AssertSuccessMessage(FormPage formPage, string name, string subject)
        {
            Assert.That(formPage.IsElementPresent(By.XPath("/html/body/div/div[2]/div/div[5]/div[2]/div/h2")), Is.EqualTo(true), "Success message is not present");
            Assert.That(formPage.IsSuccessMessageCorrect(name, subject), Is.EqualTo(true), "The success message is incorrect");
            Assert.That(formPage.IsElementPresent(By.XPath("/html/body/div/div[2]/div/div[5]/div[2]/form/div[6]")), Is.EqualTo(false), "Error messages are displaying");
        }

        private void AssertErrorMessage(FormPage formPage, string name, string email, string phone, string subject, string message)
        {
            Assert.That(formPage.IsElementPresent(By.XPath("/html/body/div/div[2]/div/div[5]/div[2]/div/h2")), Is.EqualTo(false), "Success message is present");                                                                                    
            Assert.That(formPage.IsElementPresent(By.XPath("/html/body/div/div[2]/div/div[5]/div[2]/form/div[6]")), Is.EqualTo(true), "Error messages aren't displaying");
            Assert.That(formPage.ErrorMessageCheck(name, email, phone, subject, message), Is.EqualTo(true), "Incorrect error messages");
        }
    }
}
