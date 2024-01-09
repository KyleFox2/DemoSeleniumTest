using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestSite.SeleniumTests.Pages
{
    public class HomePage : _BasePage
    {
        public HomePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        private IWebElement alertWelcomeHeader => driver.FindElement(By.XPath("//div[@id='collapseBanner']//h1"));
        private IWebElement alertWelcomeSubheader => driver.FindElement(By.XPath("/html/body/div/div[1]/div/div/div[1]/div/h4"));
        private IWebElement alertExplorationText => driver.FindElement(By.XPath("//div[@id='collapseBanner']//p[contains(.,'Exploration:')]"));
        private IWebElement alertAutomationText1 => driver.FindElement(By.XPath("//div[@id='collapseBanner']//p[contains(.,'Automation:')]"));
        private IWebElement alertAutomationText2 => driver.FindElement(By.XPath("/html/body/div/div[1]/div/div/div[2]/div[2]/p[2]"));
        private IWebElement alertInfrastructureText => driver.FindElement(By.XPath("//div[@id='collapseBanner']//p[contains(.,'Infrastructure:')]"));
        private IWebElement alertGetStartedText1 => driver.FindElement(By.XPath("/html/body/div/div[1]/div/div/div[2]/div[4]/p[1]"));
        private IWebElement alertGetStartedText2 => driver.FindElement(By.XPath("/html/body/div/div[1]/div/div/div[2]/div[4]/ul"));
        private IWebElement alertGetStartedText3 => driver.FindElement(By.XPath("/html/body/div/div[1]/div/div/div[2]/div[4]/p[2]"));
        private IWebElement RoomsTitle => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div[2]/h2"));
        private IWebElement SingleTitle => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[4]/div/div/div[3]/h3"));
        private IWebElement RoomsText => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[4]/div/div/div[3]/p"));
        private IWebElement RoomsList => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[4]/div/div/div[3]/ul"));
        private IWebElement footerText => driver.FindElement(By.XPath("/html/body/div/footer/div/p"));
        private IWebElement welcomeText => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[2]/div[2]/p"));
        private IWebElement shadyMeadowsImage => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[1]/div/img"));
        private IWebElement roomImage => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[4]/div/div/div[2]/img"));
        private IWebElement mapView => driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[6]/div/div/div/div[2]/div/img"));
        private IWebElement letMeHackBtn => driver.FindElement(By.XPath("/html/body/div/div[1]/div/div/div[3]/div[2]/button"));

        // Methods to get text from elements
        public string GetAlertWelcomeHeader() => alertWelcomeHeader.Text;
        public string GetAlertWelcomeSubheader() => alertWelcomeSubheader.Text;
        public string GetAlertExplorationText() => alertExplorationText.Text;
        public string GetAlertAutomationText() => alertAutomationText1.Text + alertAutomationText2.Text;
        public string GetAlertInfrastructureText() => alertInfrastructureText.Text;
        public string GetAlertGetStartedText() => alertGetStartedText1.Text + alertGetStartedText2.Text + alertGetStartedText3.Text;
        public string GetRoomsTitle() => RoomsTitle.Text;
        public string GetSingleTitle() => SingleTitle.Text;
        public string GetRoomsText() => RoomsText.Text + RoomsList.Text;
        public string GetFooterText() => footerText.Text;
        public string GetWelcomeText() => welcomeText.Text;
        public string GetShadyMeadowsImageSource() => shadyMeadowsImage.GetAttribute("src");
        public string GetRoomImageSource() => roomImage.GetAttribute("src");
        public string GetMapImageSource() => mapView.GetAttribute("src");       

        public bool IsAlertBannerPresent()
        {
            if (!IsElementPresent(By.Id("collapseBanner"))) return false;

            return true;
        }

        public bool AlertButtonTest()
        {
            letMeHackBtn.Click();

            if (IsAlertBannerPresent()) return false;

            return true;
        }
    }
}
