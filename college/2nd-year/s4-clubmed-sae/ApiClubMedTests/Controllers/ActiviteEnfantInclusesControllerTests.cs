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
    public class ActiviteEnfantInclusesControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly ActiviteEnfantInclusesController _controller;
        private IDataRepository<ActiviteEnfantIncluse> _dataRepository;
        public ActiviteEnfantInclusesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new ActiviteEnfantIncluseManager(_context);
            _controller = new ActiviteEnfantInclusesController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetActiviteEnfantInclusesTest()
        {
            ActionResult<IEnumerable<ActiviteEnfantIncluse>> activiteEnfantIncluses = await _controller.GetActiviteEnfantIncluses();
            var activiteEnfantIncluses1 = activiteEnfantIncluses.Value.ToList();
            activiteEnfantIncluses1 = activiteEnfantIncluses1.OrderBy(aei => aei.ActiviteEnfantIncluseId).ToList();

            var activiteEnfantIncluses2 = _context.ActiviteEnfantIncluses.ToList();
            activiteEnfantIncluses2 = activiteEnfantIncluses2.OrderBy(aei => aei.ActiviteEnfantIncluseId).ToList();

            CollectionAssert.AreEqual(activiteEnfantIncluses2, activiteEnfantIncluses1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetActiviteEnfantIncluses_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteEnfantIncluse activiteEnfantIncluse1 = new ActiviteEnfantIncluse
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };

            ActiviteEnfantIncluse activiteEnfantIncluse2 = new ActiviteEnfantIncluse
            {
                ActiviteEnfantIncluseId = 99998,
                Titre = "Nouvelle ActiviteEnfantIncluse2",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse2",
                Frequence = "Nouvelle ActiviteEnfantIncluse2",
            };


            List<ActiviteEnfantIncluse> lesActiviteEnfantIncluses = new List<ActiviteEnfantIncluse>();
            lesActiviteEnfantIncluses.Add(activiteEnfantIncluse1);
            lesActiviteEnfantIncluses.Add(activiteEnfantIncluse2);


            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesActiviteEnfantIncluses);
            var activiteEnfantIncluseController = new ActiviteEnfantInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantIncluseController.GetActiviteEnfantIncluses().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesActiviteEnfantIncluses, actionResult.Value as List<ActiviteEnfantIncluse>, "ActiviteEnfantIncluse différent");
        }

        [TestMethod()]
        public async Task GetActiviteEnfantIncluseTest()
        {
            ActionResult<ActiviteEnfantIncluse> activiteEnfantIncluse = await _controller.GetActiviteEnfantIncluse(1);
            Assert.AreEqual(_context.ActiviteEnfantIncluses.Where(c => c.ActiviteEnfantIncluseId == 1).FirstOrDefault(), activiteEnfantIncluse.Value, "ActiviteEnfantIncluse différent");
        }

        [TestMethod()]
        public void GetActiviteEnfantIncluseById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteEnfantIncluse activiteEnfantIncluse = new ActiviteEnfantIncluse
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteEnfantIncluse);
            var activiteEnfantIncluseController = new ActiviteEnfantInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantIncluseController.GetActiviteEnfantIncluse(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(activiteEnfantIncluse, actionResult.Value as ActiviteEnfantIncluse);
        }

        [TestMethod]
        public void GetActiviteEnfantIncluseById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            var activiteEnfantIncluseController = new ActiviteEnfantInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantIncluseController.GetActiviteEnfantIncluse(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetActiviteEnfantIncluseByTitleTest()
        {
            ActionResult<ActiviteEnfantIncluse> activiteEnfantIncluse = await _controller.GetActiviteByTitle("Tournois et jeux");
            ActiviteEnfantIncluse laActiviteEnfantIncluse = _context.ActiviteEnfantIncluses.Where(c => c.Titre == "Tournois et jeux").FirstOrDefault();
            Assert.IsNotNull(laActiviteEnfantIncluse);
            Assert.AreEqual(laActiviteEnfantIncluse, activiteEnfantIncluse.Value, "ActiviteEnfantIncluse différent");
        }

        [TestMethod()]
        public void GetActiviteEnfantIncluseByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            ActiviteEnfantIncluse activiteEnfantIncluse = new ActiviteEnfantIncluse
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            mockRepository.Setup(x => x.GetByStringAsync("Tournois et jeux").Result).Returns(activiteEnfantIncluse);
            var activiteEnfantIncluseController = new ActiviteEnfantInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantIncluseController.GetActiviteByTitle("Tournois et jeux").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(activiteEnfantIncluse, actionResult.Value as ActiviteEnfantIncluse, "ActiviteEnfantIncluse différent");
        }

        [TestMethod]
        public void GetActiviteEnfantIncluseByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            var activiteEnfantIncluseController = new ActiviteEnfantInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantIncluseController.GetActiviteByTitle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutActiviteEnfantIncluseTest()
        {
            ActiviteEnfantIncluse activiteEnfantIncluse = await _context.ActiviteEnfantIncluses.FindAsync(1);

            string titreOriginal = activiteEnfantIncluse.Titre;

            activiteEnfantIncluse.Titre = "Le test";
            await _controller.PutActiviteEnfantIncluse(activiteEnfantIncluse.ActiviteEnfantIncluseId, activiteEnfantIncluse);
            ActiviteEnfantIncluse modifie = await _context.ActiviteEnfantIncluses.FindAsync(1);
            Assert.AreEqual(activiteEnfantIncluse, modifie, "ActiviteEnfantIncluse différents");


            //restauration des modification
            modifie.Titre = titreOriginal;
            await _controller.PutActiviteEnfantIncluse(modifie.ActiviteEnfantIncluseId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutActiviteEnfantIncluseTest__ReturnsNoContent_AvecMoq()
        {
            ActiviteEnfantIncluse activiteEnfantIncluse = new ActiviteEnfantIncluse()
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteEnfantIncluse);
            var activiteEnfantIncluseController = new ActiviteEnfantInclusesController(mockRepository.Object);


            activiteEnfantIncluse.Titre = "a";
            var res = activiteEnfantIncluseController.PutActiviteEnfantIncluse(activiteEnfantIncluse.ActiviteEnfantIncluseId, activiteEnfantIncluse);

            var actionResult = activiteEnfantIncluseController.GetActiviteEnfantIncluse(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ActiviteEnfantIncluse>), "Pas un ActionResult<ActiviteEnfantIncluse>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(ActiviteEnfantIncluse), "Pas un ActiviteEnfantIncluse");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            activiteEnfantIncluse.ActiviteEnfantIncluseId = ((ActiviteEnfantIncluse)result).ActiviteEnfantIncluseId;
            Assert.AreEqual(activiteEnfantIncluse, (ActiviteEnfantIncluse)result, "ActiviteEnfantIncluses pas identiques");
        }

        [TestMethod]
        public void PutActiviteEnfantIncluse_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            ActiviteEnfantIncluse activiteEnfantIncluse = new ActiviteEnfantIncluse()
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteEnfantIncluse);
            var activiteEnfantIncluseController = new ActiviteEnfantInclusesController(mockRepository.Object);


            var res = activiteEnfantIncluseController.PutActiviteEnfantIncluse(15, activiteEnfantIncluse);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutActiviteEnfantIncluse_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            ActiviteEnfantIncluse activiteEnfantIncluse = new ActiviteEnfantIncluse()
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };

            // Act
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(activiteEnfantIncluse);
            var activiteEnfantIncluseController = new ActiviteEnfantInclusesController(mockRepository.Object);


            var res = activiteEnfantIncluseController.PutActiviteEnfantIncluse(99999, activiteEnfantIncluse);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostActiviteEnfantIncluseTest()
        {
            ActiviteEnfantIncluse activiteEnfantIncluse = new ActiviteEnfantIncluse()
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };

            var res = await _controller.PostActiviteEnfantIncluse(activiteEnfantIncluse);
            ActiviteEnfantIncluse add = _context.ActiviteEnfantIncluses.Where(c => c.ActiviteEnfantIncluseId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de ActiviteEnfantIncluse ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteActiviteEnfantIncluse(add.ActiviteEnfantIncluseId);
        }

        [TestMethod()]
        public async Task PostActiviteEnfantIncluseTest__ReturnsCreateAtAction_AvecMoq()
        {

            ActiviteEnfantIncluse activiteEnfantIncluse = new ActiviteEnfantIncluse()
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            var activiteEnfantIncluseController = new ActiviteEnfantInclusesController(mockRepository.Object);
            // Act
            var actionResult = activiteEnfantIncluseController.PostActiviteEnfantIncluse(activiteEnfantIncluse).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ActiviteEnfantIncluse>), "Pas un ActionResult<ActiviteEnfantIncluse>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(ActiviteEnfantIncluse), "Pas un ActiviteEnfantIncluse");
            activiteEnfantIncluse.ActiviteEnfantIncluseId = ((ActiviteEnfantIncluse)result.Value).ActiviteEnfantIncluseId;
            Assert.AreEqual(activiteEnfantIncluse, (ActiviteEnfantIncluse)result.Value, "ActiviteEnfantIncluses pas identiques");
        }

        [TestMethod()]
        public async Task DeleteActiviteEnfantIncluseTest()
        {
            ActiviteEnfantIncluse activiteEnfantIncluse = new ActiviteEnfantIncluse()
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };
            EntityEntry<ActiviteEnfantIncluse> res = _context.ActiviteEnfantIncluses.Add(activiteEnfantIncluse);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteActiviteEnfantIncluse(res.Entity.ActiviteEnfantIncluseId);

            ActiviteEnfantIncluse domaine = _context.ActiviteEnfantIncluses.Where(u => u.ActiviteEnfantIncluseId == res.Entity.ActiviteEnfantIncluseId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteActiviteEnfantIncluseTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            ActiviteEnfantIncluse activiteEnfantIncluse = new ActiviteEnfantIncluse()
            {
                ActiviteEnfantIncluseId = 99999,
                Titre = "Nouvelle ActiviteEnfantIncluse",
                Duree = 0,
                Description = "Nouvelle ActiviteEnfantIncluse",
                Frequence = "Nouvelle ActiviteEnfantIncluse",
            };
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(activiteEnfantIncluse);
            var userController = new ActiviteEnfantInclusesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteActiviteEnfantIncluse(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteActiviteEnfantIncluseTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<ActiviteEnfantIncluse>>();
            var userController = new ActiviteEnfantInclusesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteActiviteEnfantIncluse(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
