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
    public class VideosControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly VideosController _controller;
        private IDataRepository<Video> _dataRepository;
        public VideosControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new VideoManager(_context);
            _controller = new VideosController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetVideosTest()
        {
            Video video1 = new Video
            {
                VideoId = 99999,
                Lien = "Nouveau Lien"
            };

            Video video2 = new Video
            {
                VideoId = 99998,
                Lien = "Nouveau Lien2"
            };

            _context.Add(video1);
            _context.Add(video2);

            _context.SaveChanges();

            ActionResult<IEnumerable<Video>> videos = await _controller.GetVideos();
            var videos1 = videos.Value.ToList();
            videos1 = videos1.OrderBy(ap => ap.VideoId).ToList();

            var videos2 = _context.Videos.ToList();
            videos2 = videos2.OrderBy(ap => ap.VideoId).ToList();


            CollectionAssert.AreEqual(videos1, videos2, "La liste renvoyée n'est pas la bonne.");


            _context.Remove(video1);
            _context.Remove(video2);

            _context.SaveChanges();
        }


        [TestMethod()]
        public void GetVideos_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Video video1 = new Video
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            Video video2 = new Video
            {
                VideoId = 99998,


                Lien = "Nouveau Video2",

            };

            List<Video> lesVideos = new List<Video>();
            lesVideos.Add(video1);
            lesVideos.Add(video2);


            var mockRepository = new Mock<IDataRepository<Video>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesVideos);
            var videoController = new VideosController(mockRepository.Object);
            // Act
            var actionResult = videoController.GetVideos().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesVideos, actionResult.Value as List<Video>, "Video différent");
        }

        [TestMethod()]
        public async Task GetVideoTest()
        {
            ActionResult<Video> video = await _controller.GetVideo(1);
            Assert.AreEqual(_context.Videos.Where(c => c.VideoId == 1).FirstOrDefault(), video.Value, "Video différent");
        }

        [TestMethod()]
        public void GetVideoById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Video video = new Video
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            var mockRepository = new Mock<IDataRepository<Video>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(video);
            var videoController = new VideosController(mockRepository.Object);
            // Act
            var actionResult = videoController.GetVideo(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(video, actionResult.Value as Video);
        }

        [TestMethod]
        public void GetVideoById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Video>>();
            var videoController = new VideosController(mockRepository.Object);
            // Act
            var actionResult = videoController.GetVideo(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetVideoByLinkTest()
        {
            ActionResult<Video> video = await _controller.GetVideoByLink("/img/station/La-Rosière/La-Rosière.jpg");
            Video leVideo = _context.Videos.Where(c => c.Lien == "/img/station/La-Rosière/La-Rosière.jpg").FirstOrDefault();
            Assert.IsNotNull(video);
            Assert.AreEqual(leVideo, video.Value, "Video différent");
        }

        [TestMethod()]
        public void GetVideoByLink_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Video video = new Video
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            var mockRepository = new Mock<IDataRepository<Video>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau Video").Result).Returns(video);
            var videoController = new VideosController(mockRepository.Object);
            // Act
            var actionResult = videoController.GetVideoByLink("Nouveau Video").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(video, actionResult.Value as Video, "Video différent");
        }

        [TestMethod]
        public void GetVideoByLink_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Video>>();
            var videoController = new VideosController(mockRepository.Object);
            // Act
            var actionResult = videoController.GetVideoByLink("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutVideoTest()
        {
            Video video1 = new Video
            {
                VideoId = 1,
                Lien = "Nouveau Lien"
            };


            _context.Add(video1);

            _context.SaveChanges();
            Video video = await _context.Videos.FindAsync(1);

            string nomOriginal = video.Lien;

            video.Lien = "Le test";
            await _controller.PutVideo(video.VideoId, video);
            Video modifie = await _context.Videos.FindAsync(1);
            Assert.AreEqual(video, modifie, "Video différents");


            //restauration des modification
            modifie.Lien = nomOriginal;
            await _controller.PutVideo(modifie.VideoId, modifie);

            _context.Remove(video1);

            _context.SaveChanges();
        }

        [TestMethod]
        public void PutVideoTest__ReturnsNoContent_AvecMoq()
        {
            Video video = new Video()
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<Video>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(video);
            var videoController = new VideosController(mockRepository.Object);


            video.Lien = "a";
            var res = videoController.PutVideo(video.VideoId, video);

            var actionResult = videoController.GetVideo(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Video>), "Pas un ActionResult<Video>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Video), "Pas un Video");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            video.VideoId = ((Video)result).VideoId;
            Assert.AreEqual(video, (Video)result, "Videos pas identiques");
        }

        [TestMethod]
        public void PutVideo_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Video video = new Video()
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<Video>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(video);
            var videoController = new VideosController(mockRepository.Object);

            var res = videoController.PutVideo(15, video);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutVideo_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Video video = new Video()
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            // Act
            var mockRepository = new Mock<IDataRepository<Video>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(video);
            var videoController = new VideosController(mockRepository.Object);

            var res = videoController.PutVideo(99999, video);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostVideoTest()
        {
            Video video = new Video()
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            var res = await _controller.PostVideo(video);
            Video add = _context.Videos.Where(c => c.VideoId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Video ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteVideo(add.VideoId);
        }

        [TestMethod()]
        public async Task PostVideoTest__ReturnsCreateAtAction_AvecMoq()
        {

            Video video = new Video()
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Video>>();
            var videoController = new VideosController(mockRepository.Object);
            // Act
            var actionResult = videoController.PostVideo(video).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Video>), "Pas un ActionResult<Video>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Video), "Pas un Video");
            video.VideoId = ((Video)result.Value).VideoId;
            Assert.AreEqual(video, (Video)result.Value, "Videos pas identiques");
        }

        [TestMethod()]
        public async Task DeleteVideoTest()
        {
            Video video = new Video()
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            EntityEntry<Video> res = _context.Videos.Add(video);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteVideo(res.Entity.VideoId);

            Video video1 = _context.Videos.Where(u => u.VideoId == res.Entity.VideoId).FirstOrDefault();

            Assert.IsNull(video1, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteVideoTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Video video = new Video()
            {
                VideoId = 99999,


                Lien = "/img/station/La-Rosière/La-Rosière.jpg",

            };

            var mockRepository = new Mock<IDataRepository<Video>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(video);
            var userController = new VideosController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteVideo(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteVideoTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Video>>();
            var userController = new VideosController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteVideo(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
