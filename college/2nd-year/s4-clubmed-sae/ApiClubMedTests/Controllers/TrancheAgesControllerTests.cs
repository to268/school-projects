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
    public class TrancheAgesControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly TrancheAgesController _controller;
        private IDataRepository<TrancheAge> _dataRepository;
        public TrancheAgesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new TrancheAgeManager(_context);
            _controller = new TrancheAgesController(_dataRepository);

        }

        [TestMethod()]
        public async Task GetTrancheAgesTest()
        {
            ActionResult<IEnumerable<TrancheAge>> trancheAges = await _controller.GetTrancheAges();
            var trancheAges1 = trancheAges.Value.ToList();
            trancheAges1 = trancheAges1.OrderBy(ta => ta.TrancheAgeId).ToList();

            var trancheAges2 = _context.TrancheAges.ToList();
            trancheAges2 = trancheAges2.OrderBy(ta => ta.TrancheAgeId).ToList();

            CollectionAssert.AreEqual(trancheAges1, trancheAges2, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetTrancheAge_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TrancheAge trancheAge1 = new TrancheAge
            {
                TrancheAgeId = 99999,
                AgeMin = 18,
                AgeMax = 99,
            };

            TrancheAge trancheAge2 = new TrancheAge
            {
                TrancheAgeId = 99998,
                AgeMin = 24,
                AgeMax = 70,
            };


            List<TrancheAge> lesTrancheAges = new List<TrancheAge>();
            lesTrancheAges.Add(trancheAge1);
            lesTrancheAges.Add(trancheAge2);


            var mockRepository = new Mock<IDataRepository<TrancheAge>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesTrancheAges);
            var trancheAgeController = new TrancheAgesController(mockRepository.Object);
            // Act
            var actionResult = trancheAgeController.GetTrancheAges().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesTrancheAges, actionResult.Value as List<TrancheAge>, "TrancheAge différent");
        }

        [TestMethod()]
        public async Task GetTrancheAgeTest()
        {
            ActionResult<TrancheAge> trancheAge = await _controller.GetTrancheAge(1);
            Assert.AreEqual(_context.TrancheAges.Where(c => c.TrancheAgeId == 1).FirstOrDefault(), trancheAge.Value, "TrancheAge différent");
        }

        [TestMethod()]
        public void GetTrancheAgeById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TrancheAge trancheAge = new TrancheAge
            {
                TrancheAgeId = 99999,
                AgeMin = 18,
                AgeMax = 99,
            };
            var mockRepository = new Mock<IDataRepository<TrancheAge>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(trancheAge);
            var trancheAgeController = new TrancheAgesController(mockRepository.Object);
            // Act
            var actionResult = trancheAgeController.GetTrancheAge(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(trancheAge, actionResult.Value as TrancheAge);
        }

        [TestMethod]
        public void GetTrancheAgeById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TrancheAge>>();
            var vendeurController = new TrancheAgesController(mockRepository.Object);
            // Act
            var actionResult = vendeurController.GetTrancheAge(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        // FIXME: GetTrancheAgeByAgeMin is not correctly implemented
        // [TestMethod()]
        // public async Task GetTrancheAgeByAgeMin()
        // {
        //     ActionResult<TrancheAge> domainSkiable = await _controller.GetTrancheAgeByAgeMin("0");
        //     TrancheAge leTrancheAge = _context.TrancheAges.Where(c => c.AgeMin == 0).FirstOrDefault();
        //     Assert.AreEqual(leTrancheAge, domainSkiable.Value, "TrancheAge différent");
        // }

        // [TestMethod()]
        // public void GetTrancheAgeByAgeMin_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        // {
        //     // Arrange
        //     TrancheAge trancheAge = new TrancheAge
        //     {
        //         TrancheAgeId = 99999,
        //         AgeMin = 18,
        //         AgeMax = 99,
        //     };
        //     var mockRepository = new Mock<IDataRepository<TrancheAge>>();
        //     mockRepository.Setup(x => x.GetByStringAsync("Nouveau type activite").Result).Returns(trancheAge);
        //     var trancheAgeController = new TrancheAgesController(mockRepository.Object);
        //     // Act
        //     var actionResult = trancheAgeController.GetTrancheAgeByAgeMin("Nouveau type activite").Result;
        //     // Assert
        //     Assert.IsNotNull(actionResult, "Renvoie null");
        //     Assert.IsNotNull(actionResult.Value, "Revoie null");
        //     Assert.AreEqual(trancheAge, actionResult.Value as TrancheAge, "TrancheAge différent");
        // }

        // [TestMethod]
        // public void GetTrancheAgeByAgeMin_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        // {
        //     var mockRepository = new Mock<IDataRepository<TrancheAge>>();
        //     var vendeurController = new TrancheAgesController(mockRepository.Object);
        //     // Act
        //     var actionResult = vendeurController.GetTrancheAgeByAgeMin("").Result;
        //     // Assert
        //     Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        // }

        // FIXME: GetTrancheAgeByAgeMax is not correctly implemented
        // [TestMethod()]
        // public async Task GetTrancheAgeByAgeMax()
        // {
        //     ActionResult<TrancheAge> domainSkiable = await _controller.GetTrancheAgeByAgeMax("2");
        //     TrancheAge leTrancheAge = _context.TrancheAges.Where(c => c.AgeMax == 2).FirstOrDefault();
        //     Assert.AreEqual(leTrancheAge, domainSkiable.Value, "TrancheAge différent");
        // }

        // [TestMethod()]
        // public void GetTrancheAgeByAgeMax_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        // {
        //     // Arrange
        //     TrancheAge trancheAge = new TrancheAge
        //     {
        //         TrancheAgeId = 99999,
        //         AgeMin = 18,
        //         AgeMax = 99,
        //     };
        //     var mockRepository = new Mock<IDataRepository<TrancheAge>>();
        //     mockRepository.Setup(x => x.GetByStringAsync("Nouveau type activite").Result).Returns(trancheAge);
        //     var trancheAgeController = new TrancheAgesController(mockRepository.Object);
        //     // Act
        //     var actionResult = trancheAgeController.GetTrancheAgeByAgeMax("Nouveau type activite").Result;
        //     // Assert
        //     Assert.IsNotNull(actionResult, "Renvoie null");
        //     Assert.IsNotNull(actionResult.Value, "Revoie null");
        //     Assert.AreEqual(trancheAge, actionResult.Value as TrancheAge, "TrancheAge différent");
        // }

        // [TestMethod]
        // public void GetTrancheAgeByAgeMax_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        // {
        //     var mockRepository = new Mock<IDataRepository<TrancheAge>>();
        //     var vendeurController = new TrancheAgesController(mockRepository.Object);
        //     // Act
        //     var actionResult = vendeurController.GetTrancheAgeByAgeMax("").Result;
        //     // Assert
        //     Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        // }

        [TestMethod()]
        public async Task PutTrancheAgeTest()
        {
            TrancheAge trancheAge = await _context.TrancheAges.FindAsync(1);

            int ageMinOriginal = 0;

            trancheAge.AgeMin = 24;
            await _controller.PutTrancheAge(trancheAge.TrancheAgeId, trancheAge);
            TrancheAge modifie = await _context.TrancheAges.FindAsync(1);
            Assert.AreEqual(trancheAge, modifie, "TrancheAge différents");


            //restauration des modification
            modifie.AgeMin = ageMinOriginal;
            await _controller.PutTrancheAge(modifie.TrancheAgeId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutDomainSkiableTest__ReturnsNoContent_AvecMoq()
        {
            TrancheAge trancheAge = new TrancheAge
            {
                TrancheAgeId = 99999,
                AgeMin = 18,
                AgeMax = 99,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TrancheAge>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(trancheAge);
            var trancheAgeController = new TrancheAgesController(mockRepository.Object);


            trancheAge.AgeMin = 0;
            var res = trancheAgeController.PutTrancheAge(trancheAge.TrancheAgeId, trancheAge);

            var actionResult = trancheAgeController.GetTrancheAge(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TrancheAge>), "Pas un ActionResult<TrancheAge>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(TrancheAge), "Pas un TrancheAge");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            trancheAge.TrancheAgeId = ((TrancheAge)result).TrancheAgeId;
            Assert.AreEqual(trancheAge, (TrancheAge)result, "TrancheAge pas identiques");
        }

        [TestMethod]
        public void PutTrancheAge_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            TrancheAge trancheAge = new TrancheAge
            {
                TrancheAgeId = 99999,
                AgeMin = 18,
                AgeMax = 99,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TrancheAge>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(trancheAge);
            var trancheAgeController = new TrancheAgesController(mockRepository.Object);


            var res = trancheAgeController.PutTrancheAge(15, trancheAge);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutTrancheAge_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            TrancheAge trancheAge = new TrancheAge
            {
                TrancheAgeId = 99999,
                AgeMin = 18,
                AgeMax = 99,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<TrancheAge>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(trancheAge);
            var trancheAgeController = new TrancheAgesController(mockRepository.Object);


            var res = trancheAgeController.PutTrancheAge(99999, trancheAge);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostTrancheAgeTest()
        {
            TrancheAge trancheAge = new TrancheAge
            {
                TrancheAgeId = 99999,
                AgeMin = 18,
                AgeMax = 99,
            };

            var res = await _controller.PostTrancheAge(trancheAge);
            TrancheAge add = _context.TrancheAges.Where(c => c.TrancheAgeId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de TrancheAge ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteTrancheAge(add.TrancheAgeId);
        }

        [TestMethod()]
        public async Task PostUtilisateurTest__ReturnsCreateAtAction_AvecMoq()
        {

            TrancheAge trancheAge = new TrancheAge
            {
                TrancheAgeId = 99999,
                AgeMin = 18,
                AgeMax = 99,
            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<TrancheAge>>();
            var trancheAgeController = new TrancheAgesController(mockRepository.Object);
            // Act
            var actionResult = trancheAgeController.PostTrancheAge(trancheAge).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TrancheAge>), "Pas un ActionResult<DomainSkiable>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(TrancheAge), "Pas un TrancheAge");
            trancheAge.TrancheAgeId = ((TrancheAge)result.Value).TrancheAgeId;
            Assert.AreEqual(trancheAge, (TrancheAge)result.Value, "TrancheAge pas identiques");
        }

        [TestMethod()]
        public async Task DeleteTrancheAgeTest()
        {
            TrancheAge trancheAge = new TrancheAge
            {
                TrancheAgeId = 99999,
                AgeMin = 18,
                AgeMax = 99,
            };
            EntityEntry<TrancheAge> res = _context.TrancheAges.Add(trancheAge);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteTrancheAge(res.Entity.TrancheAgeId);

            TrancheAge domaine = _context.TrancheAges.Where(u => u.TrancheAgeId == res.Entity.TrancheAgeId).FirstOrDefault();

            Assert.IsNull(domaine, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteDomainSkiableTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            TrancheAge trancheAge = new TrancheAge
            {
                TrancheAgeId = 99999,
                AgeMin = 18,
                AgeMax = 99,
            };
            var mockRepository = new Mock<IDataRepository<TrancheAge>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(trancheAge);
            var userController = new TrancheAgesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTrancheAge(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteDomainSkiableTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TrancheAge>>();
            var userController = new TrancheAgesController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteTrancheAge(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
