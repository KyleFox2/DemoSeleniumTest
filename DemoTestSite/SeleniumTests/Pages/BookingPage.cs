using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using OpenQA.Selenium.Interactions;

namespace DemoTestSite.SeleniumTests.Pages
{
    [TestFixture]
    public class BookingPage : _BasePage
    {
        private Actions actions;
        public BookingPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
            // Create an Actions object
            actions = new Actions(driver);
        }

        IWebElement FirstName => driver.FindElement(By.Name("firstname"));
        IWebElement LastName => driver.FindElement(By.Name("lastname"));
        IWebElement Email => driver.FindElement(By.Name("email"));
        IWebElement PhoneNumber => driver.FindElement(By.Name("phone"));
        IWebElement BookButton => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[4]/div/div[2]/div[3]/button[2]"));
        IWebElement CancelButton => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[4]/div/div[2]/div[3]/button[1]"));
        IWebElement BookThisRoomButton => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[4]/div/div/div[3]/button"));
        IList<IWebElement> CalendarTable => driver.FindElements(By.ClassName("rbc-button-link"));
        IWebElement CalendarButtons => driver.FindElement(By.ClassName("rbc-btn-group"));
        IWebElement CalendarMonth => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[4]/div/div[2]/div[2]/div/div[1]/span[2]"));
        IWebElement SuccessMessage => driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]"));
        IWebElement ErrorMessages => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[4]/div/div[2]/div[3]/div[5]"));

        public string GetCalendarMonth() => CalendarMonth.Text;

        public void PressCalendarButtons(string btnName)
        {
            IList<IWebElement> children = CalendarButtons.FindElements(By.TagName("button"));

            foreach (var child in children)
            {
                if (btnName == child.Text)
                {
                    child.Click();
                }
            }
        }

        public void CalendarTest(int num1, int num2)
        {            
            if (num1 >= 0 && num1 <= 34 && num2 >= 0 && num2 <= 34)
            {
                // Perform drag-and-drop by moving to the source, clicking and holding, moving to the target, and releasing
                actions
                    .MoveToElement(CalendarTable[num1])
                    .ClickAndHold()
                    .MoveToElement(CalendarTable[num2])
                    .Release()
                    .Perform();
            }
            else
            {
                // Handle the case when there are not enough elements in the collection
                Console.WriteLine("Not enough elements in the CalendarTable collection.");
            }
        }

        public void BookThisRoom()
        {
            BookThisRoomButton.Click();
        } 

        public void SubmitForm()
        {
            BookButton.Click();
        }

        public void CancelForm()
        {
            CancelButton.Click();
        }

        public void SendKeys(string firstName, string lastName, string email, string phone) 
        {
            FirstName.SendKeys(firstName);
            LastName.SendKeys(lastName);
            Email.SendKeys(email);
            PhoneNumber.SendKeys(phone);
        }
        public bool IsSuccessMessageCorrect()
        {
            IWebElement headerMessage = SuccessMessage.FindElement(By.TagName("h3"));
            IWebElement subMessage = SuccessMessage.FindElement(By.XPath("/html/body/div[4]/div/div/div[1]/div[2]/p[1]"));
            
            return headerMessage.Text == "Booking Successful!" &&
                   subMessage.Text == "Congratulations! Your booking has been confirmed for:";
        }

        public bool ErrorMessageCheck(string firstName, string lastName, string email, string phone)
        {
            List<string> textValues = new List<string>();

            try
            {
                // Attempt to find the element
                IList<IWebElement> children = ErrorMessages.FindElements(By.CssSelector("p"));

                foreach (IWebElement element in children)
                {
                    textValues.Add(element.Text);
                }
            }
            catch (NoSuchElementException)
            {

            }

            bool firstNameError = true;
            bool lastNameError = true;
            bool emailError = true;
            bool phoneError = true;

            if (firstName == "") firstNameError = textValues.Contains("Firstname should not be blank");
            if (firstName.Length < 3 || firstName.Length > 18) firstNameError = textValues.Contains("size must be between 3 and 18");
            if (lastName == "") lastNameError = textValues.Contains("Lastname should not be blank");
            if (lastName.Length < 3 || lastName.Length > 30) lastNameError = textValues.Contains("size must be between 3 and 30");
            if (email == "") emailError = textValues.Contains("must not be empty");
            if (email.Contains('@')) emailError = textValues.Contains("must be a well-formed email address");
            if (phone == "") phoneError = textValues.Contains("must not be null");
            if (phone.Length < 11 || phone.Length > 21) phoneError = textValues.Contains("size must be between 11 and 21");          

            return firstNameError == true &&
                   lastNameError == true &&
                   emailError == true &&
                   phoneError == true;
        }

        public bool IsPlaceHolderTextCorrect(string firstNamePh, string lastNamePh, string emailPh, string phonePh)
        {
            //checks the placeholder text and returns true if its correct
            return FirstName.GetAttribute("placeholder") == firstNamePh &&
                   LastName.GetAttribute("placeholder") == lastNamePh &&
                   Email.GetAttribute("placeholder") == emailPh &&
                   PhoneNumber.GetAttribute("placeholder") == phonePh;
        }
    }
}
