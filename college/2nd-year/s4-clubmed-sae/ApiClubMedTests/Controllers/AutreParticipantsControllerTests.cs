using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiClubMed.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiClubMed.Models.Repository;
using Microsoft.EntityFrameworkCore;
using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NuGet.Protocol.Plugins;

namespace ApiClubMed.Controllers.Tests
{
    [TestClass()]
    public class AutreParticipantsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly AutreParticipantsController _controller;
        private IDataRepository<AutreParticipant> _dataRepository;
        public AutreParticipantsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new AutreParticipantManager(_context);
            _controller = new AutreParticipantsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetAutreParticipantsTest()
        {
            ActionResult<IEnumerable<AutreParticipant>> autreParticipants = await _controller.GetAutreParticipants();
            var autreParticipant1 = autreParticipants.Value.ToList();
            autreParticipant1 = autreParticipant1.OrderBy(ap => ap.AutreParticipantId).ToList();

            var autreParticipant2 = _context.AutreParticipants.ToList();
            autreParticipant2 = autreParticipant2.OrderBy(ap => ap.AutreParticipantId).ToList();

            CollectionAssert.AreEqual(autreParticipant2, autreParticipant1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetAutreParticipants_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            AutreParticipant autreParticipant1 = new AutreParticipant
            {
                AutreParticipantId = 99999,
                CiviliteId = 1,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            AutreParticipant autreParticipant2 = new AutreParticipant
            {
                AutreParticipantId = 99998,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant2",
                Nom = "Nouveau AutreParticipant2",
                DateNaissance = DateTime.UnixEpoch,
            };

            List<AutreParticipant> lesAutreParticipants = new List<AutreParticipant>();
            lesAutreParticipants.Add(autreParticipant1);
            lesAutreParticipants.Add(autreParticipant2);


            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesAutreParticipants);
            var autreParticipantController = new AutreParticipantsController(mockRepository.Object);
            // Act
            var actionResult = autreParticipantController.GetAutreParticipants().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesAutreParticipants, actionResult.Value as List<AutreParticipant>, "AutreParticipant différent");
        }

        [TestMethod()]
        public async Task GetAutreParticipantTest()
        {
            ActionResult<AutreParticipant> autreParticipant = await _controller.GetAutreParticipant(1);
            Assert.AreEqual(_context.AutreParticipants.Where(c => c.AutreParticipantId == 1).FirstOrDefault(), autreParticipant.Value, "AutreParticipant différent");
        }

        [TestMethod()]
        public void GetAutreParticipantById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            AutreParticipant autreParticipant = new AutreParticipant
            {
                AutreParticipantId = 99999,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(autreParticipant);
            var autreParticipantController = new AutreParticipantsController(mockRepository.Object);
            // Act
            var actionResult = autreParticipantController.GetAutreParticipant(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(autreParticipant, actionResult.Value as AutreParticipant);
        }

        [TestMethod]
        public void GetAutreParticipantById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            var autreParticipantController = new AutreParticipantsController(mockRepository.Object);
            // Act
            var actionResult = autreParticipantController.GetAutreParticipant(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetAutreParticipantByNameTest()
        {
            ActionResult<AutreParticipant> autreParticipant = await _controller.GetAutreParticipantByName("La Rosière");
            AutreParticipant leAutreParticipant = _context.AutreParticipants.Where(c => c.Nom == "La Rosière").FirstOrDefault();
            Assert.IsNotNull(autreParticipant);
            Assert.AreEqual(leAutreParticipant, autreParticipant.Value, "AutreParticipant différent");
        }

        [TestMethod()]
        public void GetAutreParticipantByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            AutreParticipant autreParticipant = new AutreParticipant
            {
                AutreParticipantId = 99999,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau AutreParticipant").Result).Returns(autreParticipant);
            var autreParticipantController = new AutreParticipantsController(mockRepository.Object);
            // Act
            var actionResult = autreParticipantController.GetAutreParticipantByName("Nouveau AutreParticipant").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(autreParticipant, actionResult.Value as AutreParticipant, "AutreParticipant différent");
        }

        [TestMethod]
        public void GetAutreParticipantByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            var autreParticipantController = new AutreParticipantsController(mockRepository.Object);
            // Act
            var actionResult = autreParticipantController.GetAutreParticipantByName("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutAutreParticipantTest()
        {
            AutreParticipant autreParticipant = await _context.AutreParticipants.FindAsync(1);

            string nomOriginal = autreParticipant.Nom;

            autreParticipant.Nom = "Le test";
            await _controller.PutAutreParticipant(autreParticipant.AutreParticipantId, autreParticipant);
            AutreParticipant modifie = await _context.AutreParticipants.FindAsync(1);
            Assert.AreEqual(autreParticipant, modifie, "AutreParticipant différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutAutreParticipant(modifie.AutreParticipantId, modifie);
        }

        [TestMethod]
        public void PutAutreParticipantTest__ReturnsNoContent_AvecMoq()
        {
            AutreParticipant autreParticipant = new AutreParticipant()
            {
                AutreParticipantId = 99999,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(autreParticipant);
            var autreParticipantController = new AutreParticipantsController(mockRepository.Object);


            autreParticipant.Nom = "a";
            var res = autreParticipantController.PutAutreParticipant(autreParticipant.AutreParticipantId, autreParticipant);

            var actionResult = autreParticipantController.GetAutreParticipant(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<AutreParticipant>), "Pas un ActionResult<AutreParticipant>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(AutreParticipant), "Pas un AutreParticipant");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            autreParticipant.AutreParticipantId = ((AutreParticipant)result).AutreParticipantId;
            Assert.AreEqual(autreParticipant, (AutreParticipant)result, "AutreParticipants pas identiques");
        }

        [TestMethod]
        public void PutAutreParticipant_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            AutreParticipant autreParticipant = new AutreParticipant()
            {
                AutreParticipantId = 99999,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(autreParticipant);
            var autreParticipantController = new AutreParticipantsController(mockRepository.Object);

            var res = autreParticipantController.PutAutreParticipant(15, autreParticipant);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutAutreParticipant_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            AutreParticipant autreParticipant = new AutreParticipant()
            {
                AutreParticipantId = 99999,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(autreParticipant);
            var autreParticipantController = new AutreParticipantsController(mockRepository.Object);

            var res = autreParticipantController.PutAutreParticipant(99999, autreParticipant);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostAutreParticipantTest()
        {
            AutreParticipant autreParticipant = new AutreParticipant()
            {
                AutreParticipantId = 99999,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            var res = await _controller.PostAutreParticipant(autreParticipant);
            AutreParticipant add = _context.AutreParticipants.Where(c => c.AutreParticipantId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de AutreParticipant ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteAutreParticipant(add.AutreParticipantId);
        }

        [TestMethod()]
        public async Task PostAutreParticipantTest__ReturnsCreateAtAction_AvecMoq()
        {

            AutreParticipant autreParticipant = new AutreParticipant()
            {
                AutreParticipantId = 99999,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            var autreParticipantController = new AutreParticipantsController(mockRepository.Object);
            // Act
            var actionResult = autreParticipantController.PostAutreParticipant(autreParticipant).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<AutreParticipant>), "Pas un ActionResult<AutreParticipant>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(AutreParticipant), "Pas un AutreParticipant");
            autreParticipant.AutreParticipantId = ((AutreParticipant)result.Value).AutreParticipantId;
            Assert.AreEqual(autreParticipant, (AutreParticipant)result.Value, "AutreParticipants pas identiques");
        }

        [TestMethod()]
        public async Task DeleteAutreParticipantTest()
        {
            AutreParticipant autreParticipant = new AutreParticipant()
            {
                AutreParticipantId = 99999,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            EntityEntry<AutreParticipant> res = _context.AutreParticipants.Add(autreParticipant);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteAutreParticipant(res.Entity.AutreParticipantId);

            AutreParticipant autreParticipan = _context.AutreParticipants.Where(u => u.AutreParticipantId == res.Entity.AutreParticipantId).FirstOrDefault();

            Assert.IsNull(autreParticipan, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteAutreParticipantTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            AutreParticipant autreParticipant = new AutreParticipant()
            {
                AutreParticipantId = 99999,
                CiviliteId = 2,
                Prenom = "Nouveau AutreParticipant",
                Nom = "Nouveau AutreParticipant",
                DateNaissance = DateTime.UnixEpoch,
            };

            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(autreParticipant);
            var userController = new AutreParticipantsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteAutreParticipant(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteAutreParticipantTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<AutreParticipant>>();
            var userController = new AutreParticipantsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteAutreParticipant(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
