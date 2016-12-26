using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationExampleGUITests
{
    public class CalculatePage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "first")]
        private IWebElement _firstNumberElement;

        [FindsBy(How = How.Id, Using = "second")]
        private IWebElement _secondNumberElement;

        [FindsBy(How = How.Id, Using = "calculate")]
        private IWebElement _calculateButtonElement;

        [FindsBy(How = How.Id, Using = "result")]
        private IWebElement _resultElement;

        public CalculatePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public CalculatePage EnterFirstNumber(String number)
        {
            _firstNumberElement.Clear();
            _firstNumberElement.SendKeys(number);
            return this;
        }

        public CalculatePage EnterSecondNumber(String number)
        {
            _secondNumberElement.Clear();
            _secondNumberElement.SendKeys(number);
            return this;
        }

        public CalculatePage ClickOnCalculateButton()
        {
            _calculateButtonElement.Click();
            return this;
        }

        public String GetResult()
        {
            return _resultElement.Text;
        }


    }
}
