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
    public class CategorieTypeChambresControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly CategorieTypeChambresController _controller;
        private IDataRepository<CategorieTypeChambre> _dataRepository;
        public CategorieTypeChambresControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new CategorieTypeChambreManager(_context);
            _controller = new CategorieTypeChambresController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetCategorieTypeChambresTest()
        {
            ActionResult<IEnumerable<CategorieTypeChambre>> categorieTypeChambres = await _controller.GetCategorieTypeChambres();
            var categorieTypeChambre1 = categorieTypeChambres.Value.ToList();
            categorieTypeChambre1 = categorieTypeChambre1.OrderBy(ap => ap.CategorieTypeChambreId).ToList();

            var categorieTypeChambre2 = _context.CategorieTypeChambres.ToList();
            categorieTypeChambre2 = categorieTypeChambre2.OrderBy(ap => ap.CategorieTypeChambreId).ToList();

            CollectionAssert.AreEqual(categorieTypeChambre2, categorieTypeChambre1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetCategorieTypeChambres_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            CategorieTypeChambre categorieTypeChambre1 = new CategorieTypeChambre
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            CategorieTypeChambre categorieTypeChambre2 = new CategorieTypeChambre
            {
                CategorieTypeChambreId = 99998,


                Libelle = "Nouveau CategorieTypeChambre2",

            };

            List<CategorieTypeChambre> lesCategorieTypeChambres = new List<CategorieTypeChambre>();
            lesCategorieTypeChambres.Add(categorieTypeChambre1);
            lesCategorieTypeChambres.Add(categorieTypeChambre2);


            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesCategorieTypeChambres);
            var categorieTypeChambreController = new CategorieTypeChambresController(mockRepository.Object);
            // Act
            var actionResult = categorieTypeChambreController.GetCategorieTypeChambres().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesCategorieTypeChambres, actionResult.Value as List<CategorieTypeChambre>, "CategorieTypeChambre différent");
        }

        [TestMethod()]
        public async Task GetCategorieTypeChambreTest()
        {
            ActionResult<CategorieTypeChambre> categorieTypeChambre = await _controller.GetCategorieTypeChambre(1);
            Assert.AreEqual(_context.CategorieTypeChambres.Where(c => c.CategorieTypeChambreId == 1).FirstOrDefault(), categorieTypeChambre.Value, "CategorieTypeChambre différent");
        }

        [TestMethod()]
        public void GetCategorieTypeChambreById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            CategorieTypeChambre categorieTypeChambre = new CategorieTypeChambre
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(categorieTypeChambre);
            var categorieTypeChambreController = new CategorieTypeChambresController(mockRepository.Object);
            // Act
            var actionResult = categorieTypeChambreController.GetCategorieTypeChambre(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(categorieTypeChambre, actionResult.Value as CategorieTypeChambre);
        }

        [TestMethod]
        public void GetCategorieTypeChambreById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            var categorieTypeChambreController = new CategorieTypeChambresController(mockRepository.Object);
            // Act
            var actionResult = categorieTypeChambreController.GetCategorieTypeChambre(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetCategorieTypeChambreByLibelleTest()
        {
            ActionResult<CategorieTypeChambre> categorieTypeChambre = await _controller.GetCategorieTypeChambreByLibelle("Supérieur");
            CategorieTypeChambre leCategorieTypeChambre = _context.CategorieTypeChambres.Where(c => c.Libelle == "Supérieur").FirstOrDefault();
            Assert.IsNotNull(categorieTypeChambre);
            Assert.AreEqual(leCategorieTypeChambre, categorieTypeChambre.Value, "CategorieTypeChambre différent");
        }

        [TestMethod()]
        public void GetCategorieTypeChambreByLibelle_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            CategorieTypeChambre categorieTypeChambre = new CategorieTypeChambre
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau CategorieTypeChambre").Result).Returns(categorieTypeChambre);
            var categorieTypeChambreController = new CategorieTypeChambresController(mockRepository.Object);
            // Act
            var actionResult = categorieTypeChambreController.GetCategorieTypeChambreByLibelle("Nouveau CategorieTypeChambre").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(categorieTypeChambre, actionResult.Value as CategorieTypeChambre, "CategorieTypeChambre différent");
        }

        [TestMethod]
        public void GetCategorieTypeChambreByLibelle_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            var categorieTypeChambreController = new CategorieTypeChambresController(mockRepository.Object);
            // Act
            var actionResult = categorieTypeChambreController.GetCategorieTypeChambreByLibelle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutCategorieTypeChambreTest()
        {
            CategorieTypeChambre categorieTypeChambre = await _context.CategorieTypeChambres.FindAsync(1);

            string nomOriginal = categorieTypeChambre.Libelle;

            categorieTypeChambre.Libelle = "Le test";
            await _controller.PutCategorieTypeChambre(categorieTypeChambre.CategorieTypeChambreId, categorieTypeChambre);
            CategorieTypeChambre modifie = await _context.CategorieTypeChambres.FindAsync(1);
            Assert.AreEqual(categorieTypeChambre, modifie, "CategorieTypeChambre différents");


            //restauration des modification
            modifie.Libelle = nomOriginal;
            await _controller.PutCategorieTypeChambre(modifie.CategorieTypeChambreId, modifie);
        }

        [TestMethod]
        public void PutCategorieTypeChambreTest__ReturnsNoContent_AvecMoq()
        {
            CategorieTypeChambre categorieTypeChambre = new CategorieTypeChambre()
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(categorieTypeChambre);
            var categorieTypeChambreController = new CategorieTypeChambresController(mockRepository.Object);


            categorieTypeChambre.Libelle = "a";
            var res = categorieTypeChambreController.PutCategorieTypeChambre(categorieTypeChambre.CategorieTypeChambreId, categorieTypeChambre);

            var actionResult = categorieTypeChambreController.GetCategorieTypeChambre(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CategorieTypeChambre>), "Pas un ActionResult<CategorieTypeChambre>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(CategorieTypeChambre), "Pas un CategorieTypeChambre");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            categorieTypeChambre.CategorieTypeChambreId = ((CategorieTypeChambre)result).CategorieTypeChambreId;
            Assert.AreEqual(categorieTypeChambre, (CategorieTypeChambre)result, "CategorieTypeChambres pas identiques");
        }

        [TestMethod]
        public void PutCategorieTypeChambre_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            CategorieTypeChambre categorieTypeChambre = new CategorieTypeChambre()
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(categorieTypeChambre);
            var categorieTypeChambreController = new CategorieTypeChambresController(mockRepository.Object);

            var res = categorieTypeChambreController.PutCategorieTypeChambre(15, categorieTypeChambre);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutCategorieTypeChambre_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            CategorieTypeChambre categorieTypeChambre = new CategorieTypeChambre()
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(categorieTypeChambre);
            var categorieTypeChambreController = new CategorieTypeChambresController(mockRepository.Object);

            var res = categorieTypeChambreController.PutCategorieTypeChambre(99999, categorieTypeChambre);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostCategorieTypeChambreTest()
        {
            CategorieTypeChambre categorieTypeChambre = new CategorieTypeChambre()
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            var res = await _controller.PostCategorieTypeChambre(categorieTypeChambre);
            CategorieTypeChambre add = _context.CategorieTypeChambres.Where(c => c.CategorieTypeChambreId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de CategorieTypeChambre ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteCategorieTypeChambre(add.CategorieTypeChambreId);
        }

        [TestMethod()]
        public async Task PostCategorieTypeChambreTest__ReturnsCreateAtAction_AvecMoq()
        {

            CategorieTypeChambre categorieTypeChambre = new CategorieTypeChambre()
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            var categorieTypeChambreController = new CategorieTypeChambresController(mockRepository.Object);
            // Act
            var actionResult = categorieTypeChambreController.PostCategorieTypeChambre(categorieTypeChambre).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CategorieTypeChambre>), "Pas un ActionResult<CategorieTypeChambre>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CategorieTypeChambre), "Pas un CategorieTypeChambre");
            categorieTypeChambre.CategorieTypeChambreId = ((CategorieTypeChambre)result.Value).CategorieTypeChambreId;
            Assert.AreEqual(categorieTypeChambre, (CategorieTypeChambre)result.Value, "CategorieTypeChambres pas identiques");
        }

        [TestMethod()]
        public async Task DeleteCategorieTypeChambreTest()
        {
            CategorieTypeChambre categorieTypeChambre = new CategorieTypeChambre()
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            EntityEntry<CategorieTypeChambre> res = _context.CategorieTypeChambres.Add(categorieTypeChambre);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteCategorieTypeChambre(res.Entity.CategorieTypeChambreId);

            CategorieTypeChambre categorieTypeChambre1 = _context.CategorieTypeChambres.Where(u => u.CategorieTypeChambreId == res.Entity.CategorieTypeChambreId).FirstOrDefault();

            Assert.IsNull(categorieTypeChambre1, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteCategorieTypeChambreTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            CategorieTypeChambre categorieTypeChambre = new CategorieTypeChambre()
            {
                CategorieTypeChambreId = 99999,


                Libelle = "Nouveau CategorieTypeChambre",

            };

            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(categorieTypeChambre);
            var userController = new CategorieTypeChambresController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteCategorieTypeChambre(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteCategorieTypeChambreTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<CategorieTypeChambre>>();
            var userController = new CategorieTypeChambresController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteCategorieTypeChambre(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}