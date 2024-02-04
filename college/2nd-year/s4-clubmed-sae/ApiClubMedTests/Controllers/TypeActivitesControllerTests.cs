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
    public class TypeActivitesControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly TypeActivitesController _controller;
        private IDataRepository<TypeActivite> _dataRepository;
        public TypeActivitesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new TypeActiviteManager(_context);
            _controller = new TypeActivitesController(_dataRepository);

        }

        [TestMethod()]
        public async Task GetTypeActivitesTest()
        {
            ActionResult<IEnumerable<TypeActivite>> typeActivites = await _controller.GetTypeActivites();
            var typeActivites1 = typeActivites.Value.ToList();
            typeActivites1 = typeActivites1.OrderBy(ta => ta.TypeActiviteId).ToList();

            var typeActivites2 = _context.TypeActivites.ToList();
            typeActivites2 = typeActivites2.OrderBy(ta => ta.TypeActiviteId).ToList();

            CollectionAssert.AreEqual(typeActivites1, typeActivites2, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetTypeActivite_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeActivite typeActivite1 = new TypeActivite
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };

            TypeActivite typeActivite2 = new TypeActivite
            {
                TypeActiviteId = 99998,
                PhotoId = 3,
                Titre = "Nouveau type activite2",
                Description = "Nouveau type activite2",
                NbActiviteIncluse = 99998,
                NbActiviteCarte = 99998,
            };


            List<TypeActivite> lesTypeActivites = new List<TypeActivite>();
            lesTypeActivites.Add(typeActivite1);
            lesTypeActivites.Add(typeActivite2);


            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesTypeActivites);
            var typeActiviteController = new TypeActivitesController(mockRepository.Object);
            // Act
            var actionResult = typeActiviteController.GetTypeActivites().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesTypeActivites, actionResult.Value as List<TypeActivite>, "TypeActivite différent");
        }

        [TestMethod()]
        public async Task GetTypeActiviteTest()
        {
            ActionResult<TypeActivite> typeActivite = await _controller.GetTypeActivite(1);
            Assert.AreEqual(_context.TypeActivites.Where(c => c.TypeActiviteId == 1).FirstOrDefault(), typeActivite.Value, "TypeActivite différent");
        }

        [TestMethod()]
        public void GetTypeActiviteById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeActivite typeActivite = new TypeActivite
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeActivite);
            var typeActiviteController = new TypeActivitesController(mockRepository.Object);
            // Act
            var actionResult = typeActiviteController.GetTypeActivite(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(typeActivite, actionResult.Value as TypeActivite);
        }

        [TestMethod]
        public void GetTypeActiviteById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            var vendeurController = new TypeActivitesController(mockRepository.Object);
            // Act
            var actionResult = vendeurController.GetTypeActivite(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetTypeActiviteByTitle()
        {
            ActionResult<TypeActivite> domainSkiable = await _controller.GetTypeActiviteByTitle("Club Med Spa by Sothys");
            TypeActivite leTypeActivite = _context.TypeActivites.Where(c => c.Titre == "Club Med Spa by Sothys").FirstOrDefault();
            Assert.AreEqual(leTypeActivite, domainSkiable.Value, "TypeActivite différent");
        }

        [TestMethod()]
        public void GetTypeActiviteByTitle_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeActivite typeActivite = new TypeActivite
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau type activite").Result).Returns(typeActivite);
            var typeActiviteController = new TypeActivitesController(mockRepository.Object);
            // Act
            var actionResult = typeActiviteController.GetTypeActiviteByTitle("Nouveau type activite").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(typeActivite, actionResult.Value as TypeActivite, "TypeActivite différent");
        }

        [TestMethod]
        public void GetTypeActiviteByTitle_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            var vendeurController = new TypeActivitesController(mockRepository.Object);
            // Act
            var actionResult = vendeurController.GetTypeActiviteByTitle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutTypeActiviteTest()
        {
            TypeActivite typeActivite = await _context.TypeActivites.FindAsync(1);

            string titreOriginal = "Club Med Spa by Sothys";

            typeActivite.Titre = "Le test";
            await _controller.PutTypeActivite(typeActivite.TypeActiviteId, typeActivite);
            TypeActivite modifie = await _context.TypeActivites.FindAsync(1);
            Assert.AreEqual(typeActivite, modifie, "TypeActivite différents");


            //restauration des modification
            modifie.Titre = titreOriginal;
            await _controller.PutTypeActivite(modifie.TypeActiviteId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutDomainSkiableTest__ReturnsNoContent_AvecMoq()
        {
            TypeActivite typeActivite = new TypeActivite()
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeActivite);
            var typeActiviteController = new TypeActivitesController(mockRepository.Object);


            typeActivite.Titre = "a";
            var res = typeActiviteController.PutTypeActivite(typeActivite.TypeActiviteId, typeActivite);

            var actionResult = typeActiviteController.GetTypeActivite(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeActivite>), "Pas un ActionResult<TypeActivite>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(TypeActivite), "Pas un TypeActivite");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            typeActivite.TypeActiviteId = ((TypeActivite)result).TypeActiviteId;
            Assert.AreEqual(typeActivite, (TypeActivite)result, "TypeActivite pas identiques");
        }

        [TestMethod]
        public void PutTypeActivite_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            TypeActivite typeActivite = new TypeActivite()
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeActivite);
            var typeActiviteController = new TypeActivitesController(mockRepository.Object);


            var res = typeActiviteController.PutTypeActivite(15, typeActivite);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutTypeActivite_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            TypeActivite typeActivite = new TypeActivite()
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(typeActivite);
            var typeActiviteController = new TypeActivitesController(mockRepository.Object);


            var res = typeActiviteController.PutTypeActivite(99999, typeActivite);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostTypeActiviteTest()
        {
            TypeActivite typeActivite = new TypeActivite()
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };

            var res = await _controller.PostTypeActivite(typeActivite);
            TypeActivite add = _context.TypeActivites.Where(c => c.TypeActiviteId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de TypeActivite ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteTypeActivite(add.TypeActiviteId);
        }

        [TestMethod()]
        public async Task PostUtilisateurTest__ReturnsCreateAtAction_AvecMoq()
        {

            TypeActivite typeActivite = new TypeActivite()
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            var typeActiviteController = new TypeActivitesController(mockRepository.Object);
            // Act
            var actionResult = typeActiviteController.PostTypeActivite(typeActivite).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeActivite>), "Pas un ActionResult<DomainSkiable>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(TypeActivite), "Pas un TypeActivite");
            typeActivite.TypeActiviteId = ((TypeActivite)result.Value).TypeActiviteId;
            Assert.AreEqual(typeActivite, (TypeActivite)result.Value, "TypeActivite pas identiques");
        }

        [TestMethod()]
        public async Task DeleteTypeActiviteTest()
        {
            TypeActivite typeActivite = new TypeActivite()
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };
            EntityEntry<TypeActivite> res = _context.TypeActivites.Add(typeActivite);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteTypeActivite(res.Entity.TypeActiviteId);

            TypeActivite domaine = _context.TypeActivites.Where(u => u.TypeActiviteId == res.Entity.TypeActiviteId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteDomainSkiableTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            TypeActivite typeActivite = new TypeActivite()
            {
                TypeActiviteId = 99999,
                PhotoId = 2,
                Titre = "Nouveau type activite",
                Description = "Nouveau type activite",
                NbActiviteIncluse = 99999,
                NbActiviteCarte = 99999,
            };
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeActivite);
            var userController = new TypeActivitesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTypeActivite(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteDomainSkiableTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeActivite>>();
            var userController = new TypeActivitesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTypeActivite(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
