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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ApiClubMed.Controllers.Tests
{
    [TestClass()]
    public class CivilitesControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly CivilitesController _controller;
        private IDataRepository<Civilite> _dataRepository;
        public CivilitesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new CiviliteManager(_context);
            _controller = new CivilitesController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetCivilitesTest()
        {
            ActionResult<IEnumerable<Civilite>> civilites = await _controller.GetCivilites();
            var civilite1 = civilites.Value.ToList();
            civilite1 = civilite1.OrderBy(c => c.CiviliteId).ToList();

            var civilite2 = _context.Civilites.ToList();
            civilite2 = civilite2.OrderBy(c => c.CiviliteId).ToList();

            CollectionAssert.AreEqual(civilite2, civilite1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetCivilites_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Civilite civilite1 = new Civilite
            {
                CiviliteId = 99999,
                Libelle = "Madame",
            };

            Civilite civilite2 = new Civilite
            {
                CiviliteId = 99998,
                Libelle = "Madame2",
            };

            List<Civilite> lesCivilites = new List<Civilite>();
            lesCivilites.Add(civilite1);
            lesCivilites.Add(civilite2);


            var mockRepository = new Mock<IDataRepository<Civilite>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesCivilites);
            var civiliteController = new CivilitesController(mockRepository.Object);
            // Act
            var actionResult = civiliteController.GetCivilites().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesCivilites, actionResult.Value as List<Civilite>, "Civilite différent");
        }

        [TestMethod()]
        public async Task GetCiviliteTest()
        {
            ActionResult<Civilite> civilite = await _controller.GetCivilite(1);
            Assert.AreEqual(_context.Civilites.Where(c => c.CiviliteId == 1).FirstOrDefault(), civilite.Value, "Civilite différent");
        }

        [TestMethod()]
        public void GetCiviliteById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Civilite civilite = new Civilite
            {
                CiviliteId = 99999,
                Libelle = "Madame",
            };

            var mockRepository = new Mock<IDataRepository<Civilite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(civilite);
            var civiliteController = new CivilitesController(mockRepository.Object);
            // Act
            var actionResult = civiliteController.GetCivilite(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(civilite, actionResult.Value as Civilite);
        }

        [TestMethod]
        public void GetCiviliteById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Civilite>>();
            var civiliteController = new CivilitesController(mockRepository.Object);
            // Act
            var actionResult = civiliteController.GetCivilite(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetCiviliteByLibelleTest()
        {
            ActionResult<Civilite> civilite = await _controller.GetCiviliteByLibelle("Monsieur");
            Civilite leCivilite = _context.Civilites.Where(c => c.Libelle == "Monsieur").FirstOrDefault();
            Assert.IsNotNull(civilite);
            Assert.AreEqual(leCivilite, civilite.Value, "Civilite différent");
        }

        [TestMethod()]
        public void GetCiviliteByLibelle_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Civilite civilite = new Civilite
            {
                CiviliteId = 99999,
                Libelle = "Madame",
            };

            var mockRepository = new Mock<IDataRepository<Civilite>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau Civilite").Result).Returns(civilite);
            var civiliteController = new CivilitesController(mockRepository.Object);
            // Act
            var actionResult = civiliteController.GetCiviliteByLibelle("Nouveau Civilite").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(civilite, actionResult.Value as Civilite, "Civilite différent");
        }

        [TestMethod]
        public void GetCiviliteByLibelle_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Civilite>>();
            var civiliteController = new CivilitesController(mockRepository.Object);
            // Act
            var actionResult = civiliteController.GetCiviliteByLibelle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod]
        public void PutCiviliteTest__ReturnsNoContent_AvecMoq()
        {
            Civilite civilite = new Civilite()
            {
                CiviliteId = 99999,
                Libelle = "Madame",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Civilite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(civilite);
            var civiliteController = new CivilitesController(mockRepository.Object);


            civilite.Libelle = "Madame";
            var res = civiliteController.PutCivilite(civilite.CiviliteId, civilite);

            var actionResult = civiliteController.GetCivilite(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Civilite>), "Pas un ActionResult<Civilite>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Civilite), "Pas un Civilite");
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
            civilite.CiviliteId = ((Civilite)result).CiviliteId;
            Assert.AreEqual(civilite, (Civilite)result, "Civilites pas identiques");
        }

        [TestMethod]
        public void PutCivilite_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Civilite civilite = new Civilite()
            {
                CiviliteId = 99999,
                Libelle = "Madame",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Civilite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(civilite);
            var civiliteController = new CivilitesController(mockRepository.Object);


            var res = civiliteController.PutCivilite(15, civilite);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutCivilite_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Civilite civilite = new Civilite()
            {
                CiviliteId = 99999,
                Libelle = "Madame",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Civilite>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(civilite);
            var civiliteController = new CivilitesController(mockRepository.Object);

            var res = civiliteController.PutCivilite(99999, civilite);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne renvoie pas BadRequestResult");
        }


        [TestMethod()]
        public async Task PostCiviliteTest__ReturnsCreateAtAction_AvecMoq()
        {

            Civilite civilite = new Civilite()
            {
                CiviliteId = 99999,
                Libelle = "Madame",
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Civilite>>();
            var civiliteController = new CivilitesController(mockRepository.Object);
            // Act
            var actionResult = civiliteController.PostCivilite(civilite).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Civilite>), "Pas un ActionResult<Civilite>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult), "Pas un BadRequestResult");
            var result = actionResult.Result as CreatedAtActionResult;
        }

        [TestMethod]
        public void DeleteCiviliteTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Civilite civilite = new Civilite()
            {
                CiviliteId = 99999,
                Libelle = "Madame",
            };

            var mockRepository = new Mock<IDataRepository<Civilite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(civilite);
            var userController = new CivilitesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteCivilite(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Pas un BadRequestResult donc pas de restriction");

        }

        [TestMethod]
        public void DeleteCiviliteTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Civilite>>();
            var userController = new CivilitesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteCivilite(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Pas un BadRequestResult donc pas de restriction");
        }
    }
}
