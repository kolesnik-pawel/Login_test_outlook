using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        //[Given(@"Open Browser and go to page '(.*)'")]
        //public void OpenBrowserAndGoToPage(string url)
        //{
        //    webDriver = new ChromeDriver();
        //    webDriver.Manage().Window.Maximize();
        //    webDriver.Navigate().GoToUrl(url);
        //}

        [Given(@"Open (Hendless Browser|Browser) and go to page '(.*)'")]
        public void OpenBrowserAndGoToPageHendless(string handless, string url)
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            if (handless == "Hendless Browser")
            {
                chromeOptions.AddArgument("--headless");
                webDriver = new ChromeDriver(chromeOptions);
            }
            else
            {
                webDriver = new ChromeDriver();
            }
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
            IWebElement? tmpElement = null;
            bool elementExist = true;

            if (findBy == "XPath")
            {
                try
                {
                    tmpElement = webDriver.FindElement(By.XPath(element));
                }
                catch(Exception e) { 
                    
                    elementExist = false;
                    //if e.Message.c
                }   

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

            if (elementExist != false)
            {
                pageElementList.Add(new ElementsOnPage(saveElementName, tmpElement));
            }
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

        [Given(@"Wait permamently for '(\d+)' (second|seconds)")]
        [Given(@"Wait permamently for '(\d+)' (minut|minuts)")]
        public void WaitPermamently(int time, string param)
        {
            if (param == "second" || param == "seconds")
            {
                Thread.Sleep(time*1000);
            }
            else if (param == "minut" || param == "minuts")
            {
                Thread.Sleep(time*1000*60);
            }
        }

        [When(@"Check then popup at ('.*') are visible And close it")]
        public void ChceckPopupAndClose(string element)
        {
            
            IWebElement? tmpElement = null;
            bool? elementExist;
            try
            {
                tmpElement = webDriver.FindElement(By.XPath(element));
                if(tmpElement is not null)
                {
                    if (ScenarioContext.Current.Any(x => x.Key == "PopapClose"))
                    {
                        ScenarioContext.Current["PopapClose"] = true;
                    }
                    else
                    {
                        ScenarioContext.Current.Add("PopapClose", true);
                    }
                    tmpElement.Click(); 
                }
            }
            catch (Exception e) 
            {
                if (ScenarioContext.Current.Any(x => x.Key == "PopapClose"))
                {
                    ScenarioContext.Current["PopapClose"] = false;
                }
                else
                {
                    ScenarioContext.Current.Add("PopapClose", false);
                }
            }
        }
    }
}
