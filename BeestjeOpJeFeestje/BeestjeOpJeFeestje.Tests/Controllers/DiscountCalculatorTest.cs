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
        public void CalculateCharacterDiscount_NoDiscount_Test()
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
        public void CalculateCharacterDiscount_SomeDiscount_Test()
        {
            //1. Arrange
            var calc = new DiscountCalculator();
            var aap = "abc";
            var aapDiscountShouldBe = 6;

            //2. Act
            var actualResult = calc.CalculateCharacterDiscount(aap);

            //3. Assert
            Assert.AreEqual(aapDiscountShouldBe, actualResult);
        }
    }
}
