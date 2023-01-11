using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Login_test_outlook
{
    [Binding]
    public sealed class SpecFlowHelper
    {
        private IWebDriver webDriver;

        private IWebElement tmpWebElement;

        private List<ElementsOnPage> pageElementList;

        [AfterScenario]
        private void AfterScenario()
        {
            webDriver.Quit();
        }

        [BeforeScenario] 
        private void BeforeScenario() 
        {
            pageElementList = new List<ElementsOnPage>(); 

        }

        [Given(@"Open Browser and go to page '(.*)'")]
        public void OpenBrowserAndGoToPage(string url)
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(url);
        }

        [When(@"Find '(.*)' element using (XPath|Name|Id) select type and try Click it")]
        public void FindElementAndClick(string element, string findBy = "XPath")
        {
            if (findBy == "XPath")
            {
                webDriver.FindElement(By.XPath(element)).Click();
            }
            else if (findBy == "Name")
            {
                webDriver.FindElement(By.Name(element)).Click();
            }
            else if (findBy == "Id")
            {
                webDriver.FindElement(By.Id(element)).Click();
            }
            else
            {
                throw new Exception($"Using unsupported By parameters {findBy}");
            }

        }

        [When(@"Find '(.*)' element using (XPath|Name|Id) and save")]
        [Then(@"Find '(.*)' element using (XPath|Name|Id) and save")]
        public void FindElementAndSave(string element, string findBy = "XPath")
        {
            if (findBy == "XPath")
            {
                tmpWebElement = webDriver.FindElement(By.XPath(element));
            }
            else if (findBy == "Name")
            {
                tmpWebElement = webDriver.FindElement(By.Name(element));
            }
            else if (findBy == "Id")
            {
                tmpWebElement = webDriver.FindElement(By.Id(element));
            }
            else
            {
                throw new Exception($"Using unsupported By parameters {findBy}");
            }

        }
        [When(@"Find '(.*)' element using (XPath|Name|Id) and save as '(.*)'")]
        [Then(@"Find '(.*)' element using (XPath|Name|Id) and save as '(.*)'")]
        public void FindElementAndSaveAs(string element, string findBy, string saveElementName)
        {
            IWebElement tmpElement;

            if (findBy == "XPath")
            {
                tmpElement = webDriver.FindElement(By.XPath(element));
            }
            else if (findBy == "Name")
            {
                tmpElement = webDriver.FindElement(By.Name(element));
            }
            else if (findBy == "Id")
            {
                tmpElement = webDriver.FindElement(By.Id(element));
            }
            else
            {
                throw new Exception($"Using unsupported By parameters {findBy}");
            }

            pageElementList.Add(new ElementsOnPage(saveElementName, tmpElement));

        }

        [Then(@"I Click at button")]
        public void ClickAtElement()
        {
            if (tmpWebElement != null)
            {
                tmpWebElement.Click();
            }
            else
            {
                throw new Exception($"Button not set at");
            }
        }

        [Then(@"I Click at '(.*)' button")]
        public void ClickAtElement(string findElementToClick)
        {
            try
            {
                IWebElement webElement = pageElementList.Find(x => x.Name == findElementToClick).ElementOnPage;
                webElement.Click();

            } 
            catch (Exception e) 
            {
                throw new Exception($"Element {findElementToClick} does't exist");
            }                        
        }

        [Then(@"I fill email input using '(.*)' email")]
        [When(@"I fill email input using '(.*)' email")]
        public void SendValueToElement(string fillValue)
        {
            tmpWebElement.SendKeys(fillValue); 
        }

        [Then(@"I Check at the '(.*)' object contains '(.*)'")]
        public void ElementContainsString(string slementStringValue, string containsString)
        {
            try
            {
                IWebElement webElement = pageElementList.Find(x => x.Name == slementStringValue).ElementOnPage;
                webElement.Text.Should().Contain(containsString);

            }
            catch (Exception e)
            {
               throw new Exception($"Element {slementStringValue} does't contain {containsString}. \n Exeption message {e.Message} ");
            }
        }

        [When(@"I check then page loaded until '(\d+)' miliseconds")]
        public void Waits(int milliseconds)
        {
           webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(milliseconds);


        }
    }
}
