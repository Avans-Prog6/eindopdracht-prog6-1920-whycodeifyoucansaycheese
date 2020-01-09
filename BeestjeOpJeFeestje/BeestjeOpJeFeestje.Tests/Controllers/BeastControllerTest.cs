using System;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Domain.Interface_Repositories;
using BeestjeOpJeFeestje.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class BeastControllerTest
    {

        private Mock<IBoekingRepository> _boekingsRepository;
        private Mock<IBeastRepository> _beastRepository;
        private Mock<IAccessoryRepository> _accessoryRepository;
        private Mock<IContactpersonRepository> _contactpersonRepository;
        private BeastController _beastcontroller;
        [TestInitialize]
        public void Init()
        {
            _boekingsRepository = new Mock<IBoekingRepository>();
            _beastRepository = new Mock<IBeastRepository>();
            _accessoryRepository = new Mock<IAccessoryRepository>();
            _contactpersonRepository = new Mock<IContactpersonRepository>();
        }
        [TestMethod]
        public void CreateBeast_Succeed_Test()
        {
            //1. Arrange
            _beastcontroller = new BeastController(_beastRepository.Object, _accessoryRepository.Object, _boekingsRepository.Object);

            var Beast = new BeastVM { Name = "Leeuw" };

            //2. Act
            _beastcontroller.Create(Beast);

            //3.Assert

            _beastRepository.Verify(b => b.Add(Beast.Beast), Times.Once());
        }
    }
}
