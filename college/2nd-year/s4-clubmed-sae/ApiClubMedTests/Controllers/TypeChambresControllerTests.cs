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
    public class TypeChambresControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly TypeChambresController _controller;
        private IDataRepository<TypeChambre> _dataRepository;
        public TypeChambresControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new TypeChambreManager(_context);
            _controller = new TypeChambresController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetTypeChambresTest()
        {
            ActionResult<IEnumerable<TypeChambre>> typeChambres = await _controller.GetTypeChambres();
            var typeChambre1 = typeChambres.Value.ToList();
            typeChambre1 = typeChambre1.OrderBy(ap => ap.TypeChambreId).ToList();

            var typeChambre2 = _context.TypeChambres.ToList();
            typeChambre2 = typeChambre2.OrderBy(ap => ap.TypeChambreId).ToList();

            CollectionAssert.AreEqual(typeChambre2, typeChambre1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetTypeChambres_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeChambre typeChambre1 = new TypeChambre
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            TypeChambre typeChambre2 = new TypeChambre
            {
                TypeChambreId = 99998,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            List<TypeChambre> lesTypeChambres = new List<TypeChambre>();
            lesTypeChambres.Add(typeChambre1);
            lesTypeChambres.Add(typeChambre2);


            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesTypeChambres);
            var typeChambreController = new TypeChambresController(mockRepository.Object);
            // Act
            var actionResult = typeChambreController.GetTypeChambres().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesTypeChambres, actionResult.Value as List<TypeChambre>, "TypeChambre différent");
        }

        [TestMethod()]
        public async Task GetTypeChambreTest()
        {
            ActionResult<TypeChambre> typeChambre = await _controller.GetTypeChambre(1);
            Assert.AreEqual(_context.TypeChambres.Where(c => c.TypeChambreId == 1).FirstOrDefault(), typeChambre.Value, "TypeChambre différent");
        }

        [TestMethod()]
        public void GetTypeChambreById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeChambre typeChambre = new TypeChambre
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeChambre);
            var typeChambreController = new TypeChambresController(mockRepository.Object);
            // Act
            var actionResult = typeChambreController.GetTypeChambre(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(typeChambre, actionResult.Value as TypeChambre);
        }

        [TestMethod]
        public void GetTypeChambreById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            var typeChambreController = new TypeChambresController(mockRepository.Object);
            // Act
            var actionResult = typeChambreController.GetTypeChambre(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetTypeChambreByLibelleTest()
        {
            ActionResult<TypeChambre> typeChambre = await _controller.GetTypeChambreByLibelle("Chambre Supérieure - Accès Jardin");
            TypeChambre leTypeChambre = _context.TypeChambres.Where(c => c.LibelleCatgorie == "Chambre Supérieure - Accès Jardin").FirstOrDefault();
            Assert.IsNotNull(typeChambre);
            Assert.AreEqual(leTypeChambre, typeChambre.Value, "TypeChambre différent");
        }

        [TestMethod()]
        public void GetTypeChambreByLibelle_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeChambre typeChambre = new TypeChambre
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau TypeChambre").Result).Returns(typeChambre);
            var typeChambreController = new TypeChambresController(mockRepository.Object);
            // Act
            var actionResult = typeChambreController.GetTypeChambreByLibelle("Nouveau TypeChambre").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(typeChambre, actionResult.Value as TypeChambre, "TypeChambre différent");
        }

        [TestMethod]
        public void GetTypeChambreByLibelle_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            var typeChambreController = new TypeChambresController(mockRepository.Object);
            // Act
            var actionResult = typeChambreController.GetTypeChambreByLibelle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutTypeChambreTest()
        {
            TypeChambre typeChambre = await _context.TypeChambres.FindAsync(1);

            string nomOriginal = typeChambre.LibelleCatgorie;

            typeChambre.LibelleCatgorie = "Le test";
            await _controller.PutTypeChambre(typeChambre.TypeChambreId, typeChambre);
            TypeChambre modifie = await _context.TypeChambres.FindAsync(1);
            Assert.AreEqual(typeChambre, modifie, "TypeChambre différents");


            //restauration des modification
            modifie.LibelleCatgorie = nomOriginal;
            await _controller.PutTypeChambre(modifie.TypeChambreId, modifie);
        }

        [TestMethod]
        public void PutTypeChambreTest__ReturnsNoContent_AvecMoq()
        {
            TypeChambre typeChambre = new TypeChambre()
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeChambre);
            var typeChambreController = new TypeChambresController(mockRepository.Object);


            typeChambre.LibelleCatgorie = "a";
            var res = typeChambreController.PutTypeChambre(typeChambre.TypeChambreId, typeChambre);

            var actionResult = typeChambreController.GetTypeChambre(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeChambre>), "Pas un ActionResult<TypeChambre>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(TypeChambre), "Pas un TypeChambre");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            typeChambre.TypeChambreId = ((TypeChambre)result).TypeChambreId;
            Assert.AreEqual(typeChambre, (TypeChambre)result, "TypeChambres pas identiques");
        }

        [TestMethod]
        public void PutTypeChambre_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            TypeChambre typeChambre = new TypeChambre()
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeChambre);
            var typeChambreController = new TypeChambresController(mockRepository.Object);

            var res = typeChambreController.PutTypeChambre(15, typeChambre);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutTypeChambre_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            TypeChambre typeChambre = new TypeChambre()
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(typeChambre);
            var typeChambreController = new TypeChambresController(mockRepository.Object);

            var res = typeChambreController.PutTypeChambre(99999, typeChambre);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostTypeChambreTest()
        {
            TypeChambre typeChambre = new TypeChambre()
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            var res = await _controller.PostTypeChambre(typeChambre);
            TypeChambre add = _context.TypeChambres.Where(c => c.TypeChambreId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de TypeChambre ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteTypeChambre(add.TypeChambreId);
        }

        [TestMethod()]
        public async Task PostTypeChambreTest__ReturnsCreateAtAction_AvecMoq()
        {

            TypeChambre typeChambre = new TypeChambre()
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            var typeChambreController = new TypeChambresController(mockRepository.Object);
            // Act
            var actionResult = typeChambreController.PostTypeChambre(typeChambre).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeChambre>), "Pas un ActionResult<TypeChambre>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(TypeChambre), "Pas un TypeChambre");
            typeChambre.TypeChambreId = ((TypeChambre)result.Value).TypeChambreId;
            Assert.AreEqual(typeChambre, (TypeChambre)result.Value, "TypeChambres pas identiques");
        }

        [TestMethod()]
        public async Task DeleteTypeChambreTest()
        {
            TypeChambre typeChambre = new TypeChambre()
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            EntityEntry<TypeChambre> res = _context.TypeChambres.Add(typeChambre);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteTypeChambre(res.Entity.TypeChambreId);

            TypeChambre typeChambre1 = _context.TypeChambres.Where(u => u.TypeChambreId == res.Entity.TypeChambreId).FirstOrDefault();

            Assert.IsNull(typeChambre1, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteTypeChambreTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            TypeChambre typeChambre = new TypeChambre()
            {
                TypeChambreId = 99999,
                ResortId = 2,
                CategorieTypeChambreId = 2,
                Surface = 319,
                Capacite = 12,
                Presentation = "Nouveau TypeChambre",
                LibelleCatgorie = "Nouveau TypeChambre",
                PrixJournalier = 64,
            };

            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeChambre);
            var userController = new TypeChambresController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTypeChambre(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteTypeChambreTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeChambre>>();
            var userController = new TypeChambresController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTypeChambre(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
