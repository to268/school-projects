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
    public class DomaineSkiablesControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly DomaineSkiablesController _controller;
        private IDataRepository<DomaineSkiable> _dataRepository;
        public DomaineSkiablesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new DomaineSkiableManager(_context);
            _controller = new DomaineSkiablesController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetDomaineSkiablesTest()
        {
            ActionResult<IEnumerable<DomaineSkiable>> domaineSkiables = await _controller.GetDomaineSkiables();
            var domaines1 = domaineSkiables.Value.ToList();
            domaines1 = domaines1.OrderBy(d => d.DomaineSkiableId).ToList();

            var domaines2 = _context.DomaineSkiables.ToList();
            domaines2 = domaines2.OrderBy(d => d.DomaineSkiableId).ToList();

            CollectionAssert.AreEqual(domaines2, domaines1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetDomaineSkiables_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            DomaineSkiable domaineSkiable1 = new DomaineSkiable
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };

            DomaineSkiable domaineSkiable2 = new DomaineSkiable
            {
                DomaineSkiableId = 99998,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable2",
                Nom = "Nouveau DomaineSkiable2",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable2 de test",
                LongueurDesPistes = 0
            };


            List<DomaineSkiable> lesDomainesSkiables = new List<DomaineSkiable>();
            lesDomainesSkiables.Add(domaineSkiable1);
            lesDomainesSkiables.Add(domaineSkiable2);


            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesDomainesSkiables);
            var domaineSkiableController = new DomaineSkiablesController(mockRepository.Object);
            // Act
            var actionResult = domaineSkiableController.GetDomaineSkiables().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesDomainesSkiables, actionResult.Value as List<DomaineSkiable>, "DomaineSkiable différent");
        }

        [TestMethod()]
        public async Task GetDomaineSkiableTest()
        {
            ActionResult<DomaineSkiable> domaineSkiable = await _controller.GetDomaineSkiable(1);
            Assert.AreEqual(_context.DomaineSkiables.Where(c => c.DomaineSkiableId == 1).FirstOrDefault(), domaineSkiable.Value, "DomaineSkiable différent");
        }

        [TestMethod()]
        public void GetDomaineSkiableById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            DomaineSkiable domaineSkiable = new DomaineSkiable
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(domaineSkiable);
            var domaineSkiableController = new DomaineSkiablesController(mockRepository.Object);
            // Act
            var actionResult = domaineSkiableController.GetDomaineSkiable(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(domaineSkiable, actionResult.Value as DomaineSkiable);
        }

        [TestMethod]
        public void GetDomaineSkiableById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            var sousLocalisationController = new DomaineSkiablesController(mockRepository.Object);
            // Act
            var actionResult = sousLocalisationController.GetDomaineSkiable(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetDomaineByNameTest()
        {
            ActionResult<DomaineSkiable> domainSkiable = await _controller.GetDomaineByName("La Rosière");
            DomaineSkiable leDomaineSkiable = _context.DomaineSkiables.Where(c => c.Nom == "La Rosière").FirstOrDefault();
            Assert.IsNotNull(domainSkiable);
            Assert.AreEqual(leDomaineSkiable, domainSkiable.Value, "DomaineSkiable différent");
        }

        [TestMethod()]
        public void GetDomaineSkiableByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            DomaineSkiable domaineSkiable = new DomaineSkiable
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau DomaineSkiable").Result).Returns(domaineSkiable);
            var domaineSkiableController = new DomaineSkiablesController(mockRepository.Object);
            // Act
            var actionResult = domaineSkiableController.GetDomaineByName("Nouveau DomaineSkiable").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(domaineSkiable, actionResult.Value as DomaineSkiable, "DomaineSkiable différent");
        }

        [TestMethod]
        public void GetDomaineSkiableByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            var sousLocalisationController = new DomaineSkiablesController(mockRepository.Object);
            // Act
            var actionResult = sousLocalisationController.GetDomaineByName("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutDomaineSkiableTest()
        {
            DomaineSkiable domaineSkiable = await _context.DomaineSkiables.FindAsync(1);

            string nomOriginal = domaineSkiable.Nom;

            domaineSkiable.Nom = "Le test";
            await _controller.PutDomaineSkiable(domaineSkiable.DomaineSkiableId, domaineSkiable);
            DomaineSkiable modifie = await _context.DomaineSkiables.FindAsync(1);
            Assert.AreEqual(domaineSkiable, modifie, "DomaineSkiable différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutDomaineSkiable(modifie.DomaineSkiableId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutDomainSkiableTest__ReturnsNoContent_AvecMoq()
        {
            DomaineSkiable domaineSkiable = new DomaineSkiable()
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };

            // Act
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(domaineSkiable);
            var domaineSkiableController = new DomaineSkiablesController(mockRepository.Object);


            domaineSkiable.Nom = "a";
            var res = domaineSkiableController.PutDomaineSkiable(domaineSkiable.DomaineSkiableId, domaineSkiable);

            var actionResult = domaineSkiableController.GetDomaineSkiable(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DomaineSkiable>), "Pas un ActionResult<DomaineSkiable>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(DomaineSkiable), "Pas un DomaineSkiable");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            domaineSkiable.DomaineSkiableId = ((DomaineSkiable)result).DomaineSkiableId;
            Assert.AreEqual(domaineSkiable, (DomaineSkiable)result, "DomaineSkiables pas identiques");
        }

        [TestMethod]
        public void PutDomaineSkiable_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            DomaineSkiable domaineSkiable = new DomaineSkiable()
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };

            // Act
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(domaineSkiable);
            var domaineSkiableController = new DomaineSkiablesController(mockRepository.Object);


            var res = domaineSkiableController.PutDomaineSkiable(15, domaineSkiable);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutDomaineSkiable_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            DomaineSkiable domaineSkiable = new DomaineSkiable()
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };

            // Act
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(domaineSkiable);
            var domaineSkiableController = new DomaineSkiablesController(mockRepository.Object);


            var res = domaineSkiableController.PutDomaineSkiable(99999, domaineSkiable);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostDomaineSkiableTest()
        {
            DomaineSkiable domaineSkiable = new DomaineSkiable()
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };

            var res = await _controller.PostDomaineSkiable(domaineSkiable);
            DomaineSkiable add = _context.DomaineSkiables.Where(c => c.DomaineSkiableId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de DomaineSkiable ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteDomaineSkiable(add.DomaineSkiableId);
        }

        [TestMethod()]
        public async Task PostDomaineSkiableTest__ReturnsCreateAtAction_AvecMoq()
        {

            DomaineSkiable domaineSkiable = new DomaineSkiable()
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            var domaineSkiableController = new DomaineSkiablesController(mockRepository.Object);
            // Act
            var actionResult = domaineSkiableController.PostDomaineSkiable(domaineSkiable).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DomaineSkiable>), "Pas un ActionResult<DomainSkiable>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(DomaineSkiable), "Pas un DomaineSkiable");
            domaineSkiable.DomaineSkiableId = ((DomaineSkiable)result.Value).DomaineSkiableId;
            Assert.AreEqual(domaineSkiable, (DomaineSkiable)result.Value, "DomaineSkiables pas identiques");
        }

        [TestMethod()]
        public async Task DeleteDomaineSkiableTest()
        {
            DomaineSkiable domaineSkiable = new DomaineSkiable()
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };
            EntityEntry<DomaineSkiable> res = _context.DomaineSkiables.Add(domaineSkiable);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteDomaineSkiable(res.Entity.DomaineSkiableId);

            DomaineSkiable domaine = _context.DomaineSkiables.Where(u => u.DomaineSkiableId == res.Entity.DomaineSkiableId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteDomainSkiableTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            DomaineSkiable domaineSkiable = new DomaineSkiable()
            {
                DomaineSkiableId = 99999,
                PhotoId = 2,
                Titre = "Nouveau DomaineSkiable",
                Nom = "Nouveau DomaineSkiable",
                AltitudeClub = 00000000000,
                AltitudeStation = 000000000,
                NbPiste = 0,
                InfoSkiAuPied = true,
                Description = "Le nouveau DomaineSkiable de test",
                LongueurDesPistes = 0
            };
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(domaineSkiable);
            var userController = new DomaineSkiablesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteDomaineSkiable(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteDomainSkiableTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<DomaineSkiable>>();
            var userController = new DomaineSkiablesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteDomaineSkiable(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
