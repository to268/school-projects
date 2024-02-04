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
    public class TransportsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly TransportsController _controller;
        private IDataRepository<Transport> _dataRepository;
        public TransportsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new TransportManager(_context);
            _controller = new TransportsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetTransportsTest()
        {
            ActionResult<IEnumerable<Transport>> transport = await _controller.GetTransports();
            var transport1 = transport.Value.ToList();
            transport1 = transport1.OrderBy(t => t.TransportId).ToList();

            var transport2 = _context.Transports.ToList();
            transport2 = transport2.OrderBy(t => t.TransportId).ToList();

            CollectionAssert.AreEqual(transport2, transport1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void Gettransport_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Transport transport1 = new Transport
            {
                TransportId = 99999,
                Libelle = "transport",
            };

            Transport transport2 = new Transport
            {
                TransportId = 99998,
                Libelle = "transport2",
            };

            List<Transport> lesTransport = new List<Transport>();
            lesTransport.Add(transport1);
            lesTransport.Add(transport2);

            var mockRepository = new Mock<IDataRepository<Transport>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesTransport);
            var transportController = new TransportsController(mockRepository.Object);
            // Act
            var actionResult = transportController.GetTransports().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesTransport, actionResult.Value as List<Transport>, "transport différent");
        }

        [TestMethod()]
        public async Task GetAllTransportTest()
        {
            ActionResult<Transport> transport = await _controller.GetTransport(1);
            Assert.AreEqual(_context.Transports.Where(t => t.TransportId == 1).FirstOrDefault(), transport.Value, "transport différent");
        }

        [TestMethod()]
        public void GetTransportById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Transport transport = new Transport
            {
                TransportId = 99999,
                Libelle = "transport",
            };

            var mockRepository = new Mock<IDataRepository<Transport>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(transport);
            var transportController = new TransportsController(mockRepository.Object);
            // Act
            var actionResult = transportController.GetTransport(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(transport, actionResult.Value as Transport);
        }

        [TestMethod]
        public void GetTransportById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Transport>>();
            var transportController = new TransportsController(mockRepository.Object);
            // Act
            var actionResult = transportController.GetTransport(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }



        [TestMethod()]
        public async Task PutTransportTest()
        {
            Transport transport = await _context.Transports.FindAsync(1);

            string libelleOriginal = transport.Libelle;

            transport.Libelle = "modif";
            await _controller.PutTransport(transport.TransportId, transport);
            Transport modifie = await _context.Transports.FindAsync(1);
            Assert.AreEqual(transport, modifie, "transport différents");


            //restauration des modification
            modifie.Libelle = libelleOriginal;
            await _controller.PutTransport(modifie.TransportId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutTransportTest__ReturnsNoContent_AvecMoq()
        {
            Transport transport = new Transport()
            {
                TransportId = 99999,
                Libelle = "transport",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Transport>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(transport);
            var transportController = new TransportsController(mockRepository.Object);


            transport.Libelle = "modif";
            var res = transportController.PutTransport(transport.TransportId, transport);

            var actionResult = transportController.GetTransport(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Transport>), "Pas un ActionResult<Transport>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Transport), "Pas un Transport");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            transport.TransportId = ((Transport)result).TransportId;
            Assert.AreEqual(transport, (Transport)result, "transport pas identiques");
        }

        [TestMethod]
        public void PutTransport_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Transport transport = new Transport()
            {
                TransportId = 99999,
                Libelle = "transport",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Transport>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(transport);
            var transportController = new TransportsController(mockRepository.Object);


            var res = transportController.PutTransport(15, transport);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutTransport_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Transport transport = new Transport()
            {
                TransportId = 99999,
                Libelle = "transport",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Transport>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(transport);
            var transportController = new TransportsController(mockRepository.Object);


            var res = transportController.PutTransport(99999, transport);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostTransportTest()
        {
            Transport transport = new Transport()
            {
                TransportId = 99999,
                Libelle = "transport",
            };

            var res = await _controller.PostTransport(transport);
            Transport add = _context.Transports.Where(c => c.TransportId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Transport ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteTransport(add.TransportId);
        }

        [TestMethod()]
        public async Task PostTransportTest__ReturnsCreateAtAction_AvecMoq()
        {

            Transport transport = new Transport()
            {
                TransportId = 99999,
                Libelle = "transport",
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Transport>>();
            var transportController = new TransportsController(mockRepository.Object);
            // Act
            var actionResult = transportController.PostTransport(transport).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Transport>), "Pas un ActionResult<Transport>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Transport), "Pas un Transport");
            transport.TransportId = ((Transport)result.Value).TransportId;
            Assert.AreEqual(transport, (Transport)result.Value, "Transport pas identiques");
        }

        [TestMethod()]
        public async Task DeleteTransportTest()
        {
            Transport transport = new Transport()
            {
                TransportId = 99999,
                Libelle = "transport",
            };

            EntityEntry<Transport> res = _context.Transports.Add(transport);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteTransport(res.Entity.TransportId);

            Transport testTransport = _context.Transports.Where(t => t.TransportId == res.Entity.TransportId).FirstOrDefault();

            Assert.IsNull(testTransport, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeletetransportTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Transport transport = new Transport()
            {
                TransportId = 99999,
                Libelle = "transport",
            };

            var mockRepository = new Mock<IDataRepository<Transport>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(transport);
            var userController = new TransportsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTransport(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeletetransportTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Transport>>();
            var userController = new TransportsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTransport(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
