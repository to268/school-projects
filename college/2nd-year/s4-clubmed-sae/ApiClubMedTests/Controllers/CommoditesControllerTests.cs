using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiClubMed.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiClubMed.Models.DataManager;
using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace ApiClubMed.Controllers.Tests
{
    [TestClass()]
    public class CommoditesControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly CommoditesController _controller;
        private IDataRepository<Commodite> _dataRepository;
        public CommoditesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new CommoditeManager(_context);
            _controller = new CommoditesController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetCommoditesTest()
        {
            ActionResult<IEnumerable<Commodite>> commodites = await _controller.GetCommodites();
            var commodite1 = commodites.Value.ToList();
            commodite1 = commodite1.OrderBy(ap => ap.CommoditeId).ToList();

            var commodite2 = _context.Commodites.ToList();
            commodite2 = commodite2.OrderBy(ap => ap.CommoditeId).ToList();

            CollectionAssert.AreEqual(commodite2, commodite1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetCommodites_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Commodite commodite1 = new Commodite
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            Commodite commodite2 = new Commodite
            {
                CommoditeId = 99998,


                Nom = "Nouveau Commodite2",

            };

            List<Commodite> lesCommodites = new List<Commodite>();
            lesCommodites.Add(commodite1);
            lesCommodites.Add(commodite2);


            var mockRepository = new Mock<IDataRepository<Commodite>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesCommodites);
            var commoditeController = new CommoditesController(mockRepository.Object);
            // Act
            var actionResult = commoditeController.GetCommodites().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesCommodites, actionResult.Value as List<Commodite>, "Commodite différent");
        }

        [TestMethod()]
        public async Task GetCommoditeTest()
        {
            ActionResult<Commodite> commodite = await _controller.GetCommodite(1);
            Assert.AreEqual(_context.Commodites.Where(c => c.CommoditeId == 1).FirstOrDefault(), commodite.Value, "Commodite différent");
        }

        [TestMethod()]
        public void GetCommoditeById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Commodite commodite = new Commodite
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            var mockRepository = new Mock<IDataRepository<Commodite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(commodite);
            var commoditeController = new CommoditesController(mockRepository.Object);
            // Act
            var actionResult = commoditeController.GetCommodite(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(commodite, actionResult.Value as Commodite);
        }

        [TestMethod]
        public void GetCommoditeById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Commodite>>();
            var commoditeController = new CommoditesController(mockRepository.Object);
            // Act
            var actionResult = commoditeController.GetCommodite(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetCommoditeByNameTest()
        {
            ActionResult<Commodite> commodite = await _controller.GetCommoditeByName("Lit bébé (dès la réservation)");
            Commodite leCommodite = _context.Commodites.Where(c => c.Nom == "Lit bébé (dès la réservation)").FirstOrDefault();
            Assert.IsNotNull(commodite);
            Assert.AreEqual(leCommodite, commodite.Value, "Commodite différent");
        }

        [TestMethod()]
        public void GetCommoditeByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Commodite commodite = new Commodite
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            var mockRepository = new Mock<IDataRepository<Commodite>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau Commodite").Result).Returns(commodite);
            var commoditeController = new CommoditesController(mockRepository.Object);
            // Act
            var actionResult = commoditeController.GetCommoditeByName("Nouveau Commodite").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(commodite, actionResult.Value as Commodite, "Commodite différent");
        }

        [TestMethod]
        public void GetCommoditeByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Commodite>>();
            var commoditeController = new CommoditesController(mockRepository.Object);
            // Act
            var actionResult = commoditeController.GetCommoditeByName("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutCommoditeTest()
        {
            Commodite commodite = await _context.Commodites.FindAsync(1);

            string nomOriginal = commodite.Nom;

            commodite.Nom = "Le test";
            await _controller.PutCommodite(commodite.CommoditeId, commodite);
            Commodite modifie = await _context.Commodites.FindAsync(1);
            Assert.AreEqual(commodite, modifie, "Commodite différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutCommodite(modifie.CommoditeId, modifie);
        }

        [TestMethod]
        public void PutCommoditeTest__ReturnsNoContent_AvecMoq()
        {
            Commodite commodite = new Commodite()
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<Commodite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(commodite);
            var commoditeController = new CommoditesController(mockRepository.Object);


            commodite.Nom = "a";
            var res = commoditeController.PutCommodite(commodite.CommoditeId, commodite);

            var actionResult = commoditeController.GetCommodite(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Commodite>), "Pas un ActionResult<Commodite>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Commodite), "Pas un Commodite");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            commodite.CommoditeId = ((Commodite)result).CommoditeId;
            Assert.AreEqual(commodite, (Commodite)result, "Commodites pas identiques");
        }

        [TestMethod]
        public void PutCommodite_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Commodite commodite = new Commodite()
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<Commodite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(commodite);
            var commoditeController = new CommoditesController(mockRepository.Object);

            var res = commoditeController.PutCommodite(15, commodite);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutCommodite_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Commodite commodite = new Commodite()
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<Commodite>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(commodite);
            var commoditeController = new CommoditesController(mockRepository.Object);

            var res = commoditeController.PutCommodite(99999, commodite);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostCommoditeTest()
        {
            Commodite commodite = new Commodite()
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            var res = await _controller.PostCommodite(commodite);
            Commodite add = _context.Commodites.Where(c => c.CommoditeId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Commodite ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteCommodite(add.CommoditeId);
        }

        [TestMethod()]
        public async Task PostCommoditeTest__ReturnsCreateAtAction_AvecMoq()
        {

            Commodite commodite = new Commodite()
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Commodite>>();
            var commoditeController = new CommoditesController(mockRepository.Object);
            // Act
            var actionResult = commoditeController.PostCommodite(commodite).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Commodite>), "Pas un ActionResult<Commodite>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Commodite), "Pas un Commodite");
            commodite.CommoditeId = ((Commodite)result.Value).CommoditeId;
            Assert.AreEqual(commodite, (Commodite)result.Value, "Commodites pas identiques");
        }

        [TestMethod()]
        public async Task DeleteCommoditeTest()
        {
            Commodite commodite = new Commodite()
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            EntityEntry<Commodite> res = _context.Commodites.Add(commodite);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteCommodite(res.Entity.CommoditeId);

            Commodite commodite1 = _context.Commodites.Where(u => u.CommoditeId == res.Entity.CommoditeId).FirstOrDefault();

            Assert.IsNull(commodite1, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteCommoditeTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Commodite commodite = new Commodite()
            {
                CommoditeId = 99999,


                Nom = "Nouveau Commodite",

            };

            var mockRepository = new Mock<IDataRepository<Commodite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(commodite);
            var userController = new CommoditesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteCommodite(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteCommoditeTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Commodite>>();
            var userController = new CommoditesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteCommodite(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
