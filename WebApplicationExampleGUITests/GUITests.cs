using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationExampleGUITests
{
    [TestFixture]
    public class GUITests
    {
        private IWebDriver _driver;
        private const string _homePageURL = "http://localhost:51287/";
        private Stopwatch _stopWatch;

        [OneTimeSetUp]
        public void StartBrowser()
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            string chromeDriverPath = TestContext.CurrentContext.TestDirectory;
            StartBrowser(chromeDriverPath);
            GoToCalculatePage();
        }

        [TestCaseSource(nameof(GetPositiveValues))]
        public void GUIPositiveTests(Tuple<int, int> value)
        {
            CalculatePage calculatePage = new CalculatePage(_driver);

            string resultString = calculatePage.EnterFirstNumber(value.Item1.ToString()).
                EnterSecondNumber(value.Item2.ToString()).
                ClickOnCalculateButton().
                GetResult();

            int actualResult = Int32.Parse(resultString);
            int expectedResult = value.Item1 + value.Item2;

            Assert.AreEqual(expectedResult, actualResult, "Summ isn't correct");
        }



       



        [OneTimeTearDown]
        public void Cleaning()
        {
            _driver.Quit();
            _stopWatch.Stop();
            File.WriteAllText(@"D:\GUITestDuration.txt", String.Concat("GUI tests duration: ", _stopWatch.ElapsedMilliseconds.ToString(), "\n"));
        }

        private static List<Tuple<int, int>> GetPositiveValues()
        {
            return GetValuesFromFile(@"D:\Learning c#\Presentation\PositiveTests.csv");
        }

        private static List<Tuple<int, int>> GetNegativeValues()
        {
            return GetValuesFromFile(@"D:\Learning c#\Presentation\NegativeTests.csv");
        }

        private static List<Tuple<int, int>> GetValuesFromFile(string filePath)
        {
            string[] values = File.ReadAllLines(filePath);
            if(values != null && values.Any())
            {
                List<Tuple<int, int>> listOfValues = new List<Tuple<int, int>>();
                foreach(string value in values)
                {
                    string[] addendums = value.Split(',');
                    listOfValues.Add(new Tuple<int, int>(Int32.Parse(addendums[0]), Int32.Parse(addendums[1])));
                }
                return listOfValues;
            }
            else
            {
                return null;
            }
        }

        private void GoToCalculatePage()
        {
            _driver.Navigate().GoToUrl(_homePageURL);
        }

        private void StartBrowser(string chromeDriverPath)
        {
            _driver = new ChromeDriver(chromeDriverPath);
            _driver.Manage().Window.Maximize();
        }
    }
}
