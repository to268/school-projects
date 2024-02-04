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
    public class PhotosControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly PhotosController _controller;
        private IDataRepository<Photo> _dataRepository;
        public PhotosControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new PhotoManager(_context);
            _controller = new PhotosController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetPhotosTest()
        {
            ActionResult<IEnumerable<Photo>> photos = await _controller.GetPhotos();
            var photo1 = photos.Value.ToList();
            photo1 = photo1.OrderBy(ap => ap.PhotoId).ToList();

            var photo2 = _context.Photos.ToList();
            photo2 = photo2.OrderBy(ap => ap.PhotoId).ToList();

            CollectionAssert.AreEqual(photo2, photo1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetPhotos_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Photo photo1 = new Photo
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            Photo photo2 = new Photo
            {
                PhotoId = 99998,


                Lien = "Nouveau Photo2",

            };

            List<Photo> lesPhotos = new List<Photo>();
            lesPhotos.Add(photo1);
            lesPhotos.Add(photo2);


            var mockRepository = new Mock<IDataRepository<Photo>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesPhotos);
            var photoController = new PhotosController(mockRepository.Object);
            // Act
            var actionResult = photoController.GetPhotos().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesPhotos, actionResult.Value as List<Photo>, "Photo différent");
        }

        [TestMethod()]
        public async Task GetPhotoTest()
        {
            ActionResult<Photo> photo = await _controller.GetPhoto(1);
            Assert.AreEqual(_context.Photos.Where(c => c.PhotoId == 1).FirstOrDefault(), photo.Value, "Photo différent");
        }

        [TestMethod()]
        public void GetPhotoById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Photo photo = new Photo
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            var mockRepository = new Mock<IDataRepository<Photo>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(photo);
            var photoController = new PhotosController(mockRepository.Object);
            // Act
            var actionResult = photoController.GetPhoto(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(photo, actionResult.Value as Photo);
        }

        [TestMethod]
        public void GetPhotoById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Photo>>();
            var photoController = new PhotosController(mockRepository.Object);
            // Act
            var actionResult = photoController.GetPhoto(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetPhotoByLinkTest()
        {
            ActionResult<Photo> photo = await _controller.GetPhotoByLink("/img/station/La-Rosière/La-Rosière.jpg");
            Photo lePhoto = _context.Photos.Where(c => c.Lien == "/img/station/La-Rosière/La-Rosière.jpg").FirstOrDefault();
            Assert.IsNotNull(photo);
            Assert.AreEqual(lePhoto, photo.Value, "Photo différent");
        }

        [TestMethod()]
        public void GetPhotoByLink_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Photo photo = new Photo
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            var mockRepository = new Mock<IDataRepository<Photo>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau Photo").Result).Returns(photo);
            var photoController = new PhotosController(mockRepository.Object);
            // Act
            var actionResult = photoController.GetPhotoByLink("Nouveau Photo").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(photo, actionResult.Value as Photo, "Photo différent");
        }

        [TestMethod]
        public void GetPhotoByLink_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Photo>>();
            var photoController = new PhotosController(mockRepository.Object);
            // Act
            var actionResult = photoController.GetPhotoByLink("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutPhotoTest()
        {
            Photo photo = await _context.Photos.FindAsync(1);

            string nomOriginal = photo.Lien;

            photo.Lien = "Le test";
            await _controller.PutPhoto(photo.PhotoId, photo);
            Photo modifie = await _context.Photos.FindAsync(1);
            Assert.AreEqual(photo, modifie, "Photo différents");


            //restauration des modification
            modifie.Lien = nomOriginal;
            await _controller.PutPhoto(modifie.PhotoId, modifie);
        }

        [TestMethod]
        public void PutPhotoTest__ReturnsNoContent_AvecMoq()
        {
            Photo photo = new Photo()
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<Photo>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(photo);
            var photoController = new PhotosController(mockRepository.Object);


            photo.Lien = "a";
            var res = photoController.PutPhoto(photo.PhotoId, photo);

            var actionResult = photoController.GetPhoto(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Photo>), "Pas un ActionResult<Photo>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Photo), "Pas un Photo");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            photo.PhotoId = ((Photo)result).PhotoId;
            Assert.AreEqual(photo, (Photo)result, "Photos pas identiques");
        }

        [TestMethod]
        public void PutPhoto_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Photo photo = new Photo()
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<Photo>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(photo);
            var photoController = new PhotosController(mockRepository.Object);

            var res = photoController.PutPhoto(15, photo);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutPhoto_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Photo photo = new Photo()
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<Photo>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(photo);
            var photoController = new PhotosController(mockRepository.Object);

            var res = photoController.PutPhoto(99999, photo);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostPhotoTest()
        {
            Photo photo = new Photo()
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            var res = await _controller.PostPhoto(photo);
            Photo add = _context.Photos.Where(c => c.PhotoId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Photo ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeletePhoto(add.PhotoId);
        }

        [TestMethod()]
        public async Task PostPhotoTest__ReturnsCreateAtAction_AvecMoq()
        {

            Photo photo = new Photo()
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Photo>>();
            var photoController = new PhotosController(mockRepository.Object);
            // Act
            var actionResult = photoController.PostPhoto(photo).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Photo>), "Pas un ActionResult<Photo>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Photo), "Pas un Photo");
            photo.PhotoId = ((Photo)result.Value).PhotoId;
            Assert.AreEqual(photo, (Photo)result.Value, "Photos pas identiques");
        }

        [TestMethod()]
        public async Task DeletePhotoTest()
        {
            Photo photo = new Photo()
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            EntityEntry<Photo> res = _context.Photos.Add(photo);
            _context.SaveChanges();
            IActionResult result = await _controller.DeletePhoto(res.Entity.PhotoId);

            Photo photo1 = _context.Photos.Where(u => u.PhotoId == res.Entity.PhotoId).FirstOrDefault();

            Assert.IsNull(photo1, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeletePhotoTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Photo photo = new Photo()
            {
                PhotoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            var mockRepository = new Mock<IDataRepository<Photo>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(photo);
            var userController = new PhotosController(mockRepository.Object);
            // Act
            var actionResult = userController.DeletePhoto(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeletePhotoTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Photo>>();
            var userController = new PhotosController(mockRepository.Object);
            // Act
            var actionResult = userController.DeletePhoto(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
