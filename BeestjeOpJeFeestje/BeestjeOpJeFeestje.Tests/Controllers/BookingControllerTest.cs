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

        [TestMethod]
        public void BookPinguinInWeekend_False_Test()
        {
            //1. Arrange
            Mock<BeastVM> pin = new Mock<BeastVM>();
            pin.Setup(p => p.Name == "Pinguin");
            var list = _beastRepository.Object.BeastsAvailable(DateTime.Parse("16/02/2008")).ToList();
            //2. Act

            bool IsIn = list.Contains(pin.Object);

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





    }
}
