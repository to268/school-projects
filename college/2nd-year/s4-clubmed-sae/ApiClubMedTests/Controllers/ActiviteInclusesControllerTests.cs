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
    public class ActiviteInclusesControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly ActiviteInclusesController _controller;
        private IDataRepository<ActiviteIncluse> _dataRepository;
        public ActiviteInclusesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new ActiviteIncluseManager(_context);
            _controller = new ActiviteInclusesController(_dataRepository);

        }

        [TestMethod()]
        public async Task GetActiviteInclusesTest()
        {
            ActionResult<IEnumerable<ActiviteIncluse>> activiteIncluses = await _controller.GetActiviteIncluses();
            var activiteIncluses1 = activiteIncluses.Value.ToList();
            activiteIncluses1 = activiteIncluses1.OrderBy(ai => ai.ActiviteIncluseId).ToList();

            var activiteIncluses2 = _context.ActiviteIncluses.ToList();
            activiteIncluses2 = activiteIncluses2.OrderBy(ai => ai.ActiviteIncluseId).ToList();

            CollectionAssert.AreEqual(activiteIncluses1, activiteIncluses2, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetActiviteIncluse_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteIncluse activiteIncluse1 = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };

            ActiviteIncluse activiteIncluse2 = new ActiviteIncluse
            {
                ActiviteIncluseId = 99998,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte2",
                Duree = 6,
                Description = "Nouvelle activite carte2",
                Frequence = "Nouvelle activite carte2",
                AgeMin = 24,
            };


            List<ActiviteIncluse> lesActiviteIncluses = new List<ActiviteIncluse>();
            lesActiviteIncluses.Add(activiteIncluse1);
            lesActiviteIncluses.Add(activiteIncluse2);


            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesActiviteIncluses);
            var activiteIncluseController = new ActiviteInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteIncluseController.GetActiviteIncluses().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesActiviteIncluses, actionResult.Value as List<ActiviteIncluse>, "ActiviteIncluse différent");
        }

        [TestMethod()]
        public async Task GetActiviteIncluseTest()
        {
            ActionResult<ActiviteIncluse> activiteIncluse = await _controller.GetActiviteIncluse(1);
            Assert.AreEqual(_context.ActiviteIncluses.Where(c => c.ActiviteIncluseId == 1).FirstOrDefault(), activiteIncluse.Value, "ActiviteIncluse différent");
        }

        [TestMethod()]
        public void GetActiviteIncluseById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteIncluse activiteIncluse = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteIncluse);
            var activiteIncluseController = new ActiviteInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteIncluseController.GetActiviteIncluse(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(activiteIncluse, actionResult.Value as ActiviteIncluse);
        }

        [TestMethod]
        public void GetActiviteIncluseById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            var vendeurController = new ActiviteInclusesController(mockRepository.Object);
            // Act
            var actionResult = vendeurController.GetActiviteIncluse(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetActiviteIncluseByTitle()
        {
            ActionResult<ActiviteIncluse> activiteIncluse = await _controller.GetActiviteIncluseByTitle("Cours collectifs de Golf");
            ActiviteIncluse leActiviteIncluse = _context.ActiviteIncluses.Where(c => c.Titre == "Cours collectifs de Golf").FirstOrDefault();
            Assert.AreEqual(leActiviteIncluse, activiteIncluse.Value, "ActiviteIncluse différent");
        }

        [TestMethod()]
        public void GetActiviteIncluseByTitle_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteIncluse activiteIncluse = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau type activite").Result).Returns(activiteIncluse);
            var activiteIncluseController = new ActiviteInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteIncluseController.GetActiviteIncluseByTitle("Nouveau type activite").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(activiteIncluse, actionResult.Value as ActiviteIncluse, "ActiviteIncluse différent");
        }

        [TestMethod]
        public void GetActiviteIncluseByTitle_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            var vendeurController = new ActiviteInclusesController(mockRepository.Object);
            // Act
            var actionResult = vendeurController.GetActiviteIncluseByTitle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task PutActiviteIncluseTest()
        {
            ActiviteIncluse activiteIncluse = await _context.ActiviteIncluses.FindAsync(1);

            string titreOriginal = "Cours collectifs de Golf";

            activiteIncluse.Titre = "Cours collectifs de Cheval";
            await _controller.PutActiviteIncluse(activiteIncluse.ActiviteIncluseId, activiteIncluse);
            ActiviteIncluse modifie = await _context.ActiviteIncluses.FindAsync(1);
            Assert.AreEqual(activiteIncluse, modifie, "ActiviteIncluse différents");


            //restauration des modification
            modifie.Titre = titreOriginal;
            await _controller.PutActiviteIncluse(modifie.ActiviteIncluseId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutDomainSkiableTest__ReturnsNoContent_AvecMoq()
        {
            ActiviteIncluse activiteIncluse = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteIncluse);
            var activiteIncluseController = new ActiviteInclusesController(mockRepository.Object);


            activiteIncluse.AgeMin = 0;
            var res = activiteIncluseController.PutActiviteIncluse(activiteIncluse.ActiviteIncluseId, activiteIncluse);

            var actionResult = activiteIncluseController.GetActiviteIncluse(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ActiviteIncluse>), "Pas un ActionResult<ActiviteIncluse>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(ActiviteIncluse), "Pas un ActiviteIncluse");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            activiteIncluse.ActiviteIncluseId = ((ActiviteIncluse)result).ActiviteIncluseId;
            Assert.AreEqual(activiteIncluse, (ActiviteIncluse)result, "ActiviteIncluse pas identiques");
        }

        [TestMethod]
        public void PutActiviteIncluse_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            ActiviteIncluse activiteIncluse = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteIncluse);
            var activiteIncluseController = new ActiviteInclusesController(mockRepository.Object);


            var res = activiteIncluseController.PutActiviteIncluse(15, activiteIncluse);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutActiviteIncluse_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            ActiviteIncluse activiteIncluse = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(activiteIncluse);
            var activiteIncluseController = new ActiviteInclusesController(mockRepository.Object);


            var res = activiteIncluseController.PutActiviteIncluse(99999, activiteIncluse);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostActiviteIncluseTest()
        {
            ActiviteIncluse activiteIncluse = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };

            var res = await _controller.PostActiviteIncluse(activiteIncluse);
            ActiviteIncluse add = _context.ActiviteIncluses.Where(c => c.ActiviteIncluseId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de ActiviteIncluse ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteActiviteIncluse(add.ActiviteIncluseId);
        }

        [TestMethod()]
        public async Task PostUtilisateurTest__ReturnsCreateAtAction_AvecMoq()
        {

            ActiviteIncluse activiteIncluse = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            var activiteIncluseController = new ActiviteInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteIncluseController.PostActiviteIncluse(activiteIncluse).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ActiviteIncluse>), "Pas un ActionResult<DomainSkiable>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(ActiviteIncluse), "Pas un ActiviteIncluse");
            activiteIncluse.ActiviteIncluseId = ((ActiviteIncluse)result.Value).ActiviteIncluseId;
            Assert.AreEqual(activiteIncluse, (ActiviteIncluse)result.Value, "ActiviteIncluse pas identiques");
        }

        [TestMethod()]
        public async Task DeleteActiviteIncluseTest()
        {
            ActiviteIncluse activiteIncluse = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };
            EntityEntry<ActiviteIncluse> res = _context.ActiviteIncluses.Add(activiteIncluse);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteActiviteIncluse(res.Entity.ActiviteIncluseId);

            ActiviteIncluse domaine = _context.ActiviteIncluses.Where(u => u.ActiviteIncluseId == res.Entity.ActiviteIncluseId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteDomainSkiableTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            ActiviteIncluse activiteIncluse = new ActiviteIncluse
            {
                ActiviteIncluseId = 99999,
                TypeActiviteId = 1,
                Titre = "Nouvelle activite carte",
                Duree = 3,
                Description = "Nouvelle activite carte",
                Frequence = "Nouvelle activite carte",
                AgeMin = 18,
            };
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteIncluse);
            var userController = new ActiviteInclusesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteActiviteIncluse(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteDomainSkiableTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteIncluse>>();
            var userController = new ActiviteInclusesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteActiviteIncluse(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
