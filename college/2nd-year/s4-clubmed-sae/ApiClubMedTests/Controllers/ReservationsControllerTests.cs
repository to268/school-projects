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
    public class ReservationsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly ReservationsController _controller;
        private IDataRepository<Reservation> _dataRepository;
        public ReservationsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new ReservationManager(_context);
            _controller = new ReservationsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetReservationsTest()
        {
            ActionResult<IEnumerable<Reservation>> reservation = await _controller.GetReservations();
            var reservation1 = reservation.Value.ToList();
            reservation1 = reservation1.OrderBy(r => r.ReservationId).ToList();

            var reservation2 = _context.Reservations.ToList();
            reservation2 = reservation2.OrderBy(r => r.ReservationId).ToList();

            CollectionAssert.AreEqual(reservation2, reservation1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetReservation_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Reservation reservation1 = new Reservation
            {
                ReservationId = 99999,
                ClientId = 85,
                TransportId = 1,
                ResortId = 1,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 198298,
                Confirmation = true,
                Validation = false,
            };

            Reservation reservation2 = new Reservation
            {
                ReservationId = 99998,
                ClientId = 85,
                TransportId = 2,
                ResortId = 2,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 28982,
                Confirmation = true,
                Validation = true,
            };

            List<Reservation> lesReservation = new List<Reservation>();
            lesReservation.Add(reservation1);
            lesReservation.Add(reservation2);

            var mockRepository = new Mock<IDataRepository<Reservation>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesReservation);
            var reservationController = new ReservationsController(mockRepository.Object);
            // Act
            var actionResult = reservationController.GetReservations().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesReservation, actionResult.Value as List<Reservation>, "reservation différent");
        }

        [TestMethod()]
        public async Task GetAllReservationTest()
        {
            ActionResult<Reservation> reservation = await _controller.GetReservation(1);
            Assert.AreEqual(_context.Reservations.Where(r => r.ReservationId == 1).FirstOrDefault(), reservation.Value, "reservation différent");
        }

        [TestMethod()]
        public void GetReservationById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Reservation reservation = new Reservation
            {
                ReservationId = 99999,
                ClientId = 85,
                TransportId = 1,
                ResortId = 1,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 198298,
                Confirmation = true,
                Validation = false,
            };

            var mockRepository = new Mock<IDataRepository<Reservation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(reservation);
            var reservationController = new ReservationsController(mockRepository.Object);
            // Act
            var actionResult = reservationController.GetReservation(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(reservation, actionResult.Value as Reservation);
        }

        [TestMethod]
        public void GetReservationById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            var reservationController = new ReservationsController(mockRepository.Object);
            // Act
            var actionResult = reservationController.GetReservation(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutReservationTest()
        {
            Reservation reservation = await _context.Reservations.FindAsync(6);

            bool confirmationOriginal = reservation.Confirmation;

            reservation.Confirmation = !confirmationOriginal;
            await _controller.PutReservation(reservation.ReservationId, reservation);
            Reservation modifie = await _context.Reservations.FindAsync(6);
            Assert.AreEqual(reservation, modifie, "Reservation différents");


            //restauration des modification
            modifie.Confirmation = confirmationOriginal;
            await _controller.PutReservation(modifie.ReservationId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutReservationTest__ReturnsNoContent_AvecMoq()
        {
            Reservation reservation = new Reservation()
            {
                ReservationId = 99999,
                ClientId = 85,
                TransportId = 1,
                ResortId = 1,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 198298,
                Confirmation = true,
                Validation = false,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(reservation);
            var reservationController = new ReservationsController(mockRepository.Object);


            reservation.Confirmation = !reservation.Confirmation;
            var res = reservationController.PutReservation(reservation.ReservationId, reservation);

            var actionResult = reservationController.GetReservation(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Reservation>), "Pas un ActionResult<Reservation>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Reservation), "Pas un Reservation");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            reservation.ReservationId = ((Reservation)result).ReservationId;
            Assert.AreEqual(reservation, (Reservation)result, "reservation pas identiques");
        }

        [TestMethod]
        public void PutReservation_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Reservation reservation = new Reservation()
            {
                ReservationId = 99999,
                ClientId = 85,
                TransportId = 1,
                ResortId = 1,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 198298,
                Confirmation = true,
                Validation = false,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(reservation);
            var reservationController = new ReservationsController(mockRepository.Object);


            var res = reservationController.PutReservation(15, reservation);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutReservation_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Reservation reservation = new Reservation()
            {
                ReservationId = 99999,
                ClientId = 85,
                TransportId = 1,
                ResortId = 1,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 198298,
                Confirmation = true,
                Validation = false,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(reservation);
            var reservationController = new ReservationsController(mockRepository.Object);


            var res = reservationController.PutReservation(99999, reservation);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostReservationTest()
        {
            Reservation reservation = new Reservation()
            {
                ReservationId = 99999,
                ClientId = 85,
                TransportId = 1,
                ResortId = 1,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 198298,
                Confirmation = true,
                Validation = false,
            };

            var res = await _controller.PostReservation(reservation);
            Reservation add = _context.Reservations.Where(r => r.ReservationId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de reservation ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteReservation(add.ReservationId);
        }

        [TestMethod()]
        public async Task PostReservationTest__ReturnsCreateAtAction_AvecMoq()
        {

            Reservation reservation = new Reservation()
            {
                ReservationId = 99999,
                ClientId = 85,
                TransportId = 1,
                ResortId = 1,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 198298,
                Confirmation = true,
                Validation = false,
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            var reservationController = new ReservationsController(mockRepository.Object);
            // Act
            var actionResult = reservationController.PostReservation(reservation).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Reservation>), "Pas un ActionResult<Reservation>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Reservation), "Pas un Reservation");
            reservation.ReservationId = ((Reservation)result.Value).ReservationId;
            Assert.AreEqual(reservation, (Reservation)result.Value, "Reservation pas identiques");
        }

        [TestMethod()]
        public async Task DeleteReservationTest()
        {
            Reservation reservation = new Reservation()
            {
                ReservationId = 99999,
                ClientId = 85,
                TransportId = 1,
                ResortId = 1,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 198298,
                Confirmation = true,
                Validation = false,
            };

            EntityEntry<Reservation> res = _context.Reservations.Add(reservation);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteReservation(res.Entity.ReservationId);

            Reservation testreservation = _context.Reservations.Where(t => t.ReservationId == res.Entity.ReservationId).FirstOrDefault();

            Assert.IsNull(testreservation, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteReservationTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Reservation reservation = new Reservation()
            {
                ReservationId = 99999,
                ClientId = 85,
                TransportId = 1,
                ResortId = 1,
                DateDebut = DateTime.UnixEpoch,
                DateFin = DateTime.Now,
                Prix = 198298,
                Confirmation = true,
                Validation = false,
            };

            var mockRepository = new Mock<IDataRepository<Reservation>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(reservation);
            var userController = new ReservationsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteReservation(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteReservationTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            var userController = new ReservationsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteReservation(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
