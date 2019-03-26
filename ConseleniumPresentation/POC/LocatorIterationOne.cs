using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ConseleniumPresentation
{
    [TestFixture]
    public class LocatorIterationOne
    {
        private int iteration = 1000;
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var path = System.IO.Path.GetDirectoryName( 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Substring(6);
            driver = new ChromeDriver($"{path}\\drivers");
            driver.Navigate().GoToUrl($"{path}\\fakewebsite\\simplecase.html");
        }
        
        [Test]
        public void TimeByID()
        {
            var locator = "buttonID";
            var idLocator = By.Id(locator);

            var measuredTime = MeasureTime(idLocator);

            PrintResults(measuredTime, "id", locator);
        }
        
        [Test]
        public void TimeByClass()
        {
            var locator = "button";
            var classNameLocator = By.ClassName(locator);

            var measuredTime = MeasureTime(classNameLocator);

            PrintResults(measuredTime, "class", locator);
        }
        
        [Test]
        public void TimeByCss()
        {
            var locator = "#buttonID";
            var classLocator = By.CssSelector(locator);

            var measuredTime = MeasureTime(classLocator);

            PrintResults(measuredTime, "css", locator);
        }
        
        [Test]
        public void TimeByXpath()
        {
            var locator = "//*[@id=\"buttonID\"]";
            var xpathLocator = By.XPath(locator);

            var measuredTime = MeasureTime(xpathLocator);

            PrintResults(measuredTime, "Xpath", locator);
        }

        private List<long> MeasureTime(By locator)
        {
            var resultsList = new List<long>();
            for (var i = 0; i < iteration; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                driver.FindElement(locator);
                watch.Stop();
                resultsList.Add(watch.ElapsedMilliseconds);
            }

            return resultsList;
        }

        private void PrintResults(List<long> resultsList, string @by, string locatorUsed)
        {
            
            var max = resultsList.Max();
            var min = resultsList.Min();
            var average = resultsList.Average();
            
            Console.WriteLine("----------AllTime is In Millisecodns-------------------------");
            Console.WriteLine($"LocatorTypeUsed {@by}   Value {locatorUsed}");
            Console.WriteLine($"Samples {iteration} Avergere {average}  Min {min}  Max {max}");
            Console.WriteLine("-----------------------------------");
            for (var i = 0; i < iteration; i+=10)
            {
                var row = string.Empty;
                for (var j = 0; j < 10; j++)
                {
                    row +=$"{resultsList[i+j]} |";
                }
                Console.WriteLine($"ROw {i}|  {row}");
            }
            Console.WriteLine("-----------------------------------");
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();            
        }
//todo  more complex html scructure 
//why is first one longer???
//other browsers
//

    }
}