using System;
using System.Linq;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Domain;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class BookingControllerTest
    {
        private Mock<IBoekingRepository> _boekingsRepository;
        private Mock<IBeastRepository> _beastRepository;
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

        }

        public void BookPinguinInWeekend_False_Test()
        {
            //1. Arrange
            Mock<Beast> pin = new Mock<Beast>();
            pin.Setup(p => p.Name == "Pinguin");
            var list = _beastRepository.Object.BeastsAvailable(DateTime.Parse("16/02/2008")).ToList();
            //2. Act

            bool IsIn = list.Contains(pin.Object);

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
                    Name = "Pinguin",
                    Price = 100,
                    Type = "Woestijn"
                }
            };
            booking.Beast = beasts;
            return booking;
        }
    }
}
