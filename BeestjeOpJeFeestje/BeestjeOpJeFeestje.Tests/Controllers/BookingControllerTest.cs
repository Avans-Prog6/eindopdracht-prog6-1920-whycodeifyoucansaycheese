using System;
using System.Linq;
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
            Mock<BeastVM> pin = new Mock<BeastVM>();
            pin.Setup(p => p.Name == "Pinguin");
            var list = _beastRepository.Object.BeastsAvailable(DateTime.Parse("16/02/2008")).ToList();
            //2. Act

            bool IsIn = list.Contains(pin.Object);

        }

    
    }
}
