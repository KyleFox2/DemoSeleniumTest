using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoTestSite.SeleniumTests.Pages
{
    public class FormPage : _BasePage
    {       
        IWebElement FormName => driver.FindElement(By.Id("name"));
        IWebElement FormEmail => driver.FindElement(By.Id("email"));
        IWebElement FormPhone => driver.FindElement(By.Id("phone"));
        IWebElement FormSubject => driver.FindElement(By.Id("subject"));
        IWebElement FormMessage => driver.FindElement(By.Id("description"));
        IWebElement FormSubmit => driver.FindElement(By.Id("submitContact"));
        IWebElement FormErrorMessages => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[5]/div[2]/form/div[6]"));          

        public FormPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }       

        public void FillOutForm(string formName, string formEmail, string formPhone, string formSubject, string formMessage)
        {
            FormName.SendKeys(formName);
            FormEmail.SendKeys(formEmail);
            FormPhone.SendKeys(formPhone);
            FormSubject.SendKeys(formSubject);
            FormMessage.SendKeys(formMessage);
        }

        public void SubmitForm()
        {
            FormSubmit.Click();
        }

        public bool IsSuccessMessageCorrect(string name, string subject)
        {
            IWebElement headerMessage = driver.FindElement(By.CssSelector("#root > div:nth-child(2) > div > div.row.contact > div:nth-child(2) > div > h2"));
            IWebElement paragrpahOne = driver.FindElement(By.CssSelector("#root > div:nth-child(2) > div > div.row.contact > div:nth-child(2) > div > p:nth-child(2)"));
            IWebElement paragrpahTwo = driver.FindElement(By.CssSelector("#root > div:nth-child(2) > div > div.row.contact > div:nth-child(2) > div > p:nth-child(3)"));
            IWebElement paragrpahThree = driver.FindElement(By.CssSelector("#root > div:nth-child(2) > div > div.row.contact > div:nth-child(2) > div > p:nth-child(4)"));

            return headerMessage.Text == $"Thanks for getting in touch {name}!" &&
                   paragrpahOne.Text == "We'll get back to you about" &&
                   paragrpahTwo.Text == subject &&
            paragrpahThree.Text == "as soon as possible.";
        }
        
        public bool ErrorMessageCheck(string name, string email, string phone, string subject, string message)
        {
            List<string> textValues = new List<string>();

            try
            {
                // Attempt to find the element
                IList<IWebElement> children = FormErrorMessages.FindElements(By.CssSelector("p"));
                
                foreach (IWebElement element in children)
                {
                    textValues.Add(element.Text);
                }
            }
            catch (NoSuchElementException)
            {   

            }           

            bool nameError = true;
            bool emailError = true;
            bool phoneError = true;
            bool subjectError = true;
            bool messageError = true;

            if (message.Length < 5 || message.Length > 2000) messageError = textValues.Contains("Message must be between 20 and 2000 characters.");
            if (message == "") messageError = textValues.Contains("Message may not be blank");
            if (subject.Length < 5 || subject.Length > 100) subjectError = textValues.Contains("Subject must be between 5 and 100 characters.");
            if (subject == "") subjectError = textValues.Contains("Subject may not be blank");
            if (name == "") nameError = textValues.Contains("Name may not be blank");
            if (phone.Length < 11 || phone.Length > 21) phoneError = textValues.Contains("Phone must be between 11 and 21 characters.");
            if (email == "") emailError = textValues.Contains("Email may not be blank");
            if (phone == "") phoneError = textValues.Contains("Phone may not be blank");

            return nameError == true &&
                   emailError == true &&
                   phoneError == true &&
                   subjectError == true &&
                   messageError == true;
        }       

        public bool IsPlaceHolderTextCorrect(string formNamePh, string formEmailPh, string formPhonePh, string formSubjectPh, string formMessagePh)
        {
            return FormName.GetAttribute("placeholder") == formNamePh &&
                   FormEmail.GetAttribute("placeholder") == formEmailPh &&
                   FormPhone.GetAttribute("placeholder") == formPhonePh &&
                   FormSubject.GetAttribute("placeholder") == formSubjectPh &&
                   FormMessage.GetAttribute("placeholder") == formMessagePh;
        }

        public bool IsAddressInformationCorrect(string name, string address, string telephone, string email)
        {
            IWebElement FormAdressText = driver.FindElement(By.CssSelector("div.col-sm-5:nth-child(3)"));
            IList<IWebElement> children = FormAdressText.FindElements(By.CssSelector("p"));
            List<string> textValues = new List<string>();

            foreach (var child in children)
            {
                textValues.Add(child.Text);
            }

            if(textValues.Count != 4)
            {
                return false;
            }

            return textValues[0] == name &&
                   textValues[1] == address &&
                   textValues[2] == telephone &&
                   textValues[3] == email;
        }
    }
}
