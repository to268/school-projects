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
    public class PayementsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly PayementsController _controller;
        private IDataRepository<Payement> _dataRepository;
        public PayementsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new PayementManager(_context);
            _controller = new PayementsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetPayementsTest()
        {
            ActionResult<IEnumerable<Payement>> payement = await _controller.GetPayements();
            var payement1 = payement.Value.ToList();
            payement1 = payement1.OrderBy(p => p.PayementId).ToList();

            var payement2 = _context.Payements.ToList();
            payement2 = payement2.OrderBy(p => p.PayementId).ToList();

            CollectionAssert.AreEqual(payement2, payement1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetPayement_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Payement payement1 = new Payement
            {
                PayementId = 99999,
                ReservationId = 6,
                Montant = 4134,
            };

            Payement payement2 = new Payement
            {
                PayementId = 99998,
                ReservationId = 6,
                Montant = 2488,
            };

            List<Payement> lesPayement = new List<Payement>();
            lesPayement.Add(payement1);
            lesPayement.Add(payement2);

            var mockRepository = new Mock<IDataRepository<Payement>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesPayement);
            var payementController = new PayementsController(mockRepository.Object);
            // Act
            var actionResult = payementController.GetPayements().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesPayement, actionResult.Value as List<Payement>, "Payement différent");
        }

        [TestMethod()]
        public async Task GetAllPayementTest()
        {
            ActionResult<Payement> payement = await _controller.GetPayement(1);
            Assert.AreEqual(_context.Payements.Where(p => p.PayementId == 1).FirstOrDefault(), payement.Value, "Payement différent");
        }

        [TestMethod()]
        public void GetPayementById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Payement payement = new Payement
            {
                PayementId = 99999,
                ReservationId = 6,
                Montant = 4134,
            };

            var mockRepository = new Mock<IDataRepository<Payement>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(payement);
            var payementController = new PayementsController(mockRepository.Object);
            // Act
            var actionResult = payementController.GetPayement(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(payement, actionResult.Value as Payement);
        }

        [TestMethod]
        public void GetPayementById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Payement>>();
            var payementController = new PayementsController(mockRepository.Object);
            // Act
            var actionResult = payementController.GetPayement(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }



        [TestMethod()]
        public async Task PutPayementTest()
        {
            Payement payement = await _context.Payements.FindAsync(1);

            double montantOriginal = payement.Montant;

            payement.Montant = 0.01;
            await _controller.PutPayement(payement.PayementId, payement);
            Payement modifie = await _context.Payements.FindAsync(1);
            Assert.AreEqual(payement, modifie, "Payement différents");


            //restauration des modification
            modifie.Montant = montantOriginal;
            await _controller.PutPayement(modifie.PayementId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutPayementTest__ReturnsNoContent_AvecMoq()
        {
            Payement payement = new Payement()
            {
                PayementId = 99999,
                ReservationId = 6,
                Montant = 4134,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Payement>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(payement);
            var payementController = new PayementsController(mockRepository.Object);


            payement.Montant = 0.01;
            var res = payementController.PutPayement(payement.PayementId, payement);

            var actionResult = payementController.GetPayement(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Payement>), "Pas un ActionResult<Payement>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Payement), "Pas un Payement");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            payement.PayementId = ((Payement)result).PayementId;
            Assert.AreEqual(payement, (Payement)result, "Payement pas identiques");
        }

        [TestMethod]
        public void PutPayement_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Payement payement = new Payement()
            {
                PayementId = 99999,
                ReservationId = 6,
                Montant = 4134,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Payement>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(payement);
            var payementController = new PayementsController(mockRepository.Object);


            var res = payementController.PutPayement(15, payement);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutPayement_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Payement payement = new Payement()
            {
                PayementId = 99999,
                ReservationId = 6,
                Montant = 4134,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Payement>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(payement);
            var payementController = new PayementsController(mockRepository.Object);


            var res = payementController.PutPayement(99999, payement);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostPayementTest()
        {
            Payement payement = new Payement()
            {
                PayementId = 99999,
                ReservationId = 6,
                Montant = 4134,
            };

            var res = await _controller.PostPayement(payement);
            Payement add = _context.Payements.Where(c => c.PayementId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Payement ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeletePayement(add.PayementId);
        }

        [TestMethod()]
        public async Task PostPayementTest__ReturnsCreateAtAction_AvecMoq()
        {

            Payement payement = new Payement()
            {
                PayementId = 99999,
                ReservationId = 6,
                Montant = 4134,
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Payement>>();
            var payementController = new PayementsController(mockRepository.Object);
            // Act
            var actionResult = payementController.PostPayement(payement).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Payement>), "Pas un ActionResult<Payement>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Payement), "Pas un Payement");
            payement.PayementId = ((Payement)result.Value).PayementId;
            Assert.AreEqual(payement, (Payement)result.Value, "Payement pas identiques");
        }

        [TestMethod()]
        public async Task DeletePayementTest()
        {
            Payement payement = new Payement()
            {
                PayementId = 99999,
                ReservationId = 6,
                Montant = 4134,
            };

            EntityEntry<Payement> res = _context.Payements.Add(payement);
            _context.SaveChanges();
            IActionResult result = await _controller.DeletePayement(res.Entity.PayementId);

            Payement testPayement = _context.Payements.Where(u => u.PayementId == res.Entity.PayementId).FirstOrDefault();

            Assert.IsNull(testPayement, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeletePayementTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Payement payement = new Payement()
            {
                PayementId = 99999,
                ReservationId = 6,
                Montant = 4134,
            };

            var mockRepository = new Mock<IDataRepository<Payement>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(payement);
            var userController = new PayementsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeletePayement(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeletePayementTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Payement>>();
            var userController = new PayementsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeletePayement(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
