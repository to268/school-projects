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
    public class ResortsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly ResortsController _controller;
        private IDataRepository<Resort> _dataRepository;
        public ResortsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new ResortManager(_context);
            _controller = new ResortsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetResortsTest()
        {
            ActionResult<IEnumerable<Resort>> resorts = await _controller.GetResorts();
            var resorts1 = resorts.Value.ToList();
            resorts1.Sort();

            var resorts2 = _context.Resorts.ToList();
            resorts2.Sort();
            resorts2 = resorts2.Take(12).ToList();

            CollectionAssert.AreEqual(resorts2, resorts1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetResorts_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Resort resort1 = new Resort
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };

            Resort resort2 = new Resort
            {
                ResortId = 99998,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort2",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort2",
                LienPdfDocClub = "Nouveau Resort2",
                PrixDepart = 1
            };


            List<Resort> lesResorts = new List<Resort>();
            lesResorts.Add(resort1);
            lesResorts.Add(resort2);


            var mockRepository = new Mock<IDataRepository<Resort>>();
            mockRepository.Setup(x => x.GetPage(1, 12).Result).Returns(lesResorts);
            var resortController = new ResortsController(mockRepository.Object);
            // Act
            var actionResult = resortController.GetResorts(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesResorts, actionResult.Value as List<Resort>, "Resort différent");
        }

        [TestMethod()]
        public async Task GetResortTest()
        {
            ActionResult<Resort> resort = await _controller.GetResort(1);
            Assert.AreEqual(_context.Resorts.Where(c => c.ResortId == 1).FirstOrDefault(), resort.Value, "Resort différent");
        }

        [TestMethod()]
        public void GetResortById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Resort resort = new Resort
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };
            var mockRepository = new Mock<IDataRepository<Resort>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(resort);
            var resortController = new ResortsController(mockRepository.Object);
            // Act
            var actionResult = resortController.GetResort(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(resort, actionResult.Value as Resort);
        }

        [TestMethod]
        public void GetResortById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Resort>>();
            var resortController = new ResortsController(mockRepository.Object);
            // Act
            var actionResult = resortController.GetResort(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetResortByNameTest()
        {
            ActionResult<Resort> domainSkiable = await _controller.GetResortByName("La Rosière");
            Resort leResort = _context.Resorts.Where(c => c.Nom == "La Rosière").FirstOrDefault();
            Assert.IsNotNull(domainSkiable);
            Assert.AreEqual(leResort, domainSkiable.Value, "Resort différent");
        }

        [TestMethod()]
        public void GetResortByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Resort resort = new Resort
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };
            var mockRepository = new Mock<IDataRepository<Resort>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau Resort").Result).Returns(resort);
            var resortController = new ResortsController(mockRepository.Object);
            // Act
            var actionResult = resortController.GetResortByName("Nouveau Resort").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(resort, actionResult.Value as Resort, "Resort différent");
        }

        [TestMethod]
        public void GetResortByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Resort>>();
            var resortController = new ResortsController(mockRepository.Object);
            // Act
            var actionResult = resortController.GetResortByName("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutResortTest()
        {
            Resort resort = await _context.Resorts.FindAsync(1);

            string nomOriginal = resort.Nom;

            resort.Nom = "Le test";
            await _controller.PutResort(resort.ResortId, resort);
            Resort modifie = await _context.Resorts.FindAsync(1);
            Assert.AreEqual(resort, modifie, "Resort différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutResort(modifie.ResortId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutDomainSkiableTest__ReturnsNoContent_AvecMoq()
        {
            Resort resort = new Resort()
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Resort>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(resort);
            var resortController = new ResortsController(mockRepository.Object);


            resort.Nom = "a";
            var res = resortController.PutResort(resort.ResortId, resort);

            var actionResult = resortController.GetResort(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Resort>), "Pas un ActionResult<Resort>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Resort), "Pas un Resort");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            resort.ResortId = ((Resort)result).ResortId;
            Assert.AreEqual(resort, (Resort)result, "Resorts pas identiques");
        }

        [TestMethod]
        public void PutResort_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Resort resort = new Resort()
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Resort>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(resort);
            var resortController = new ResortsController(mockRepository.Object);


            var res = resortController.PutResort(15, resort);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutResort_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Resort resort = new Resort()
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Resort>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(resort);
            var resortController = new ResortsController(mockRepository.Object);


            var res = resortController.PutResort(99999, resort);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostResortTest()
        {
            Resort resort = new Resort()
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };

            var res = await _controller.PostResort(resort);
            Resort add = _context.Resorts.Where(c => c.ResortId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Resort ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteResort(add.ResortId);
        }

        [TestMethod()]
        public async Task PostResortTest__ReturnsCreateAtAction_AvecMoq()
        {

            Resort resort = new Resort()
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<Resort>>();
            var resortController = new ResortsController(mockRepository.Object);
            // Act
            var actionResult = resortController.PostResort(resort).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Resort>), "Pas un ActionResult<DomainSkiable>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Resort), "Pas un Resort");
            resort.ResortId = ((Resort)result.Value).ResortId;
            Assert.AreEqual(resort, (Resort)result.Value, "Resorts pas identiques");
        }

        [TestMethod()]
        public async Task DeleteResortTest()
        {
            Resort resort = new Resort()
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };
            EntityEntry<Resort> res = _context.Resorts.Add(resort);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteResort(res.Entity.ResortId);

            Resort leResort = _context.Resorts.Where(u => u.ResortId == res.Entity.ResortId).FirstOrDefault();

            Assert.IsNull(leResort, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteDomainSkiableTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Resort resort = new Resort()
            {
                ResortId = 99999,
                DomaineId = 1,
                LocalisationId = 1,
                SouslocalisationId = 1,
                Nom = "Nouveau Resort",
                MoyenneAvis = 0,
                DescriptionGen = "Nouveau Resort",
                LienPdfDocClub = "Nouveau Resort",
                PrixDepart = 1
            };
            var mockRepository = new Mock<IDataRepository<Resort>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(resort);
            var userController = new ResortsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteResort(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteDomainSkiableTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Resort>>();
            var userController = new ResortsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteResort(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}