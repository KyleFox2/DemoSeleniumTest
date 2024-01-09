using DemoTestSite.SeleniumTests.Pages;
using DemoTestSite.SeleniumTests.SeleniumTests;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestSite.SeleniumTests.Tests
{
    [TestFixture]
    public class HomePageTests : BaseTest
    {
        private HomePage homePage;

        [SetUp]
        public void SetUpHomePage()
        {           
            homePage = new HomePage(driver, wait);
        }          

        [TestCase("Welcome to Restful Booker Platform", TestScenario.WelcomeHeader)]
        [TestCase("Your one stop shop to practise Software Testing!", TestScenario.WelcomeSubheader)]
        [TestCase("Exploration: Testing is more than just finding bugs. With Restful-booker-platform you can use it to hone your exploratory testing skills by diving into the application to find out more about how it works. There are many features for you to explore, with more being continuously added. So there will always be something new to explore!", TestScenario.ExplorationText)]
        [TestCase("Automation: Restful-booker-platform is an open source application and it offers a range of different technologies that you can automate against, either online or via a locally deployed instance.Check out the restful-booker-platform source code to learn more about the various APIs and JavaScript features to practise your Automation in Testing skills.", TestScenario.AutomationText)]
        [TestCase("Infrastructure: Restful-booker-platform is a continuously deployed application using CircleCi and Docker. All the deployment assets can be found in the restful-booker-platform source repository for you to create your own pipeline. You can also learn more about the build process in this public build pipeline.", TestScenario.InfrastructureText)]
        [TestCase("Get Started: How you use this application is up to you, but here are a few things to get you started:Explore the home page\r\nAccess the admin panel with the credentials admin/password\r\nYou can read more about the features here\r\nIf you find a particularly bad bug, feel free to raise it herePlease note: for security reasons the database resets every 10 minutes.", TestScenario.GetStartedText)]
        [TestCase("restful-booker-platform v1.6.0 Created by Mark Winteringham / Richard Bradshaw - © 2019-22 Cookie-Policy - Privacy-Policy - Admin panel\r\nLearn more about Automation in Testing", TestScenario.GetFooterText)]
        [TestCase("Welcome to Shady Meadows, a delightful Bed & Breakfast nestled in the hills on Newingtonfordburyshire. A place so beautiful you will never want to leave. All our rooms have comfortable beds and we provide breakfast from the locally sourced supermarket. It is a delightful place.", TestScenario.GetWelcomeText)]
        [TestCase("Rooms", TestScenario.RoomsTitle)]
        [TestCase("single", TestScenario.SingleTitle)]
        [TestCase("Aenean porttitor mauris sit amet lacinia molestie. In posuere accumsan aliquet. Maecenas sit amet nisl massa. Interdum et malesuada fames ac ante.TV\r\nWiFi\r\nSafe", TestScenario.RoomsText)]
        public void TestHomePageText(string expectedText, TestScenario scenario)
        {
            string actualText = GetActualText(scenario);
            
            Assert.IsTrue(homePage.IsAlertBannerPresent(), "Alert banner isn't present");
            Assert.That(actualText, Is.EqualTo(expectedText), "There is a discrepancy between the two sets of text");
        }

        [TestCase("https://www.mwtestconsultancy.co.uk/img/rbp-logo.png", TestScenario.GetHomeImage)]
        [TestCase("https://www.mwtestconsultancy.co.uk/img/testim/room2.jpg", TestScenario.GetWelcomeImage)]
        [TestCase("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB0AAAAiCAYAAACjv9J3AAACQ0lEQVR4AWLABkbBv3//BIHYEYgLgbgRiAOB2IAWFrEAaKunwwiDIIDjTaSElJIOUsKVkCpi27ZtnG0jtu1MZh8mnD3fw/9tZ3+fPywPU2Ig6QqbxBTpAAXmoc0fX19h7+oObAcXYNg5hfD5DZzcPRJMKbGcZMEe2khAs749qNH42Jr1AVgOHsD14/NPXJEUKM5sxrcL1Rpv3Om3TxKCCawksN8WgWq1J+GWA/s8LAFzaHGfNZwUSC19wwdYVjR0EsNLdAzVKnfKhc+uCe6UgdkYPL68QqPWC1VKV8q1GwNRz5ZeD/AcXUTdaMi1CyvbF6A+uAXRdOBIPL3S9aHTK4JzOXQMgyn3FlSsO9gmfAcCYms1BtmZ9dA+oWUcasWgxxSAijXHv5r0PgLY8OzZuWFbmNA1DgVR+aqNbdCxLQWpqg3Hv7k6pZPQKw69wsSi5NF1BzdLKHDoGgZDlgCUrVj+hU9irMvLznUbvIRaObQMg9XADjssmgseS9Euc4idmXCECR3j0FwMgsd4xMtmtspVK4x79n5h67tX0GkKSmdc+6eE5nFoFnaAQbPaCaVLJmkVeAZNWo8o6rqaNav42BCaLfsqddLZluBQqmlCewROkhH1bGedYShZMCRdp9ZFINBPPRqswCBZmMDHlxcCK+VaFLhoThd3HRrHT7AnLpCFHeH4QHUKIA+HoHBWK61dZU8d5OEgFM5o/pVWkIXtv+E2pS39IAfPIFwwrc4syMEq/3bmQQ7+HO0txGyqNhIqaUYBAA36UJJokkxWAAAAAElFTkSuQmCC", TestScenario.GetMapImage)]
        public void TestHomePageImages(string expectedLink, TestScenario scenario)
        {
            string srcLink = GetImage(scenario);

            Assert.That(srcLink, Is.EqualTo(expectedLink), "There is a discrepancy between the two links");
        }

        [Test]
        public void AlertDismissBtn()
        {
            Assert.IsTrue(homePage.IsAlertBannerPresent(), "Alert banner isn't present");
            Assert.That(homePage.AlertButtonTest(), Is.EqualTo(true), "The alert banner hasn't been dismissed");
        }

        private string GetActualText(TestScenario scenario)
        {
            switch (scenario)
            {
                case TestScenario.WelcomeHeader:
                    return homePage.GetAlertWelcomeHeader();
                case TestScenario.WelcomeSubheader:
                    return homePage.GetAlertWelcomeSubheader();
                case TestScenario.ExplorationText:
                    return homePage.GetAlertExplorationText();
                case TestScenario.AutomationText:
                    return homePage.GetAlertAutomationText();
                case TestScenario.InfrastructureText:
                    return homePage.GetAlertInfrastructureText();
                case TestScenario.GetStartedText:
                    return homePage.GetAlertGetStartedText();
                case TestScenario.GetFooterText:
                    return homePage.GetFooterText();
                case TestScenario.GetWelcomeText:
                    return homePage.GetWelcomeText();
                case TestScenario.RoomsTitle:
                    return homePage.GetRoomsTitle();
                case TestScenario.RoomsText:
                    return homePage.GetRoomsText();
                case TestScenario.SingleTitle:
                    return homePage.GetSingleTitle();
                default:
                    return "";
            }
        }

        private string GetImage(TestScenario scenario)
        {
            switch (scenario)
            {
                case TestScenario.GetHomeImage:
                    return homePage.GetShadyMeadowsImageSource();
                case TestScenario.GetWelcomeImage:
                    return homePage.GetRoomImageSource();
                case TestScenario.GetMapImage:
                    return homePage.GetMapImageSource();
                default:
                    return "";
            }
        }

        public enum TestScenario
        {
            WelcomeHeader,
            WelcomeSubheader,
            ExplorationText,
            AutomationText,
            InfrastructureText,
            GetStartedText,
            GetFooterText,
            GetWelcomeText,
            RoomsTitle,
            SingleTitle,
            RoomsText,
            GetHomeImage,
            GetWelcomeImage,
            GetMapImage
        }
    }
}
