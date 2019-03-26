using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ConseleniumPresentation
{
    [TestFixture]
    public class LocatorTestIterationOne
    {
        private int iteration = 1000;

        [Test]
        public void POC()
        {
            var path = System.IO.Path.GetDirectoryName( 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Substring(6);
            var driver = new ChromeDriver($"{path}\\drivers");
            driver.Navigate().GoToUrl($"{path}\\fakewebsite\\simplecase.html");
            var resultsList = new List<long>();  
            for (int i = 0; i < iteration; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                driver.FindElement(By.Id("buttonID"));
                watch.Stop();
                resultsList.Add(watch.ElapsedMilliseconds);
            }

            PrintResults(resultsList);
            driver.Quit();
            
        }

        private void PrintResults(List<long> resultsList)
        {
            var max = resultsList.Max();
            var min = resultsList.Min();
            var average = resultsList.Average();
            
            Console.WriteLine("----------AllTime is In Millisecodns-------------------------");
            Console.WriteLine($"Samples {iteration} Avergere {average}  Min {min}  Max {max}");
            Console.WriteLine("-----------------------------------");
            for (int i = 0; i < iteration; i+=5)
            {
                Console.WriteLine($"ROw {i}  {resultsList[i]}  {resultsList[i+1]} {resultsList[i+2]} {resultsList[i+3]} {resultsList[i+4]}");
            }
            Console.WriteLine("-----------------------------------");
        }
    }
}