using OpenQA.Selenium;


namespace Login_test_outlook
{
    public class ElementsOnPage
    {
        public string  Name { get; set; }

        public IWebElement ElementOnPage{ get; set; }

        public ElementsOnPage(string name, IWebElement webElementOnPage) 
        {
            Name = name;   
            ElementOnPage = webElementOnPage;
            
        }
    }
}
