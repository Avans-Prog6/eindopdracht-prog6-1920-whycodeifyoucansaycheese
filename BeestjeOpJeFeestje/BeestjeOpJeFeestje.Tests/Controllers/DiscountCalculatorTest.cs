using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BeestjeOpJeFeestje.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class DiscountCalculatorTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void CalculateCharacterDiscount_DiscountFalse_Test()
        {
            //1. Arrange
            var calc = new DiscountCalculator();
            var bono = "bono";
            var bonoDiscountSHouldBe = 0;

            //2. Act
            var actualResult = calc.CalculateCharacterDiscount(bono);

            //3. Assert
            Assert.AreEqual(bonoDiscountSHouldBe, actualResult);
        }

        [TestMethod]
        public void CalculateCharacterDiscount_DiscountTrue_Test()
        {
            //1. Arrange
            var calc = new DiscountCalculator();
            var abc = "abc";
            var abcDiscountShouldBe = 6;

            //2. Act
            var actualResult = calc.CalculateCharacterDiscount(abc);

            //3. Assert
            Assert.AreEqual(abcDiscountShouldBe, actualResult);
        }

        [TestMethod]
        public void DuckDiscount_DuckDiscountTrue_Test()
        {
            //1. Arrange
            var calc = new DiscountCalculator();
            var duckName = "Eend";
            var random = new Random();

            //2. Act
            var result = calc.DuckDiscount(duckName);

            //3. Assert
        }
    }
}
