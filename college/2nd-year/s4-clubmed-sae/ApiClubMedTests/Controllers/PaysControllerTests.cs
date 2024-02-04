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
    public class PaysControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly PaysController _controller;
        private IDataRepository<Pays> _dataRepository;
        public PaysControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new PaysManager(_context);
            _controller = new PaysController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetPaysTest()
        {
            ActionResult<IEnumerable<Pays>> pays = await _controller.GetPays();
            var pays1 = pays.Value.ToList();
            pays1 = pays1.OrderBy(p => p.PaysId).ToList();

            var pays2 = _context.Pays.ToList();
            pays2 = pays2.OrderBy(p => p.PaysId).ToList();

            CollectionAssert.AreEqual(pays2, pays1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetPays_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Pays pays1 = new Pays
            {
                PaysId = 99999,
                Nom = "Fronce",
            };

            Pays pays2 = new Pays
            {
                PaysId = 99998,
                Nom = "Flance",
            };

            List<Pays> lesPays = new List<Pays>();
            lesPays.Add(pays1);
            lesPays.Add(pays2);

            var mockRepository = new Mock<IDataRepository<Pays>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesPays);
            var paysController = new PaysController(mockRepository.Object);
            // Act
            var actionResult = paysController.GetPays().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesPays, actionResult.Value as List<Pays>, "Pays différent");
        }

        [TestMethod()]
        public async Task GetAllPaysTest()
        {
            ActionResult<Pays> pays = await _controller.GetPays(1);
            Assert.AreEqual(_context.Pays.Where(p => p.PaysId == 1).FirstOrDefault(), pays.Value, "Pays différent");
        }

        [TestMethod()]
        public void GetPaysById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Pays pays = new Pays
            {
                PaysId = 99999,
                Nom = "Fronce",
            };

            var mockRepository = new Mock<IDataRepository<Pays>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(pays);
            var paysController = new PaysController(mockRepository.Object);
            // Act
            var actionResult = paysController.GetPays(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(pays, actionResult.Value as Pays);
        }

        [TestMethod]
        public void GetPaysById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Pays>>();
            var paysController = new PaysController(mockRepository.Object);
            // Act
            var actionResult = paysController.GetPays(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }



        [TestMethod()]
        public async Task PutPaysTest()
        {
            Pays pays = await _context.Pays.FindAsync(1);

            string nomOriginal = pays.Nom;

            pays.Nom = "Pays";
            await _controller.PutPays(pays.PaysId, pays);
            Pays modifie = await _context.Pays.FindAsync(1);
            Assert.AreEqual(pays, modifie, "Pays différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutPays(modifie.PaysId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutPaysTest__ReturnsNoContent_AvecMoq()
        {
            Pays pays = new Pays()
            {
                PaysId = 99999,
                Nom = "Fronce",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Pays>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(pays);
            var paysController = new PaysController(mockRepository.Object);


            pays.Nom = "Pays";
            var res = paysController.PutPays(pays.PaysId, pays);

            var actionResult = paysController.GetPays(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Pays>), "Pas un ActionResult<Pays>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Pays), "Pas un Pays");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            pays.PaysId = ((Pays)result).PaysId;
            Assert.AreEqual(pays, (Pays)result, "Pays pas identiques");
        }

        [TestMethod]
        public void PutPays_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Pays pays = new Pays()
            {
                PaysId = 99999,
                Nom = "Fronce",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Pays>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(pays);
            var paysController = new PaysController(mockRepository.Object);


            var res = paysController.PutPays(15, pays);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutPays_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Pays pays = new Pays()
            {
                PaysId = 99999,
                Nom = "Fronce",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Pays>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(pays);
            var paysController = new PaysController(mockRepository.Object);


            var res = paysController.PutPays(99999, pays);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostPaysTest()
        {
            Pays pays = new Pays()
            {
                PaysId = 99999,
                Nom = "Fronce",
            };

            var res = await _controller.PostPays(pays);
            Pays add = _context.Pays.Where(c => c.PaysId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Pays ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeletePays(add.PaysId);
        }

        [TestMethod()]
        public async Task PostPaysTest__ReturnsCreateAtAction_AvecMoq()
        {

            Pays pays = new Pays()
            {
                PaysId = 99999,
                Nom = "Fronce",
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Pays>>();
            var paysController = new PaysController(mockRepository.Object);
            // Act
            var actionResult = paysController.PostPays(pays).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Pays>), "Pas un ActionResult<Pays>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Pays), "Pas un Pays");
            pays.PaysId = ((Pays)result.Value).PaysId;
            Assert.AreEqual(pays, (Pays)result.Value, "Pays pas identiques");
        }

        [TestMethod()]
        public async Task DeletePaysTest()
        {
            Pays pays = new Pays()
            {
                PaysId = 99999,
                Nom = "Fronce",
            };

            EntityEntry<Pays> res = _context.Pays.Add(pays);
            _context.SaveChanges();
            IActionResult result = await _controller.DeletePays(res.Entity.PaysId);

            Pays testPays = _context.Pays.Where(u => u.PaysId == res.Entity.PaysId).FirstOrDefault();

            Assert.IsNull(testPays, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeletePaysTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Pays pays = new Pays()
            {
                PaysId = 99999,
                Nom = "Fronce",
            };

            var mockRepository = new Mock<IDataRepository<Pays>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(pays);
            var userController = new PaysController(mockRepository.Object);
            // Act
            var actionResult = userController.DeletePays(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeletePaysTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Pays>>();
            var userController = new PaysController(mockRepository.Object);
            // Act
            var actionResult = userController.DeletePays(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
