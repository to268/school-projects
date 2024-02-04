using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiClubMed.Models.Repository;
using Microsoft.EntityFrameworkCore;
using ApiClubMed.Models.EntityFramework;
using ApiClubMed.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace ApiClubMed.Controllers.Tests
{
    [TestClass()]
    public class ActiviteCartesControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly ActiviteCartesController _controller;
        private IDataRepository<ActiviteCarte> _dataRepository;
        public ActiviteCartesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new ActiviteCarteManager(_context);
            _controller = new ActiviteCartesController(_dataRepository);

        }

        [TestMethod()]
        public async Task GetActiviteCartesTest()
        {
            ActionResult<IEnumerable<ActiviteCarte>> activiteCartes = await _controller.GetActiviteCarte();
            var activiteCartes1 = activiteCartes.Value.ToList();
            activiteCartes1 = activiteCartes1.OrderBy(ac => ac.ActiviteCarteId).ToList();

            var activiteCartes2 = _context.ActiviteCartes.ToList();
            activiteCartes2 = activiteCartes2.OrderBy(ac => ac.ActiviteCarteId).ToList();

            CollectionAssert.AreEqual(activiteCartes1, activiteCartes2, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetActiviteCarte_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteCarte activiteCarte1 = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };

            ActiviteCarte activiteCarte2 = new ActiviteCarte
            {
                ActiviteCarteId = 99998,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte2",
                Duree = 6,
                Description = "Nouvelle activite carte2",
                Frequence = "Nouvelle activite carte2",
                AgeMin = 24,
                Prix = 12
            };


            List<ActiviteCarte> lesActiviteCartes = new List<ActiviteCarte>();
            lesActiviteCartes.Add(activiteCarte1);
            lesActiviteCartes.Add(activiteCarte2);


            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesActiviteCartes);
            var activiteCarteController = new ActiviteCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteCarteController.GetActiviteCarte().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesActiviteCartes, actionResult.Value as List<ActiviteCarte>, "ActiviteCarte différent");
        }

        [TestMethod()]
        public async Task GetActiviteCarteTest()
        {
            ActionResult<ActiviteCarte> activiteCarte = await _controller.GetActiviteCarte(1);
            Assert.AreEqual(_context.ActiviteCartes.Where(c => c.ActiviteCarteId == 1).FirstOrDefault(), activiteCarte.Value, "ActiviteCarte différent");
        }

        [TestMethod()]
        public void GetActiviteCarteById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteCarte activiteCarte = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteCarte);
            var activiteCarteController = new ActiviteCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteCarteController.GetActiviteCarte(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(activiteCarte, actionResult.Value as ActiviteCarte);
        }

        [TestMethod]
        public void GetActiviteCarteById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            var vendeurController = new ActiviteCartesController(mockRepository.Object);
            // Act
            var actionResult = vendeurController.GetActiviteCarte(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetActiviteCarteByTitle()
        {
            ActionResult<ActiviteCarte> domainSkiable = await _controller.GetActiviteCarteByTitle("0");
            ActiviteCarte leActiviteCarte = _context.ActiviteCartes.Where(c => c.AgeMin == 0).FirstOrDefault();
            Assert.AreEqual(leActiviteCarte, domainSkiable.Value, "ActiviteCarte différent");
        }

        [TestMethod()]
        public void GetActiviteCarteByTitle_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteCarte activiteCarte = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau type activite").Result).Returns(activiteCarte);
            var activiteCarteController = new ActiviteCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteCarteController.GetActiviteCarteByTitle("Nouveau type activite").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(activiteCarte, actionResult.Value as ActiviteCarte, "ActiviteCarte différent");
        }

        [TestMethod]
        public void GetActiviteCarteByTitle_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            var vendeurController = new ActiviteCartesController(mockRepository.Object);
            // Act
            var actionResult = vendeurController.GetActiviteCarteByTitle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task PutActiviteCarteTest()
        {
            ActiviteCarte activiteCarte = await _context.ActiviteCartes.FindAsync(1);

            string titreOriginal = "Soin visage";

            activiteCarte.Titre = "Soin des pieds";
            await _controller.PutActiviteCarte(activiteCarte.ActiviteCarteId, activiteCarte);
            ActiviteCarte modifie = await _context.ActiviteCartes.FindAsync(1);
            Assert.AreEqual(activiteCarte, modifie, "ActiviteCarte différents");


            //restauration des modification
            modifie.Titre = titreOriginal;
            await _controller.PutActiviteCarte(modifie.ActiviteCarteId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutDomainSkiableTest__ReturnsNoContent_AvecMoq()
        {
            ActiviteCarte activiteCarte = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteCarte);
            var activiteCarteController = new ActiviteCartesController(mockRepository.Object);


            activiteCarte.AgeMin = 0;
            var res = activiteCarteController.PutActiviteCarte(activiteCarte.ActiviteCarteId, activiteCarte);

            var actionResult = activiteCarteController.GetActiviteCarte(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ActiviteCarte>), "Pas un ActionResult<ActiviteCarte>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(ActiviteCarte), "Pas un ActiviteCarte");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            activiteCarte.ActiviteCarteId = ((ActiviteCarte)result).ActiviteCarteId;
            Assert.AreEqual(activiteCarte, (ActiviteCarte)result, "ActiviteCarte pas identiques");
        }

        [TestMethod]
        public void PutActiviteCarte_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            ActiviteCarte activiteCarte = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteCarte);
            var activiteCarteController = new ActiviteCartesController(mockRepository.Object);


            var res = activiteCarteController.PutActiviteCarte(15, activiteCarte);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutActiviteCarte_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            ActiviteCarte activiteCarte = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(activiteCarte);
            var activiteCarteController = new ActiviteCartesController(mockRepository.Object);


            var res = activiteCarteController.PutActiviteCarte(99999, activiteCarte);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostActiviteCarteTest()
        {
            ActiviteCarte activiteCarte = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };

            var res = await _controller.PostActiviteCarte(activiteCarte);
            ActiviteCarte add = _context.ActiviteCartes.Where(c => c.ActiviteCarteId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de ActiviteCarte ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteActiviteCarte(add.ActiviteCarteId);
        }

        [TestMethod()]
        public async Task PostUtilisateurTest__ReturnsCreateAtAction_AvecMoq()
        {

            ActiviteCarte activiteCarte = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            var activiteCarteController = new ActiviteCartesController(mockRepository.Object);
            // Act
            var actionResult = activiteCarteController.PostActiviteCarte(activiteCarte).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ActiviteCarte>), "Pas un ActionResult<DomainSkiable>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(ActiviteCarte), "Pas un ActiviteCarte");
            activiteCarte.ActiviteCarteId = ((ActiviteCarte)result.Value).ActiviteCarteId;
            Assert.AreEqual(activiteCarte, (ActiviteCarte)result.Value, "ActiviteCarte pas identiques");
        }

        [TestMethod()]
        public async Task DeleteActiviteCarteTest()
        {
            ActiviteCarte activiteCarte = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };
            EntityEntry<ActiviteCarte> res = _context.ActiviteCartes.Add(activiteCarte);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteActiviteCarte(res.Entity.ActiviteCarteId);

            ActiviteCarte domaine = _context.ActiviteCartes.Where(u => u.ActiviteCarteId == res.Entity.ActiviteCarteId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteDomainSkiableTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            ActiviteCarte activiteCarte = new ActiviteCarte
            {
                ActiviteCarteId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
                Prix = 42
            };
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteCarte);
            var userController = new ActiviteCartesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteActiviteCarte(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteDomainSkiableTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteCarte>>();
            var userController = new ActiviteCartesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteActiviteCarte(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
