using System;
using System.Collections.Generic;
using System.Linq;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Type = BeestjeOpJeFeestje.Domain.Type;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class DiscountCalculatorTest
    {

        private DiscountCalculator calc;
        private Mock<IBoekingRepository> _boekingsRepository;

        [TestInitialize]
        public void Init()
        {
            _boekingsRepository = new Mock<IBoekingRepository>();
        }
        [TestMethod]
        public void TypeDiscount_HasDiscount_Test()
        {
            //1. Arange
            calc = new DiscountCalculator();
            Booking booking = GetSampleBooking();

            //2. Act

            var discount = calc.TypeDiscount(booking.Beast.ToList());

            //3. Assert

            Assert.AreEqual(10, discount.PercentageDiscount);
        }
        [TestMethod]
        public void TypeDiscount_NoDiscount_Test()
        {
            //1. Arange
            calc = new DiscountCalculator();
            Booking booking = GetBadSampleBooking();

            //2. Act

            var discount = calc.TypeDiscount(booking.Beast.ToList());

            //3. Assert

            Assert.IsNull(discount);
        }

        private Booking GetSampleBooking()
        {
            Booking booking = new Booking();
            List<Beast> beasts = new List<Beast>
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
            booking.Beast = beasts;
            return booking;
        }

        private Booking GetBadSampleBooking()
        {
            Booking booking = new Booking();
            List<Beast> beasts = new List<Beast>
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
                    Name = "Cobra",
                    Price = 100,
                    Type = "Woestijn"
                }
            };
            booking.Beast = beasts;
            return booking;
        }
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
