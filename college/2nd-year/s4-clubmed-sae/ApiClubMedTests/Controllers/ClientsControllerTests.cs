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
    public class ClientControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly ClientsController _controller;
        private IDataRepository<Client> _dataRepository;
        public ClientControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new ClientManager(_context);
            _controller = new ClientsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetClientsTest()
        {
            ActionResult<IEnumerable<Client>> clients = await _controller.GetClients();
            var clients1 = clients.Value.ToList();
            clients1 = clients1.OrderBy(cli => cli.ClientId).ToList();

            var clients2 = _context.Clients.ToList();
            clients2 = clients2.OrderBy(cli => cli.ClientId).ToList();

            CollectionAssert.AreEqual(clients2, clients1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetClient_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Client client1 = new Client
            {
                ClientId = 99999,
                PaysId = 1,
                CiviliteId = 1,
                Prenom = "Robert",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "robert@marchand.com",
                Tel = "0627634837",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            Client client2 = new Client
            {
                ClientId = 99998,
                PaysId = 1,
                CiviliteId = 2,
                Prenom = "Marie",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "marie@marchand.com",
                Tel = "0692386909",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            List<Client> lesClient = new List<Client>();
            lesClient.Add(client1);
            lesClient.Add(client2);


            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesClient);
            var clientsController = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = clientsController.GetClients().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesClient, actionResult.Value as List<Client>, "Client différent");
        }

        [TestMethod()]
        public async Task GetAllClientTest()
        {
            ActionResult<Client> clients = await _controller.GetClient(1);
            Assert.AreEqual(_context.Clients.Where(c => c.ClientId == 1).FirstOrDefault(), clients.Value, "Client différent");
        }

        [TestMethod()]
        public void GetClientById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Client client = new Client
            {
                ClientId = 99999,
                PaysId = 1,
                CiviliteId = 1,
                Prenom = "Robert",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "robert@marchand.com",
                Tel = "0627634837",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(client);
            var clientsController = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = clientsController.GetClient(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(client, actionResult.Value as Client);
        }

        [TestMethod]
        public void GetClientById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Client>>();
            var clientsController = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = clientsController.GetClient(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }



        [TestMethod()]
        public async Task PutClientTest()
        {
            Client client = await _context.Clients.FindAsync(8);

            string nomOriginal = client.Nom;

            client.Nom = "Nom";
            await _controller.PutClient(client.ClientId, client);
            Client modifie = await _context.Clients.FindAsync(8);
            Assert.AreEqual(client, modifie, "Client différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutClient(modifie.ClientId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutClientTest__ReturnsNoContent_AvecMoq()
        {
            Client client = new Client()
            {
                ClientId = 99999,
                PaysId = 1,
                CiviliteId = 1,
                Prenom = "Robert",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "robert@marchand.com",
                Tel = "0627634837",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(client);
            var clientsController = new ClientsController(mockRepository.Object);


            client.Nom = "Nom";
            var res = clientsController.PutClient(client.ClientId, client);

            var actionResult = clientsController.GetClient(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Client>), "Pas un ActionResult<Client>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Client), "Pas un Client");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            client.ClientId = ((Client)result).ClientId;
            Assert.AreEqual(client, (Client)result, "Client pas identiques");
        }

        [TestMethod]
        public void PutClient_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Client client = new Client()
            {
                ClientId = 99999,
                PaysId = 1,
                CiviliteId = 1,
                Prenom = "Robert",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "robert@marchand.com",
                Tel = "0627634837",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(client);
            var clientsController = new ClientsController(mockRepository.Object);


            var res = clientsController.PutClient(15, client);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutClient_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Client client = new Client()
            {
                ClientId = 99999,
                PaysId = 1,
                CiviliteId = 1,
                Prenom = "Robert",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "robert@marchand.com",
                Tel = "0627634837",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(client);
            var clientsController = new ClientsController(mockRepository.Object);


            var res = clientsController.PutClient(99999, client);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostClientTest()
        {
            Client client = new Client()
            {
                ClientId = 99999,
                PaysId = 1,
                CiviliteId = 1,
                Prenom = "Robert",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "robert@marchand.com",
                Tel = "0627634837",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            var res = await _controller.PostClient(client);
            Client add = _context.Clients.Where(c => c.ClientId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Client ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteClient(add.ClientId);
        }

        [TestMethod()]
        public async Task PostClientTest__ReturnsCreateAtAction_AvecMoq()
        {

            Client client = new Client()
            {
                ClientId = 99999,
                PaysId = 1,
                CiviliteId = 1,
                Prenom = "Robert",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "robert@marchand.com",
                Tel = "0627634837",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Client>>();
            var clientsController = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = clientsController.PostClient(client).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Client>), "Pas un ActionResult<Client>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Client), "Pas un Client");
            client.ClientId = ((Client)result.Value).ClientId;
            Assert.AreEqual(client, (Client)result.Value, "Client pas identiques");
        }

        [TestMethod()]
        public async Task DeleteClientTest()
        {
            Client client = new Client()
            {
                ClientId = 99999,
                PaysId = 1,
                CiviliteId = 1,
                Prenom = "Robert",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "robert@marchand.com",
                Tel = "0627634837",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            EntityEntry<Client> res = _context.Clients.Add(client);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteClient(res.Entity.ClientId);

            Client testClient = _context.Clients.Where(u => u.ClientId == res.Entity.ClientId).FirstOrDefault();

            Assert.IsNull(testClient, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteClientTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Client client = new Client()
            {
                ClientId = 99999,
                PaysId = 1,
                CiviliteId = 1,
                Prenom = "Robert",
                Nom = "Marchand",
                DateNaissance = DateTime.UnixEpoch,
                Email = "robert@marchand.com",
                Tel = "0627634837",
                NumRue = 83,
                NomRue = "rue des pralines",
                CodePostal = "83478",
                Ville = "Beaufort",
                Password = "password"
            };

            var mockRepository = new Mock<IDataRepository<Client>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(client);
            var userController = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteClient(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteClientTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Client>>();
            var userController = new ClientsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteClient(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
