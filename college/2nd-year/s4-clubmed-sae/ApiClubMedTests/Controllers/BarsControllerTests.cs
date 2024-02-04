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
    public class BarsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly BarsController _controller;
        private IDataRepository<Bar> _dataRepository;
        public BarsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new BarManager(_context);
            _controller = new BarsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetBarsTest()
        {
            ActionResult<IEnumerable<Bar>> bars = await _controller.GetBars();
            var bar1 = bars.Value.ToList();
            bar1 = bar1.OrderBy(ap => ap.BarId).ToList();

            var bar2 = _context.Bars.ToList();
            bar2 = bar2.OrderBy(ap => ap.BarId).ToList();

            CollectionAssert.AreEqual(bar2, bar1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetBars_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Bar bar1 = new Bar
            {
                BarId = 99999,
                PhotoId = 1,
                Description = "bar 5*,...",
                Nom = "Nouveau Bar",
                ResortId = 2,
            };

            Bar bar2 = new Bar
            {
                BarId = 99998,
                PhotoId = 2,
                Description = "bar 5*,...2",
                Nom = "Nouveau Bar2",
                ResortId = 2,
            };

            List<Bar> lesBars = new List<Bar>();
            lesBars.Add(bar1);
            lesBars.Add(bar2);


            var mockRepository = new Mock<IDataRepository<Bar>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesBars);
            var barController = new BarsController(mockRepository.Object);
            // Act
            var actionResult = barController.GetBars().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesBars, actionResult.Value as List<Bar>, "Bar différent");
        }

        [TestMethod()]
        public async Task GetBarTest()
        {
            ActionResult<Bar> bar = await _controller.GetBar(1);
            Assert.AreEqual(_context.Bars.Where(c => c.BarId == 1).FirstOrDefault(), bar.Value, "Bar différent");
        }

        [TestMethod()]
        public void GetBarById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Bar bar = new Bar
            {
                BarId = 99999,
                PhotoId = 2,
                Description = "bar 5*,...",
                Nom = "Nouveau Bar",
                ResortId = 2,
            };

            var mockRepository = new Mock<IDataRepository<Bar>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(bar);
            var barController = new BarsController(mockRepository.Object);
            // Act
            var actionResult = barController.GetBar(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(bar, actionResult.Value as Bar);
        }

        [TestMethod]
        public void GetBarById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Bar>>();
            var barController = new BarsController(mockRepository.Object);
            // Act
            var actionResult = barController.GetBar(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetBarByNameTest()
        {
            ActionResult<Bar> bar = await _controller.GetBarByName("La Croisette");
            Bar leBar = _context.Bars.Where(c => c.Nom == "La Croisette").FirstOrDefault();
            Assert.IsNotNull(bar);
            Assert.AreEqual(leBar, bar.Value, "Bar différent");
        }

        [TestMethod()]
        public void GetBarByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Bar bar = new Bar
            {
                BarId = 99999,
                PhotoId = 2,
                Nom = "Nouveau Bar",
                Description = "bar 5*,...",
                ResortId = 2,
            };

            var mockRepository = new Mock<IDataRepository<Bar>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau Bar").Result).Returns(bar);
            var barController = new BarsController(mockRepository.Object);
            // Act
            var actionResult = barController.GetBarByName("Nouveau Bar").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(bar, actionResult.Value as Bar, "Bar différent");
        }

        [TestMethod]
        public void GetBarByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Bar>>();
            var barController = new BarsController(mockRepository.Object);
            // Act
            var actionResult = barController.GetBarByName("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutBarTest()
        {
            Bar bar = await _context.Bars.FindAsync(2);

            string nomOriginal = bar.Nom;

            bar.Nom = "Le test";
            await _controller.PutBar(bar.BarId, bar);
            Bar modifie = await _context.Bars.FindAsync(2);
            Assert.AreEqual(bar, modifie, "Bar différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutBar(modifie.BarId, modifie);
        }

        [TestMethod]
        public void PutBarTest__ReturnsNoContent_AvecMoq()
        {
            Bar bar = new Bar()
            {
                BarId = 99999,
                PhotoId = 2,
                Description = "bar 5*,...",
                Nom = "Nouveau Bar",
                ResortId = 2,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Bar>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(bar);
            var barController = new BarsController(mockRepository.Object);


            bar.Nom = "a";
            var res = barController.PutBar(bar.BarId, bar);

            var actionResult = barController.GetBar(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Bar>), "Pas un ActionResult<Bar>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Bar), "Pas un Bar");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            bar.BarId = ((Bar)result).BarId;
            Assert.AreEqual(bar, (Bar)result, "Bars pas identiques");
        }

        [TestMethod]
        public void PutBar_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Bar bar = new Bar()
            {
                BarId = 99999,
                PhotoId = 2,
                Description = "bar 5*,...",
                Nom = "Nouveau Bar",
                ResortId = 2,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Bar>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(bar);
            var barController = new BarsController(mockRepository.Object);

            var res = barController.PutBar(15, bar);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutBar_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Bar bar = new Bar()
            {
                BarId = 99999,
                PhotoId = 2,
                Description = "bar 5*,...",
                Nom = "Nouveau Bar",
                ResortId = 2,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Bar>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(bar);
            var barController = new BarsController(mockRepository.Object);

            var res = barController.PutBar(99999, bar);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostBarTest()
        {
            Bar bar = new Bar()
            {
                BarId = 99999,
                PhotoId = 2,
                Description = "bar 5*,...",
                Nom = "Nouveau Bar",
                ResortId = 2,
            };

            var res = await _controller.PostBar(bar);
            Bar add = _context.Bars.Where(c => c.BarId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Bar ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteBar(add.BarId);
        }

        [TestMethod()]
        public async Task PostBarTest__ReturnsCreateAtAction_AvecMoq()
        {

            Bar bar = new Bar()
            {
                BarId = 99999,
                PhotoId = 2,
                Description = "bar 5*,...",
                Nom = "Nouveau Bar",
                ResortId = 2,
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Bar>>();
            var barController = new BarsController(mockRepository.Object);
            // Act
            var actionResult = barController.PostBar(bar).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Bar>), "Pas un ActionResult<Bar>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Bar), "Pas un Bar");
            bar.BarId = ((Bar)result.Value).BarId;
            Assert.AreEqual(bar, (Bar)result.Value, "Bars pas identiques");
        }

        [TestMethod()]
        public async Task DeleteBarTest()
        {
            Bar bar = new Bar()
            {
                BarId = 99999,
                PhotoId = 2,
                Description = "bar 5*,...",
                Nom = "Nouveau Bar",
                ResortId = 2,
            };

            EntityEntry<Bar> res = _context.Bars.Add(bar);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteBar(res.Entity.BarId);

            Bar bar1 = _context.Bars.Where(u => u.BarId == res.Entity.BarId).FirstOrDefault();

            Assert.IsNull(bar1, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteBarTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Bar bar = new Bar()
            {
                BarId = 99999,
                PhotoId = 2,
                Description = "bar 5*,...",
                Nom = "Nouveau Bar",
                ResortId = 2,
            };

            var mockRepository = new Mock<IDataRepository<Bar>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(bar);
            var userController = new BarsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteBar(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteBarTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Bar>>();
            var userController = new BarsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteBar(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
