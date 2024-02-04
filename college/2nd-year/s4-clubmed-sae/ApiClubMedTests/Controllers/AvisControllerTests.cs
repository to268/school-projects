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
    public class AvisControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly AvisController _controller;
        private IDataRepository<Avis> _dataRepository;
        public AvisControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new AvisManager(_context);
            _controller = new AvisController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetAvisTest()
        {
            ActionResult<IEnumerable<Avis>> aviss = await _controller.GetAvis();
            var avis1 = aviss.Value.ToList();
            avis1 = avis1.OrderBy(a => a.AvisId).ToList();

            var avis2 = _context.Avis.ToList();
            avis2 = avis2.OrderBy(a => a.AvisId).ToList();

            CollectionAssert.AreEqual(avis2, avis1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetAvis_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Avis avis1 = new Avis
            {
                AvisId = 99999,
                ResortId = 5,
                ClientId = 85,
                PhotoId = 2,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,

            };

            Avis avis2 = new Avis
            {
                AvisId = 99998,
                PhotoId = 2,
                ClientId = 85,
                ResortId = 348,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,
            };


            List<Avis> lesAvis = new List<Avis>();
            lesAvis.Add(avis1);
            lesAvis.Add(avis2);


            var mockRepository = new Mock<IDataRepository<Avis>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesAvis);
            var avisController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = avisController.GetAvis().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesAvis, actionResult.Value as List<Avis>, "Avis différent");
        }

        [TestMethod()]
        public async Task GetAllAvisTest()
        {
            ActionResult<Avis> avis = await _controller.GetAvis(1);
            Assert.AreEqual(_context.Avis.Where(c => c.AvisId == 1).FirstOrDefault(), avis.Value, "Avis différent");
        }

        [TestMethod()]
        public void GetAvisById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Avis avis = new Avis
            {
                AvisId = 99999,
                PhotoId = 2,
                ResortId = 5,
                ClientId = 85,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,



            };
            var mockRepository = new Mock<IDataRepository<Avis>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(avis);
            var avisController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = avisController.GetAvis(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(avis, actionResult.Value as Avis);
        }

        [TestMethod]
        public void GetAvisById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Avis>>();
            var avisController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = avisController.GetAvis(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }



        [TestMethod()]
        public async Task PutAvisTest()
        {
            Avis avis = await _context.Avis.FindAsync(1);

            string nomOriginal = avis.Commentaire;

            avis.Commentaire = "Le test";
            await _controller.PutAvis(avis.AvisId, avis);
            Avis modifie = await _context.Avis.FindAsync(1);
            Assert.AreEqual(avis, modifie, "Avis différents");


            //restauration des modification
            modifie.Commentaire = nomOriginal;
            await _controller.PutAvis(modifie.AvisId, modifie);
            Console.Write("d");
        }

        [TestMethod]
        public void PutAvisTest__ReturnsNoContent_AvecMoq()
        {
            Avis avis = new Avis()
            {
                AvisId = 99999,
                PhotoId = 2,
                ResortId = 5,
                ClientId = 85,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,



            };

            // Act
            var mockRepository = new Mock<IDataRepository<Avis>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(avis);
            var avisController = new AvisController(mockRepository.Object);


            avis.Commentaire = "a";
            var res = avisController.PutAvis(avis.AvisId, avis);

            var actionResult = avisController.GetAvis(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Avis>), "Pas un ActionResult<Avis>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Avis), "Pas un Avis");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            avis.AvisId = ((Avis)result).AvisId;
            Assert.AreEqual(avis, (Avis)result, "Avis pas identiques");
        }

        [TestMethod]
        public void PutAvis_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Avis avis = new Avis()
            {
                AvisId = 99999,
                PhotoId = 2,
                ResortId = 5,
                ClientId = 85,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,



            };

            // Act
            var mockRepository = new Mock<IDataRepository<Avis>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(avis);
            var avisController = new AvisController(mockRepository.Object);


            var res = avisController.PutAvis(15, avis);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutAvis_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Avis avis = new Avis()
            {
                AvisId = 99999,
                PhotoId = 2,
                ResortId = 5,
                ClientId = 85,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,



            };

            // Act
            var mockRepository = new Mock<IDataRepository<Avis>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(avis);
            var avisController = new AvisController(mockRepository.Object);


            var res = avisController.PutAvis(99999, avis);


            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostAvisTest()
        {
            Avis avis = new Avis()
            {
                AvisId = 99999,
                PhotoId = 2,
                ResortId = 5,
                ClientId = 85,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,



            };

            var res = await _controller.PostAvis(avis);
            Avis add = _context.Avis.Where(c => c.AvisId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Avis ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteAvis(add.AvisId);
        }

        [TestMethod()]
        public async Task PostAvisTest__ReturnsCreateAtAction_AvecMoq()
        {

            Avis avis = new Avis()
            {
                AvisId = 99999,
                PhotoId = 2,
                ResortId = 5,
                ClientId = 85,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,



            };
            // Arrange
            var mockRepository = new Mock<IDataRepository<Avis>>();
            var avisController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = avisController.PostAvis(avis).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Avis>), "Pas un ActionResult<Avis>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Avis), "Pas un Avis");
            avis.AvisId = ((Avis)result.Value).AvisId;
            Assert.AreEqual(avis, (Avis)result.Value, "Avis pas identiques");
        }

        [TestMethod()]
        public async Task DeleteAvisTest()
        {
            Avis avis = new Avis()
            {
                AvisId = 99999,
                PhotoId = 2,
                ResortId = 5,
                ClientId = 85,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,



            };
            EntityEntry<Avis> res = _context.Avis.Add(avis);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteAvis(res.Entity.AvisId);

            Avis avi = _context.Avis.Where(u => u.AvisId == res.Entity.AvisId).FirstOrDefault();

            Assert.IsNull(avi, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteAvisTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Avis avis = new Avis()
            {
                AvisId = 99999,
                PhotoId = 2,
                ResortId = 5,
                ClientId = 85,
                Note = 2,
                Commentaire = "Le nouveau Avis de test",
                Date = DateTime.Now,



            };
            var mockRepository = new Mock<IDataRepository<Avis>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(avis);
            var userController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteAvis(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteAvisTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Avis>>();
            var userController = new AvisController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteAvis(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
