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
    public class ActiviteEnfantCartesControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly ActiviteEnfantCartesController _controller;
        private IDataRepository<ActiviteEnfantCarte> _dataRepository;
        public ActiviteEnfantCartesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new ActiviteEnfantCarteManager(_context);
            _controller = new ActiviteEnfantCartesController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetActiviteEnfantCartesTest()
        {
            ActionResult<IEnumerable<ActiviteEnfantCarte>> activiteEnfantCartes = await _controller.GetActiviteEnfantCartes();
            var activiteEnfantCartes1 = activiteEnfantCartes.Value.ToList();
            activiteEnfantCartes1 = activiteEnfantCartes1.OrderBy(aec => aec.ActiviteEnfantCarteId).ToList();

            var activiteEnfantCartes2 = _context.ActiviteEnfantCartes.ToList();
            activiteEnfantCartes2 = activiteEnfantCartes2.OrderBy(aec => aec.ActiviteEnfantCarteId).ToList();

            CollectionAssert.AreEqual(activiteEnfantCartes2, activiteEnfantCartes1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetActiviteEnfantCartes_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteEnfantCarte activiteEnfantCarte1 = new ActiviteEnfantCarte
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };

            ActiviteEnfantCarte activiteEnfantCarte2 = new ActiviteEnfantCarte
            {
                ActiviteEnfantCarteId = 99998,
                Titre = "Nouvelle ActiviteEnfantCarte2",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte2",
                Frequence = "Nouvelle ActiviteEnfantCarte2",
                Prix = 0
            };


            List<ActiviteEnfantCarte> lesActiviteEnfantCartes = new List<ActiviteEnfantCarte>();
            lesActiviteEnfantCartes.Add(activiteEnfantCarte1);
            lesActiviteEnfantCartes.Add(activiteEnfantCarte2);


            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesActiviteEnfantCartes);
            var activiteEnfantCarteController = new ActiviteEnfantCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantCarteController.GetActiviteEnfantCartes().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesActiviteEnfantCartes, actionResult.Value as List<ActiviteEnfantCarte>, "ActiviteEnfantCarte différent");
        }

        [TestMethod()]
        public async Task GetActiviteEnfantCarteTest()
        {
            ActionResult<ActiviteEnfantCarte> activiteEnfantCarte = await _controller.GetActiviteEnfantCarte(1);
            Assert.AreEqual(_context.ActiviteEnfantCartes.Where(c => c.ActiviteEnfantCarteId == 1).FirstOrDefault(), activiteEnfantCarte.Value, "ActiviteEnfantCarte différent");
        }

        [TestMethod()]
        public void GetActiviteEnfantCarteById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteEnfantCarte activiteEnfantCarte = new ActiviteEnfantCarte
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteEnfantCarte);
            var activiteEnfantCarteController = new ActiviteEnfantCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantCarteController.GetActiviteEnfantCarte(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(activiteEnfantCarte, actionResult.Value as ActiviteEnfantCarte);
        }

        [TestMethod]
        public void GetActiviteEnfantCarteById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            var activiteEnfantCarteController = new ActiviteEnfantCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantCarteController.GetActiviteEnfantCarte(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetActiviteEnfantCarteByTitleTest()
        {
            ActionResult<ActiviteEnfantCarte> activiteEnfantCarte = await _controller.GetActiviteByTitle("Baby Club Med");
            ActiviteEnfantCarte laActiviteEnfantCarte = _context.ActiviteEnfantCartes.Where(c => c.Titre == "Baby Club Med").FirstOrDefault();
            Assert.IsNotNull(laActiviteEnfantCarte);
            Assert.AreEqual(laActiviteEnfantCarte, activiteEnfantCarte.Value, "ActiviteEnfantCarte différent");
        }

        [TestMethod()]
        public void GetActiviteEnfantCarteByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteEnfantCarte activiteEnfantCarte = new ActiviteEnfantCarte
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            mockRepository.Setup(x => x.GetByStringAsync("Baby Club Med").Result).Returns(activiteEnfantCarte);
            var activiteEnfantCarteController = new ActiviteEnfantCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantCarteController.GetActiviteByTitle("Baby Club Med").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(activiteEnfantCarte, actionResult.Value as ActiviteEnfantCarte, "ActiviteEnfantCarte différent");
        }

        [TestMethod]
        public void GetActiviteEnfantCarteByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            var activiteEnfantCarteController = new ActiviteEnfantCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantCarteController.GetActiviteByTitle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutActiviteEnfantCarteTest()
        {
            ActiviteEnfantCarte activiteEnfantCarte = await _context.ActiviteEnfantCartes.FindAsync(1);

            string titreOriginal = activiteEnfantCarte.Titre;

            activiteEnfantCarte.Titre = "Le test";
            await _controller.PutActiviteEnfantCarte(activiteEnfantCarte.ActiviteEnfantCarteId, activiteEnfantCarte);
            ActiviteEnfantCarte modifie = await _context.ActiviteEnfantCartes.FindAsync(1);
            Assert.AreEqual(activiteEnfantCarte, modifie, "ActiviteEnfantCarte différents");


            //restauration des modification
            modifie.Titre = titreOriginal;
            await _controller.PutActiviteEnfantCarte(modifie.ActiviteEnfantCarteId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutActiviteEnfantCarteTest__ReturnsNoContent_AvecMoq()
        {
            ActiviteEnfantCarte activiteEnfantCarte = new ActiviteEnfantCarte()
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteEnfantCarte);
            var activiteEnfantCarteController = new ActiviteEnfantCartesController(mockRepository.Object);


            activiteEnfantCarte.Titre = "a";
            var res = activiteEnfantCarteController.PutActiviteEnfantCarte(activiteEnfantCarte.ActiviteEnfantCarteId, activiteEnfantCarte);

            var actionResult = activiteEnfantCarteController.GetActiviteEnfantCarte(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ActiviteEnfantCarte>), "Pas un ActionResult<ActiviteEnfantCarte>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(ActiviteEnfantCarte), "Pas un ActiviteEnfantCarte");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            activiteEnfantCarte.ActiviteEnfantCarteId = ((ActiviteEnfantCarte)result).ActiviteEnfantCarteId;
            Assert.AreEqual(activiteEnfantCarte, (ActiviteEnfantCarte)result, "ActiviteEnfantCartes pas identiques");
        }

        [TestMethod]
        public void PutActiviteEnfantCarte_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            ActiviteEnfantCarte activiteEnfantCarte = new ActiviteEnfantCarte()
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteEnfantCarte);
            var activiteEnfantCarteController = new ActiviteEnfantCartesController(mockRepository.Object);


            var res = activiteEnfantCarteController.PutActiviteEnfantCarte(15, activiteEnfantCarte);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutActiviteEnfantCarte_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            ActiviteEnfantCarte activiteEnfantCarte = new ActiviteEnfantCarte()
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(activiteEnfantCarte);
            var activiteEnfantCarteController = new ActiviteEnfantCartesController(mockRepository.Object);


            var res = activiteEnfantCarteController.PutActiviteEnfantCarte(99999, activiteEnfantCarte);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostActiviteEnfantCarteTest()
        {
            ActiviteEnfantCarte activiteEnfantCarte = new ActiviteEnfantCarte()
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };

            var res = await _controller.PostActiviteEnfantCarte(activiteEnfantCarte);
            ActiviteEnfantCarte add = _context.ActiviteEnfantCartes.Where(c => c.ActiviteEnfantCarteId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de ActiviteEnfantCarte ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteActiviteEnfantCarte(add.ActiviteEnfantCarteId);
        }

        [TestMethod()]
        public async Task PostActiviteEnfantCarteTest__ReturnsCreateAtAction_AvecMoq()
        {

            ActiviteEnfantCarte activiteEnfantCarte = new ActiviteEnfantCarte()
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            var activiteEnfantCarteController = new ActiviteEnfantCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantCarteController.PostActiviteEnfantCarte(activiteEnfantCarte).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ActiviteEnfantCarte>), "Pas un ActionResult<ActiviteEnfantCarte>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(ActiviteEnfantCarte), "Pas un ActiviteEnfantCarte");
            activiteEnfantCarte.ActiviteEnfantCarteId = ((ActiviteEnfantCarte)result.Value).ActiviteEnfantCarteId;
            Assert.AreEqual(activiteEnfantCarte, (ActiviteEnfantCarte)result.Value, "ActiviteEnfantCartes pas identiques");
        }

        [TestMethod()]
        public async Task DeleteActiviteEnfantCarteTest()
        {
            ActiviteEnfantCarte activiteEnfantCarte = new ActiviteEnfantCarte()
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };
            EntityEntry<ActiviteEnfantCarte> res = _context.ActiviteEnfantCartes.Add(activiteEnfantCarte);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteActiviteEnfantCarte(res.Entity.ActiviteEnfantCarteId);

            ActiviteEnfantCarte domaine = _context.ActiviteEnfantCartes.Where(u => u.ActiviteEnfantCarteId == res.Entity.ActiviteEnfantCarteId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteActiviteEnfantCarteTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            ActiviteEnfantCarte activiteEnfantCarte = new ActiviteEnfantCarte()
            {
                ActiviteEnfantCarteId = 99999,
                Titre = "Nouvelle ActiviteEnfantCarte",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantCarte",
                Frequence = "Nouvelle ActiviteEnfantCarte",
                Prix = 0
            };
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteEnfantCarte);
            var userController = new ActiviteEnfantCartesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteActiviteEnfantCarte(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteActiviteEnfantCarteTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantCarte>>();
            var userController = new ActiviteEnfantCartesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteActiviteEnfantCarte(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
