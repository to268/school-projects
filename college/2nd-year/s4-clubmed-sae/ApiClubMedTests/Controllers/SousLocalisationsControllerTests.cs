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
    public class SousLocalisationsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly SousLocalisationsController _controller;
        private IDataRepository<SousLocalisation> _dataRepository;
        public SousLocalisationsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new SousLocalisationManager(_context);
            _controller = new SousLocalisationsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetSousLocalisationsTest()
        {
            ActionResult<IEnumerable<SousLocalisation>> sousLocalisations = await _controller.GetSousLocalisations();
            var sousLocalisations1 = sousLocalisations.Value.ToList();
            sousLocalisations1 = sousLocalisations1.OrderBy(sl => sl.SousLocalisationId).ToList();

            var sousLocalisations2 = _context.SousLocalisations.ToList();
            sousLocalisations2 = sousLocalisations2.OrderBy(sl => sl.SousLocalisationId).ToList();

            CollectionAssert.AreEqual(sousLocalisations2, sousLocalisations1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetSousLocalisations_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            SousLocalisation sousLocalisation1 = new SousLocalisation
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };

            SousLocalisation sousLocalisation2 = new SousLocalisation
            {
                SousLocalisationId = 99998,
                Nom = "Nouvelle SousLocalisation2"
            };


            List<SousLocalisation> lesSousLocalisations = new List<SousLocalisation>();
            lesSousLocalisations.Add(sousLocalisation1);
            lesSousLocalisations.Add(sousLocalisation2);


            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesSousLocalisations);
            var sousLocalisationController = new SousLocalisationsController(mockRepository.Object);
            // Act
            var actionResult = sousLocalisationController.GetSousLocalisations().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesSousLocalisations, actionResult.Value as List<SousLocalisation>, "SousLocalisation différent");
        }

        [TestMethod()]
        public async Task GetSousLocalisationTest()
        {
            ActionResult<SousLocalisation> sousLocalisation = await _controller.GetSousLocalisation(1);
            Assert.AreEqual(_context.SousLocalisations.Where(c => c.SousLocalisationId == 1).FirstOrDefault(), sousLocalisation.Value, "SousLocalisation différent");
        }

        [TestMethod()]
        public void GetSousLocalisationById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            SousLocalisation sousLocalisation = new SousLocalisation
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(sousLocalisation);
            var sousLocalisationController = new SousLocalisationsController(mockRepository.Object);
            // Act
            var actionResult = sousLocalisationController.GetSousLocalisation(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(sousLocalisation, actionResult.Value as SousLocalisation);
        }

        [TestMethod]
        public void GetSousLocalisationById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            var sousLocalisationController = new SousLocalisationsController(mockRepository.Object);
            // Act
            var actionResult = sousLocalisationController.GetSousLocalisation(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetSousLocalisationByNameTest()
        {
            ActionResult<SousLocalisation> sousLocalisation = await _controller.GetSousLocalisationByName("France");
            SousLocalisation laSousLocalisation = _context.SousLocalisations.Where(c => c.Nom == "France").FirstOrDefault();
            Assert.IsNotNull(laSousLocalisation);
            Assert.AreEqual(laSousLocalisation, sousLocalisation.Value, "SousLocalisation différent");
        }

        [TestMethod()]
        public void GetSousLocalisationByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            SousLocalisation sousLocalisation = new SousLocalisation
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouvelle SousLocalisation").Result).Returns(sousLocalisation);
            var sousLocalisationController = new SousLocalisationsController(mockRepository.Object);
            // Act
            var actionResult = sousLocalisationController.GetSousLocalisationByName("Nouvelle SousLocalisation").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(sousLocalisation, actionResult.Value as SousLocalisation, "SousLocalisation différent");
        }

        [TestMethod]
        public void GetSousLocalisationByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            var sousLocalisationController = new SousLocalisationsController(mockRepository.Object);
            // Act
            var actionResult = sousLocalisationController.GetSousLocalisationByName("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutSousLocalisationTest()
        {
            SousLocalisation sousLocalisation = await _context.SousLocalisations.FindAsync(1);

            string nomOriginal = sousLocalisation.Nom;

            sousLocalisation.Nom = "Le test";
            await _controller.PutSousLocalisation(sousLocalisation.SousLocalisationId, sousLocalisation);
            SousLocalisation modifie = await _context.SousLocalisations.FindAsync(1);
            Assert.AreEqual(sousLocalisation, modifie, "SousLocalisation différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutSousLocalisation(modifie.SousLocalisationId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutSousLocalisationTest__ReturnsNoContent_AvecMoq()
        {
            SousLocalisation sousLocalisation = new SousLocalisation()
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(sousLocalisation);
            var sousLocalisationController = new SousLocalisationsController(mockRepository.Object);


            sousLocalisation.Nom = "a";
            var res = sousLocalisationController.PutSousLocalisation(sousLocalisation.SousLocalisationId, sousLocalisation);

            var actionResult = sousLocalisationController.GetSousLocalisation(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<SousLocalisation>), "Pas un ActionResult<SousLocalisation>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(SousLocalisation), "Pas un SousLocalisation");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            sousLocalisation.SousLocalisationId = ((SousLocalisation)result).SousLocalisationId;
            Assert.AreEqual(sousLocalisation, (SousLocalisation)result, "SousLocalisations pas identiques");
        }

        [TestMethod]
        public void PutSousLocalisation_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            SousLocalisation sousLocalisation = new SousLocalisation()
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(sousLocalisation);
            var sousLocalisationController = new SousLocalisationsController(mockRepository.Object);


            var res = sousLocalisationController.PutSousLocalisation(15, sousLocalisation);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutSousLocalisation_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            SousLocalisation sousLocalisation = new SousLocalisation()
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(sousLocalisation);
            var sousLocalisationController = new SousLocalisationsController(mockRepository.Object);


            var res = sousLocalisationController.PutSousLocalisation(99999, sousLocalisation);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostSousLocalisationTest()
        {
            SousLocalisation sousLocalisation = new SousLocalisation()
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };

            var res = await _controller.PostSousLocalisation(sousLocalisation);
            SousLocalisation add = _context.SousLocalisations.Where(c => c.SousLocalisationId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de SousLocalisation ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteSousLocalisation(add.SousLocalisationId);
        }

        [TestMethod()]
        public async Task PostSousLocalisationTest__ReturnsCreateAtAction_AvecMoq()
        {

            SousLocalisation sousLocalisation = new SousLocalisation()
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            var sousLocalisationController = new SousLocalisationsController(mockRepository.Object);
            // Act
            var actionResult = sousLocalisationController.PostSousLocalisation(sousLocalisation).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<SousLocalisation>), "Pas un ActionResult<SousLocalisation>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(SousLocalisation), "Pas un SousLocalisation");
            sousLocalisation.SousLocalisationId = ((SousLocalisation)result.Value).SousLocalisationId;
            Assert.AreEqual(sousLocalisation, (SousLocalisation)result.Value, "SousLocalisations pas identiques");
        }

        [TestMethod()]
        public async Task DeleteSousLocalisationTest()
        {
            SousLocalisation sousLocalisation = new SousLocalisation()
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };
            EntityEntry<SousLocalisation> res = _context.SousLocalisations.Add(sousLocalisation);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteSousLocalisation(res.Entity.SousLocalisationId);

            SousLocalisation domaine = _context.SousLocalisations.Where(u => u.SousLocalisationId == res.Entity.SousLocalisationId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteSousLocalisationTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            SousLocalisation sousLocalisation = new SousLocalisation()
            {
                SousLocalisationId = 99999,
                Nom = "Nouvelle SousLocalisation"
            };
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(sousLocalisation);
            var userController = new SousLocalisationsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteSousLocalisation(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteSousLocalisationTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<SousLocalisation>>();
            var userController = new SousLocalisationsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteSousLocalisation(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
