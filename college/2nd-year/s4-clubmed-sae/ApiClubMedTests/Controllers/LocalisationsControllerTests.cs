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
    public class LocalisationsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly LocalisationsController _controller;
        private IDataRepository<Localisation> _dataRepository;
        public LocalisationsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new LocalisationManager(_context);
            _controller = new LocalisationsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetLocalisationsTest()
        {
            ActionResult<IEnumerable<Localisation>> localisations = await _controller.GetLocalisations();
            var localisations1 = localisations.Value.ToList();
            localisations1 = localisations1.OrderBy(l => l.LocalisationId).ToList();

            var localisations2 = _context.Localisations.ToList();
            localisations2 = localisations2.OrderBy(l => l.LocalisationId).ToList();

            CollectionAssert.AreEqual(localisations2, localisations1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetLocalisations_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Localisation localisation1 = new Localisation
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };

            Localisation localisation2 = new Localisation
            {
                LocalisationId = 99998,
                Nom = "Nouvelle Localisation2"
            };


            List<Localisation> lesLocalisations = new List<Localisation>();
            lesLocalisations.Add(localisation1);
            lesLocalisations.Add(localisation2);


            var mockRepository = new Mock<IDataRepository<Localisation>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesLocalisations);
            var localisationController = new LocalisationsController(mockRepository.Object);
            // Act
            var actionResult = localisationController.GetLocalisations().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesLocalisations, actionResult.Value as List<Localisation>, "Localisation différent");
        }

        [TestMethod()]
        public async Task GetLocalisationTest()
        {
            ActionResult<Localisation> localisation = await _controller.GetLocalisation(1);
            Assert.AreEqual(_context.Localisations.Where(c => c.LocalisationId == 1).FirstOrDefault(), localisation.Value, "Localisation différent");
        }

        [TestMethod()]
        public void GetLocalisationById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Localisation localisation = new Localisation
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(localisation);
            var localisationController = new LocalisationsController(mockRepository.Object);
            // Act
            var actionResult = localisationController.GetLocalisation(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(localisation, actionResult.Value as Localisation);
        }

        [TestMethod]
        public void GetLocalisationById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            var localisationController = new LocalisationsController(mockRepository.Object);
            // Act
            var actionResult = localisationController.GetLocalisation(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetLocalisationByNameTest()
        {
            ActionResult<Localisation> localisation = await _controller.GetLocalisationByName("Europe");
            Localisation laLocalisation = _context.Localisations.Where(c => c.Nom == "Europe").FirstOrDefault();
            Assert.IsNotNull(laLocalisation);
            Assert.AreEqual(laLocalisation, localisation.Value, "Localisation différent");
        }

        [TestMethod()]
        public void GetLocalisationByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Localisation localisation = new Localisation
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouvelle Localisation").Result).Returns(localisation);
            var localisationController = new LocalisationsController(mockRepository.Object);
            // Act
            var actionResult = localisationController.GetLocalisationByName("Nouvelle Localisation").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(localisation, actionResult.Value as Localisation, "Localisation différent");
        }

        [TestMethod]
        public void GetLocalisationByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            var localisationController = new LocalisationsController(mockRepository.Object);
            // Act
            var actionResult = localisationController.GetLocalisationByName("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutLocalisationTest()
        {
            Localisation localisation = await _context.Localisations.FindAsync(1);

            string nomOriginal = localisation.Nom;

            localisation.Nom = "Le test";
            await _controller.PutLocalisation(localisation.LocalisationId, localisation);
            Localisation modifie = await _context.Localisations.FindAsync(1);
            Assert.AreEqual(localisation, modifie, "Localisation différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutLocalisation(modifie.LocalisationId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutLocalisationTest__ReturnsNoContent_AvecMoq()
        {
            Localisation localisation = new Localisation()
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(localisation);
            var localisationController = new LocalisationsController(mockRepository.Object);


            localisation.Nom = "a";
            var res = localisationController.PutLocalisation(localisation.LocalisationId, localisation);

            var actionResult = localisationController.GetLocalisation(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Localisation>), "Pas un ActionResult<Localisation>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Localisation), "Pas un Localisation");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            localisation.LocalisationId = ((Localisation)result).LocalisationId;
            Assert.AreEqual(localisation, (Localisation)result, "Localisations pas identiques");
        }

        [TestMethod]
        public void PutLocalisation_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Localisation localisation = new Localisation()
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(localisation);
            var localisationController = new LocalisationsController(mockRepository.Object);


            var res = localisationController.PutLocalisation(15, localisation);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutLocalisation_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Localisation localisation = new Localisation()
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(localisation);
            var localisationController = new LocalisationsController(mockRepository.Object);


            var res = localisationController.PutLocalisation(99999, localisation);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostLocalisationTest()
        {
            Localisation localisation = new Localisation()
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };

            var res = await _controller.PostLocalisation(localisation);
            Localisation add = _context.Localisations.Where(c => c.LocalisationId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Localisation ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteLocalisation(add.LocalisationId);
        }

        [TestMethod()]
        public async Task PostLocalisationTest__ReturnsCreateAtAction_AvecMoq()
        {

            Localisation localisation = new Localisation()
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            var localisationController = new LocalisationsController(mockRepository.Object);
            // Act
            var actionResult = localisationController.PostLocalisation(localisation).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Localisation>), "Pas un ActionResult<Localisation>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Localisation), "Pas un Localisation");
            localisation.LocalisationId = ((Localisation)result.Value).LocalisationId;
            Assert.AreEqual(localisation, (Localisation)result.Value, "Localisations pas identiques");
        }

        [TestMethod()]
        public async Task DeleteLocalisationTest()
        {
            Localisation localisation = new Localisation()
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };
            EntityEntry<Localisation> res = _context.Localisations.Add(localisation);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteLocalisation(res.Entity.LocalisationId);

            Localisation domaine = _context.Localisations.Where(u => u.LocalisationId == res.Entity.LocalisationId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteLocalisationTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Localisation localisation = new Localisation()
            {
                LocalisationId = 99999,
                Nom = "Nouvelle Localisation"
            };
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(localisation);
            var userController = new LocalisationsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteLocalisation(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteLocalisationTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Localisation>>();
            var userController = new LocalisationsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteLocalisation(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
