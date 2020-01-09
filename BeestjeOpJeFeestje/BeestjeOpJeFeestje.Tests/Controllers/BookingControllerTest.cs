using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using BeestjeOpJeFeestje.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class BookingControllerTest
    {
        private Mock<IBoekingRepository> _boekingsRepository;
        private Mock<IBeastRepository> _beastRepository;
        private Mock<IAccessoryRepository> _accessoryRepository;
        private Mock<IContactpersonRepository> _contactpersonRepository;
        //private Mock<ApplicationUserManager> _userManager;
        //private Mock<ApplicationSignInManager> _signInManager;
        private BookingController _bookingscontroller;
        //private Mock<IUserStore<ApplicationUser>> _userStore;
        //private Mock<IAuthenticationManager> _authManager;
        [TestInitialize]
        public void Init()
        {
            _boekingsRepository = new Mock<IBoekingRepository>();
            _beastRepository = new Mock<IBeastRepository>();
            _accessoryRepository = new Mock<IAccessoryRepository>();
            _contactpersonRepository = new Mock<IContactpersonRepository>();
        }
        #region Rules
        [TestMethod]
        public void DateInWeekend_True_Test()
        {
            //1. Arrange
            Mock<BookingVM> booking = new Mock<BookingVM>();
            var date = DateTime.Parse("16/02/2008");
            booking.Object.Date = date;
            //2. Act
            bool isTrue = Validator.IsWeekend(booking.Object);

            //3. Assert
            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void DateInWeekend_False_Test()
        {
            //1. Arrange
            Mock<BookingVM> booking = new Mock<BookingVM>();
            var date = DateTime.Parse("15/02/2008");
            booking.Object.Date = date;
            //2. Act
            bool isFalse = Validator.IsWeekend(booking.Object);

            //3. Assert
            Assert.IsFalse(isFalse);
        }

        [TestMethod]
        public void ExcludeDesert_True_Test()
        {
            //1. Arrange
            Mock<BookingVM> booking = new Mock<BookingVM>();
            var date = DateTime.Parse("15/02/2008");
            booking.Object.Date = date;
            //2. Act
            bool isTrue = Validator.ExcludeDesert(booking.Object);

            //3. Assert
            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void ExcludeSnow_True_Test()
        {
            //1. Arrange
            Mock<BookingVM> booking = new Mock<BookingVM>();
            var date = DateTime.Parse("15/06/2008");
            booking.Object.Date = date;
            //2. Act
            bool isTrue = Validator.ExcludeSnow(booking.Object);

            //3. Assert
            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void AddLion_NoFarmAnimalsInList_Test()
        {
            //1. Arrange
            var existingBooking = new Booking { ID = 1, Date = DateTime.Now.AddDays(1) };
            var list = new List<Beast>();
            var beast = new Beast { Name = "Leeuw" };
            //list.Add(beast);

            beast.Booking.Add(existingBooking);
            _boekingsRepository.Setup(b => b.TempBooking).Returns(new BookingVM { ID = 2, Date = DateTime.Now });
            _beastRepository.Setup(b => b.GetAll()).Returns(GetListLion());
            _bookingscontroller = new BookingController(_boekingsRepository.Object, _beastRepository.Object, _accessoryRepository.Object, _contactpersonRepository.Object);


            //2. Act
            var result = (RedirectToRouteResult)  _bookingscontroller.AddCheckedAnimal(new BeastVM(beast));

            //3. Assert
            _beastRepository.VerifySet(m => m.ExcludeFarm = true);
        }

        [TestMethod]
        public void AddPolarBear_NoFarmAnimalsInList_Test()
        {
            //1. Arrange
            var existingBooking = new Booking { ID = 1, Date = DateTime.Now.AddDays(1) };
            var list = new List<Beast>();
            var beast = new Beast { Name = "Ijsbeer" };
            //list.Add(beast);

            beast.Booking.Add(existingBooking);
            _boekingsRepository.Setup(b => b.TempBooking).Returns(new BookingVM { ID = 2, Date = DateTime.Now });
            _beastRepository.Setup(b => b.GetAll()).Returns(GetListLion());
            _bookingscontroller = new BookingController(_boekingsRepository.Object, _beastRepository.Object, _accessoryRepository.Object, _contactpersonRepository.Object);


            //2. Act
            var result = (RedirectToRouteResult)_bookingscontroller.AddCheckedAnimal(new BeastVM(beast));

            //3. Assert
            _beastRepository.VerifySet(m => m.ExcludeFarm = true);
        }

        [TestMethod]
        public void AddCow_ExcludePolarLionIsCalled_Test()
        {
            //1. Arrange
            var existingBooking = new Booking { ID = 1, Date = DateTime.Now.AddDays(1) };
            var list = new List<Beast>();
            var beast = new Beast { Name = "Koe" , Type = "Boerderij"};
            //list.Add(beast);

            beast.Booking.Add(existingBooking);
            _boekingsRepository.Setup(b => b.TempBooking).Returns(new BookingVM { ID = 2, Date = DateTime.Now });
            _beastRepository.Setup(b => b.GetAll()).Returns(GetListLion());
            _bookingscontroller = new BookingController(_boekingsRepository.Object, _beastRepository.Object, _accessoryRepository.Object, _contactpersonRepository.Object);


            //2. Act
            _bookingscontroller.AddCheckedAnimal(new BeastVM(beast));

            //3. Assert
            _beastRepository.VerifySet(m => m.ExcludePolarLion = true);
        }



        public List<Beast> GetListLion()
        {
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
            return beasts;
        }
        #endregion

        [TestMethod]

        public void AddCheckAnimalRedirect_Step1_Test()
        {
            //1. Arrange
            _boekingsRepository.SetupGet(b => b.TempBooking).Returns(new BookingVM());
            var Controller = new BookingController(_boekingsRepository.Object, _beastRepository.Object, _accessoryRepository.Object, _contactpersonRepository.Object);
            var Beast = new BeastVM { Name = "Leeuw" };

            //2. Act
            var result = (RedirectToRouteResult)Controller.AddCheckedAnimal(Beast);
            result.RouteValues["action"].Equals("Step1");
            //result.RouteValues["controller"].Equals("Booking");
            //3. Assert

            Assert.AreEqual("Step1", result.RouteValues["action"]);
            //Assert.AreEqual("Booking", result.RouteValues["controller"]);
        }





    }
}
