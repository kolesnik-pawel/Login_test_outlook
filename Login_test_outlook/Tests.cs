using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using NUnit.Framework;
using FluentAssertions;

namespace Login_test_outlook
{
    public class Tests
    {
        private IWebDriver webDriver;

        private WebDriverWait Wait(int milliseconds)
        {
            return new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(milliseconds));                           
        }

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
        }

        [Test]
        public void FirstTest()
        {
            webDriver.Navigate().GoToUrl("https://outlook.live.com");

            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

            IWebElement singInButton = webDriver.FindElement(By.XPath("/html/body/header/div/aside/div/nav/ul/li[2]/a"));

            singInButton.Click();
            
            Wait(30);

            IWebElement inputEmail = webDriver.FindElement(By.Name("loginfmt"));
            
            Wait(30);
            inputEmail.SendKeys("Test_mail@exchange.com");

            Wait(30);
            IWebElement NextButton = webDriver.FindElement(By.XPath("//*[@id=\'idSIButton9\']"));

            NextButton.Click();

            IWebElement UserError = webDriver.FindElement(By.Id("usernameError"));

            UserError.Text.Should().Contain("That Microsoft account doesn't exist. Enter a different account");


        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }
    }
}
