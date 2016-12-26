using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationExampleApiTests
{
    [TestFixture]
    public class APITests
    {
        private Stopwatch _stopWatch;

        [OneTimeSetUp]
        public void Preparation()
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        [TestCaseSource(nameof(GetPositiveValues))]
        public void APITestsPositive(Tuple<int, int> positiveValue)
        {
            WebHelper webHelper = new WebHelper();
            String address = GetAddress(positiveValue.Item1, positiveValue.Item2);
            int actualResult = Int32.Parse(webHelper.GetResponseByGetQuery(address));

            int expectedResult = positiveValue.Item1 + positiveValue.Item2;

            Assert.AreEqual(expectedResult, actualResult, "Summ aren't equal");
        }

        [TestCaseSource(nameof(GetNegativeValues))]
        public void APITestsNegative(Tuple<int, int> negativeValue)
        {
            WebHelper webHelper = new WebHelper();
            String address = GetAddress(negativeValue.Item1, negativeValue.Item2);
            String actualResult = webHelper.GetErrorMessageResponseByGetQuery(address);

            String expectedResult = "Incorrect value";

            Assert.AreEqual(expectedResult, actualResult, "Messages aren't equal");
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
            if (values != null && values.Any())
            {
                List<Tuple<int, int>> listOfValues = new List<Tuple<int, int>>();
                foreach (string value in values)
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

        private string GetAddress(int first, int second)
        {
            String adressSummController = "http://localhost:51287/Default/api/Calculate/Sum";
            return $"{adressSummController}?first={first}&second={second}";
        }

        [OneTimeTearDown]
        public void StopTimer()
        {
            _stopWatch.Stop();
            File.WriteAllText(@"D:\ApiTestDuration.txt", String.Concat("API tests duration: ",_stopWatch.ElapsedMilliseconds.ToString(), "\n"));
        }
    }
}
