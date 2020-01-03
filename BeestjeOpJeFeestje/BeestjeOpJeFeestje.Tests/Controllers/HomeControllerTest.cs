using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeestjeOpJeFeestje;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Domain.Repositories;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(new BoekingRepository(new Domain.BeesteOpJeFeestjeEntities()));

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
