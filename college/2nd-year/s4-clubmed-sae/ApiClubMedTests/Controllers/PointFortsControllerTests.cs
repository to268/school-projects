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
    public class PointFortsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly PointFortsController _controller;
        private IDataRepository<PointFort> _dataRepository;
        public PointFortsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new PointFortManager(_context);
            _controller = new PointFortsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetPointFortsTest()
        {
            ActionResult<IEnumerable<PointFort>> pointForts = await _controller.GetPointForts();
            var pointFort1 = pointForts.Value.ToList();
            pointFort1 = pointFort1.OrderBy(ap => ap.PointFortId).ToList();

            var pointFort2 = _context.PointForts.ToList();
            pointFort2 = pointFort2.OrderBy(ap => ap.PointFortId).ToList();

            CollectionAssert.AreEqual(pointFort2, pointFort1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetPointForts_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            PointFort pointFort1 = new PointFort
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            PointFort pointFort2 = new PointFort
            {
                PointFortId = 99998,


                Libelle = "Nouveau PointFort2",

            };

            List<PointFort> lesPointForts = new List<PointFort>();
            lesPointForts.Add(pointFort1);
            lesPointForts.Add(pointFort2);


            var mockRepository = new Mock<IDataRepository<PointFort>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesPointForts);
            var pointFortController = new PointFortsController(mockRepository.Object);
            // Act
            var actionResult = pointFortController.GetPointForts().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesPointForts, actionResult.Value as List<PointFort>, "PointFort différent");
        }

        [TestMethod()]
        public async Task GetPointFortTest()
        {
            ActionResult<PointFort> pointFort = await _controller.GetPointFort(1);
            Assert.AreEqual(_context.PointForts.Where(c => c.PointFortId == 1).FirstOrDefault(), pointFort.Value, "PointFort différent");
        }

        [TestMethod()]
        public void GetPointFortById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            PointFort pointFort = new PointFort
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            var mockRepository = new Mock<IDataRepository<PointFort>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(pointFort);
            var pointFortController = new PointFortsController(mockRepository.Object);
            // Act
            var actionResult = pointFortController.GetPointFort(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(pointFort, actionResult.Value as PointFort);
        }

        [TestMethod]
        public void GetPointFortById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<PointFort>>();
            var pointFortController = new PointFortsController(mockRepository.Object);
            // Act
            var actionResult = pointFortController.GetPointFort(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetPointFortByLibelleTest()
        {
            ActionResult<PointFort> pointFort = await _controller.GetPointFortByLibelle("Terrasse aménagée");
            PointFort lePointFort = _context.PointForts.Where(c => c.Libelle == "Terrasse aménagée").FirstOrDefault();
            Assert.IsNotNull(pointFort);
            Assert.AreEqual(lePointFort, pointFort.Value, "PointFort différent");
        }

        [TestMethod()]
        public void GetPointFortByLibelle_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            PointFort pointFort = new PointFort
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            var mockRepository = new Mock<IDataRepository<PointFort>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau PointFort").Result).Returns(pointFort);
            var pointFortController = new PointFortsController(mockRepository.Object);
            // Act
            var actionResult = pointFortController.GetPointFortByLibelle("Nouveau PointFort").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(pointFort, actionResult.Value as PointFort, "PointFort différent");
        }

        [TestMethod]
        public void GetPointFortByLibelle_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<PointFort>>();
            var pointFortController = new PointFortsController(mockRepository.Object);
            // Act
            var actionResult = pointFortController.GetPointFortByLibelle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutPointFortTest()
        {
            PointFort pointFort = await _context.PointForts.FindAsync(1);

            string nomOriginal = pointFort.Libelle;

            pointFort.Libelle = "Le test";
            await _controller.PutPointFort(pointFort.PointFortId, pointFort);
            PointFort modifie = await _context.PointForts.FindAsync(1);
            Assert.AreEqual(pointFort, modifie, "PointFort différents");


            //restauration des modification
            modifie.Libelle = nomOriginal;
            await _controller.PutPointFort(modifie.PointFortId, modifie);
        }

        [TestMethod]
        public void PutPointFortTest__ReturnsNoContent_AvecMoq()
        {
            PointFort pointFort = new PointFort()
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<PointFort>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(pointFort);
            var pointFortController = new PointFortsController(mockRepository.Object);


            pointFort.Libelle = "a";
            var res = pointFortController.PutPointFort(pointFort.PointFortId, pointFort);

            var actionResult = pointFortController.GetPointFort(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<PointFort>), "Pas un ActionResult<PointFort>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(PointFort), "Pas un PointFort");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            pointFort.PointFortId = ((PointFort)result).PointFortId;
            Assert.AreEqual(pointFort, (PointFort)result, "PointForts pas identiques");
        }

        [TestMethod]
        public void PutPointFort_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            PointFort pointFort = new PointFort()
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<PointFort>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(pointFort);
            var pointFortController = new PointFortsController(mockRepository.Object);

            var res = pointFortController.PutPointFort(15, pointFort);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutPointFort_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            PointFort pointFort = new PointFort()
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<PointFort>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(pointFort);
            var pointFortController = new PointFortsController(mockRepository.Object);

            var res = pointFortController.PutPointFort(99999, pointFort);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostPointFortTest()
        {
            PointFort pointFort = new PointFort()
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            var res = await _controller.PostPointFort(pointFort);
            PointFort add = _context.PointForts.Where(c => c.PointFortId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de PointFort ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeletePointFort(add.PointFortId);
        }

        [TestMethod()]
        public async Task PostPointFortTest__ReturnsCreateAtAction_AvecMoq()
        {

            PointFort pointFort = new PointFort()
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<PointFort>>();
            var pointFortController = new PointFortsController(mockRepository.Object);
            // Act
            var actionResult = pointFortController.PostPointFort(pointFort).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<PointFort>), "Pas un ActionResult<PointFort>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(PointFort), "Pas un PointFort");
            pointFort.PointFortId = ((PointFort)result.Value).PointFortId;
            Assert.AreEqual(pointFort, (PointFort)result.Value, "PointForts pas identiques");
        }

        [TestMethod()]
        public async Task DeletePointFortTest()
        {
            PointFort pointFort = new PointFort()
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            EntityEntry<PointFort> res = _context.PointForts.Add(pointFort);
            _context.SaveChanges();
            IActionResult result = await _controller.DeletePointFort(res.Entity.PointFortId);

            PointFort pointFort1 = _context.PointForts.Where(u => u.PointFortId == res.Entity.PointFortId).FirstOrDefault();

            Assert.IsNull(pointFort1, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeletePointFortTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            PointFort pointFort = new PointFort()
            {
                PointFortId = 99999,


                Libelle = "Nouveau PointFort",

            };

            var mockRepository = new Mock<IDataRepository<PointFort>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(pointFort);
            var userController = new PointFortsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeletePointFort(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeletePointFortTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<PointFort>>();
            var userController = new PointFortsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeletePointFort(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
