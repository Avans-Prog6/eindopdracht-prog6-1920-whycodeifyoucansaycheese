using System;
using System.Collections.Generic;
using System.Linq;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class DiscountCalculatorTest
    {
        private DiscountCalculator _calc;
        
        [TestMethod]
        public void TypeDiscount_DiscountTrue_Test()
        {
            //1. Arange
            _calc = new DiscountCalculator();

            var beasts = new List<Beast>
            {
                new Beast
                {
                    Name = "Koe",
                    Price = 100,
                    Type = "Boerderij"
                },
                new Beast
                {
                    Name = "Paard",
                    Price = 100,
                    Type = "Boerderij"
                },
                new Beast
                {
                    Name = "Varken",
                    Price = 100,
                    Type = "Boerderij"
                }
            };

            //2. Act
            var result = _calc.TypeDiscount(beasts);

            //3. Assert
            Assert.AreEqual(10, result.PercentageDiscount);


            /*            var beast1 = new Mock<Beast>();
            var beast2 = new Mock<Beast>();
            var beast3 = new Mock<Beast>();

            beast1.Object.Type = "Boerderij";
            beast2.Object.Type = "Boerderij";
            beast3.Object.Type = "Boerderij";

           List<Mock<Beast>> beasts = new List<Mock<Beast>>
            {
                beast1,
                beast2,
                beast3
            };*/

        }
        [TestMethod]
        public void TypeDiscount_DiscountFalse_Test()
        {
            //1. Arange
            _calc = new DiscountCalculator();

            var beasts = new List<Beast>
            {
                new Beast
                {
                    Name = "Koe",
                    Price = 100,
                    Type = "Boerderij"
                },
                new Beast
                {
                    Name = "Paard",
                    Price = 100,
                    Type = "Boerderij"
                },
                new Beast
                {
                    Name = "Hagedis",
                    Price = 200,
                    Type = "Woestijn"
                }
            };

            //2. Act
            var result = _calc.TypeDiscount(beasts);

            //3. Assert
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void CalculateCharacterDiscount_DiscountFalse_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const string bono = "bono";
            const int bonoDiscountShouldBe = 0;

            //2. Act
            var actualResult = _calc.CalculateCharacterDiscount(bono);

            //3. Assert
            Assert.AreEqual(bonoDiscountShouldBe, actualResult);
        }

        [TestMethod]
        public void CalculateCharacterDiscount_DiscountTrue_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const string abc = "abc";
            const int abcDiscountShouldBe = 6;

            //2. Act
            var actualResult = _calc.CalculateCharacterDiscount(abc);

            //3. Assert
            Assert.AreEqual(abcDiscountShouldBe, actualResult);
        }

        [TestMethod]
        public void DuckDiscount_AppliesDuckDiscountAppropriately_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const string duckName = "Eend";
            const int expectedDuckDiscount = 50;

            //2. Act
            var discount = _calc.DuckDiscount(duckName);

            //3. Assert
            if (_calc.DuckDiscountBool)
                Assert.AreEqual(expectedDuckDiscount, discount.PercentageDiscount);
            else
                Assert.IsNull(discount);
        }

        [TestMethod]
        public void DateDiscount_MondayTuesdayAppliesDiscount_Test()
        {
            //1. Arrange
            _calc = new DiscountCalculator();
            const int expectedDateDiscount = 15;
            var monday = new DateTime(2020, 01, 06);
            var tuesday = new DateTime(2020, 01, 07);
            var wednesday = new DateTime(2020, 01, 08);

            //2. Act
            var resultMonday = _calc.DateDiscount(monday);
            var resultTuesday = _calc.DateDiscount(tuesday);
            var resultWednesday = _calc.DateDiscount(wednesday);

            //3. Assert
            Assert.AreEqual(expectedDateDiscount, resultMonday.PercentageDiscount);
            Assert.AreEqual(expectedDateDiscount, resultTuesday.PercentageDiscount);
            Assert.IsNull(resultWednesday);
        }
    }
}
