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
    public class RegroupementClubsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly RegroupementClubsController _controller;
        private IDataRepository<RegroupementClub> _dataRepository;
        public RegroupementClubsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new RegroupementClubManager(_context);
            _controller = new RegroupementClubsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetRegroupementClubsTest()
        {
            RegroupementClub regroupementClub1 = new RegroupementClub
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            RegroupementClub regroupementClub2 = new RegroupementClub
            {
                RegroupementClubId = 99998,
                Libelle = "Nouveau Libelle2"
            };

            _context.Add(regroupementClub1);
            _context.Add(regroupementClub2);

            _context.SaveChanges();

            ActionResult<IEnumerable<RegroupementClub>> regroupementClubs = await _controller.GetRegroupementClubs();
            var regroupementClubs1 = regroupementClubs.Value.ToList();
            regroupementClubs1.Sort();

            var regroupementClubs2 = _context.RegroupementClubs.ToList();
            regroupementClubs2.Sort();


            CollectionAssert.AreEqual(regroupementClubs2, regroupementClubs1, "La liste renvoyée n'est pas la bonne.");


            _context.Remove(regroupementClub1);
            _context.Remove(regroupementClub2);

            _context.SaveChanges();

            //await _controller.DeleteRegroupementClub(regroupementClub1.RegroupementClubId);
            //await _controller.DeleteRegroupementClub(regroupementClub2.RegroupementClubId);
        }


        [TestMethod()]
        public void GetRegroupementClubs_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            RegroupementClub regroupementClub1 = new RegroupementClub
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            RegroupementClub regroupementClub2 = new RegroupementClub
            {
                RegroupementClubId = 99998,
                Libelle = "Nouveau Libelle2"
            };


            List<RegroupementClub> lesRegroupementClubs = new List<RegroupementClub>();
            lesRegroupementClubs.Add(regroupementClub1);
            lesRegroupementClubs.Add(regroupementClub2);


            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesRegroupementClubs);
            var regroupementClubController = new RegroupementClubsController(mockRepository.Object);
            // Act
            var actionResult = regroupementClubController.GetRegroupementClubs().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesRegroupementClubs, actionResult.Value as List<RegroupementClub>, "RegroupementClub différent");
        }

        [TestMethod()]
        public async Task GetRegroupementClubTest()
        {
            ActionResult<RegroupementClub> regroupementClub = await _controller.GetRegroupementClub(1);
            Assert.AreEqual(_context.RegroupementClubs.Where(c => c.RegroupementClubId == 1).FirstOrDefault(), regroupementClub.Value, "RegroupementClub différent");
        }

        [TestMethod()]
        public void GetRegroupementClubById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            RegroupementClub regroupementClub = new RegroupementClub
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(regroupementClub);
            var regroupementClubController = new RegroupementClubsController(mockRepository.Object);
            // Act
            var actionResult = regroupementClubController.GetRegroupementClub(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(regroupementClub, actionResult.Value as RegroupementClub);
        }

        [TestMethod]
        public void GetRegroupementClubById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            var regroupementClubController = new RegroupementClubsController(mockRepository.Object);
            // Act
            var actionResult = regroupementClubController.GetRegroupementClub(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetRegroupementClubByLibelleTest()
        {
            RegroupementClub regroupementClub1 = new RegroupementClub
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau RegroupementClub"
            };


            _context.Add(regroupementClub1);
            _context.SaveChanges();

            ActionResult<RegroupementClub> regroupementClub = await _controller.GetRegroupementClubByLibelle("Nouveau RegroupementClub");
            RegroupementClub laRegroupementClub = _context.RegroupementClubs.Where(c => c.Libelle == "Nouveau RegroupementClub").FirstOrDefault();
            Assert.IsNotNull(laRegroupementClub);
            Assert.AreEqual(laRegroupementClub, regroupementClub.Value, "RegroupementClub différent");


            _context.Remove(regroupementClub1);
            _context.SaveChanges();
            //await _controller.DeleteRegroupementClub(regroupementClub1.RegroupementClubId);
        }

        [TestMethod()]
        public void GetRegroupementClubByLibelle_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            RegroupementClub regroupementClub = new RegroupementClub
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau RegroupementClub").Result).Returns(regroupementClub);
            var regroupementClubController = new RegroupementClubsController(mockRepository.Object);
            // Act
            var actionResult = regroupementClubController.GetRegroupementClubByLibelle("Nouveau RegroupementClub").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(regroupementClub, actionResult.Value as RegroupementClub, "RegroupementClub différent");
        }

        [TestMethod]
        public void GetRegroupementClubByLibelle_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            var regroupementClubController = new RegroupementClubsController(mockRepository.Object);
            // Act
            var actionResult = regroupementClubController.GetRegroupementClubByLibelle("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutRegroupementClubTest()
        {
            RegroupementClub regroupement = new RegroupementClub
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            await _controller.PostRegroupementClub(regroupement);

            RegroupementClub regroupementClub = await _context.RegroupementClubs.FindAsync(99999);

            string libelleOriginal = regroupementClub.Libelle;

            regroupementClub.Libelle = "Le test";
            await _controller.PutRegroupementClub(regroupementClub.RegroupementClubId, regroupementClub);
            RegroupementClub modifie = await _context.RegroupementClubs.FindAsync(99999);
            Assert.AreEqual(regroupementClub, modifie, "RegroupementClub différents");


            //restauration des modification
            modifie.Libelle = libelleOriginal;
            await _controller.PutRegroupementClub(modifie.RegroupementClubId, modifie);

            await _controller.DeleteRegroupementClub(regroupement.RegroupementClubId);
        }

        [TestMethod]
        public void PutRegroupementClubTest__ReturnsNoContent_AvecMoq()
        {
            RegroupementClub regroupementClub = new RegroupementClub()
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(regroupementClub);
            var regroupementClubController = new RegroupementClubsController(mockRepository.Object);


            regroupementClub.Libelle = "a";
            var res = regroupementClubController.PutRegroupementClub(regroupementClub.RegroupementClubId, regroupementClub);

            var actionResult = regroupementClubController.GetRegroupementClub(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<RegroupementClub>), "Pas un ActionResult<RegroupementClub>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(RegroupementClub), "Pas un RegroupementClub");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            regroupementClub.RegroupementClubId = ((RegroupementClub)result).RegroupementClubId;
            Assert.AreEqual(regroupementClub, (RegroupementClub)result, "RegroupementClubs pas identiques");
        }

        [TestMethod]
        public void PutRegroupementClub_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            RegroupementClub regroupementClub = new RegroupementClub()
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(regroupementClub);
            var regroupementClubController = new RegroupementClubsController(mockRepository.Object);


            var res = regroupementClubController.PutRegroupementClub(15, regroupementClub);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutRegroupementClub_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            RegroupementClub regroupementClub = new RegroupementClub()
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            // Act
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(regroupementClub);
            var regroupementClubController = new RegroupementClubsController(mockRepository.Object);


            var res = regroupementClubController.PutRegroupementClub(99999, regroupementClub);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostRegroupementClubTest()
        {
            RegroupementClub regroupementClub = new RegroupementClub()
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };

            var res = await _controller.PostRegroupementClub(regroupementClub);
            RegroupementClub add = _context.RegroupementClubs.Where(c => c.RegroupementClubId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de RegroupementClub ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteRegroupementClub(add.RegroupementClubId);
        }

        [TestMethod()]
        public async Task PostRegroupementClubTest__ReturnsCreateAtAction_AvecMoq()
        {

            RegroupementClub regroupementClub = new RegroupementClub()
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            var regroupementClubController = new RegroupementClubsController(mockRepository.Object);
            // Act
            var actionResult = regroupementClubController.PostRegroupementClub(regroupementClub).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<RegroupementClub>), "Pas un ActionResult<RegroupementClub>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(RegroupementClub), "Pas un RegroupementClub");
            regroupementClub.RegroupementClubId = ((RegroupementClub)result.Value).RegroupementClubId;
            Assert.AreEqual(regroupementClub, (RegroupementClub)result.Value, "RegroupementClubs pas identiques");
        }

        [TestMethod()]
        public async Task DeleteRegroupementClubTest()
        {
            RegroupementClub regroupementClub = new RegroupementClub()
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            EntityEntry<RegroupementClub> res = _context.RegroupementClubs.Add(regroupementClub);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteRegroupementClub(res.Entity.RegroupementClubId);

            RegroupementClub domaine = _context.RegroupementClubs.Where(u => u.RegroupementClubId == res.Entity.RegroupementClubId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteRegroupementClubTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            RegroupementClub regroupementClub = new RegroupementClub()
            {
                RegroupementClubId = 99999,
                Libelle = "Nouveau Libelle"
            };
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(regroupementClub);
            var userController = new RegroupementClubsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteRegroupementClub(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteRegroupementClubTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<RegroupementClub>>();
            var userController = new RegroupementClubsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteRegroupementClub(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}