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
    public class TypeClubsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly TypeClubsController _controller;
        private IDataRepository<TypeClub> _dataRepository;
        public TypeClubsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new TypeClubManager(_context);
            _controller = new TypeClubsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetTypeClubsTest()
        {
            ActionResult<IEnumerable<TypeClub>> typeClubs = await _controller.GetTypeClubs();
            var typeClubs1 = typeClubs.Value.ToList();
            typeClubs1 = typeClubs1.OrderBy(tc => tc.TypeClubId).ToList();

            var typeClubs2 = _context.TypeClubs.ToList();
            typeClubs2 = typeClubs2.OrderBy(tc => tc.TypeClubId).ToList();

            CollectionAssert.AreEqual(typeClubs1, typeClubs2, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetTypeClubs_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeClub typeClub1 = new TypeClub
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            TypeClub typeClub2 = new TypeClub
            {
                TypeClubId = 99998,
                Libelle = "Nouveau Libelle2"
            };


            List<TypeClub> lesTypeClubs = new List<TypeClub>();
            lesTypeClubs.Add(typeClub1);
            lesTypeClubs.Add(typeClub2);


            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesTypeClubs);
            var typeClubController = new TypeClubsController(mockRepository.Object);
            // Act
            var actionResult = typeClubController.GetTypeClubs().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesTypeClubs, actionResult.Value as List<TypeClub>, "TypeClub différent");
        }

        [TestMethod()]
        public async Task GetTypeClubTest()
        {
            ActionResult<TypeClub> typeClub = await _controller.GetTypeClub(1);
            Assert.AreEqual(_context.TypeClubs.Where(c => c.TypeClubId == 1).FirstOrDefault(), typeClub.Value, "TypeClub différent");
        }

        [TestMethod()]
        public void GetTypeClubById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeClub typeClub = new TypeClub
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeClub);
            var typeClubController = new TypeClubsController(mockRepository.Object);
            // Act
            var actionResult = typeClubController.GetTypeClub(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(typeClub, actionResult.Value as TypeClub);
        }

        [TestMethod]
        public void GetTypeClubById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            var typeClubController = new TypeClubsController(mockRepository.Object);
            // Act
            var actionResult = typeClubController.GetTypeClub(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetTypeClubByLibelleTest()
        {
            ActionResult<TypeClub> typeClub = await _controller.GetTypeClubByLibelle("En famille");
            TypeClub laTypeClub = _context.TypeClubs.Where(c => c.Libelle == "En famille").FirstOrDefault();
            Assert.IsNotNull(laTypeClub);
            Assert.AreEqual(laTypeClub, typeClub.Value, "TypeClub différent");
        }

        [TestMethod()]
        public void GetTypeClubByLibelle_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeClub typeClub = new TypeClub
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            mockRepository.Setup(x => x.GetByStringAsync("En famille").Result).Returns(typeClub);
            var typeClubController = new TypeClubsController(mockRepository.Object);
            // Act
            var actionResult = typeClubController.GetTypeClubByLibelle("En famille").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(typeClub, actionResult.Value as TypeClub, "TypeClub différent");
        }

        [TestMethod]
        public void GetTypeClubByLibelle_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            var typeClubController = new TypeClubsController(mockRepository.Object);
            // Act
            var actionResult = typeClubController.GetTypeClubByLibelle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutTypeClubTest()
        {
            TypeClub typeClub = await _context.TypeClubs.FindAsync(1);

            string libelleOriginal = typeClub.Libelle;

            typeClub.Libelle = "Le test";
            await _controller.PutTypeClub(typeClub.TypeClubId, typeClub);
            TypeClub modifie = await _context.TypeClubs.FindAsync(1);
            Assert.AreEqual(typeClub, modifie, "TypeClub différents");


            //restauration des modification
            modifie.Libelle = libelleOriginal;
            await _controller.PutTypeClub(modifie.TypeClubId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutTypeClubTest__ReturnsNoContent_AvecMoq()
        {
            TypeClub typeClub = new TypeClub()
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeClub);
            var typeClubController = new TypeClubsController(mockRepository.Object);


            typeClub.Libelle = "a";
            var res = typeClubController.PutTypeClub(typeClub.TypeClubId, typeClub);

            var actionResult = typeClubController.GetTypeClub(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeClub>), "Pas un ActionResult<TypeClub>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(TypeClub), "Pas un TypeClub");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            typeClub.TypeClubId = ((TypeClub)result).TypeClubId;
            Assert.AreEqual(typeClub, (TypeClub)result, "TypeClubs pas identiques");
        }

        [TestMethod]
        public void PutTypeClub_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            TypeClub typeClub = new TypeClub()
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeClub);
            var typeClubController = new TypeClubsController(mockRepository.Object);


            var res = typeClubController.PutTypeClub(15, typeClub);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutTypeClub_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            TypeClub typeClub = new TypeClub()
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(typeClub);
            var typeClubController = new TypeClubsController(mockRepository.Object);


            var res = typeClubController.PutTypeClub(99999, typeClub);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostTypeClubTest()
        {
            TypeClub typeClub = new TypeClub()
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            var res = await _controller.PostTypeClub(typeClub);
            TypeClub add = _context.TypeClubs.Where(c => c.TypeClubId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de TypeClub ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteTypeClub(add.TypeClubId);
        }

        [TestMethod()]
        public async Task PostTypeClubTest__ReturnsCreateAtAction_AvecMoq()
        {

            TypeClub typeClub = new TypeClub()
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            var typeClubController = new TypeClubsController(mockRepository.Object);
            // Act
            var actionResult = typeClubController.PostTypeClub(typeClub).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeClub>), "Pas un ActionResult<TypeClub>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(TypeClub), "Pas un TypeClub");
            typeClub.TypeClubId = ((TypeClub)result.Value).TypeClubId;
            Assert.AreEqual(typeClub, (TypeClub)result.Value, "TypeClubs pas identiques");
        }

        [TestMethod()]
        public async Task DeleteTypeClubTest()
        {
            TypeClub typeClub = new TypeClub()
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            EntityEntry<TypeClub> res = _context.TypeClubs.Add(typeClub);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteTypeClub(res.Entity.TypeClubId);

            TypeClub domaine = _context.TypeClubs.Where(u => u.TypeClubId == res.Entity.TypeClubId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteTypeClubTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            TypeClub typeClub = new TypeClub()
            {
                TypeClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(typeClub);
            var userController = new TypeClubsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTypeClub(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteTypeClubTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeClub>>();
            var userController = new TypeClubsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTypeClub(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
