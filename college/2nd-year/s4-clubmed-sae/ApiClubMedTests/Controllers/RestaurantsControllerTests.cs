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
    public class RestaurantsControllerTests
    {
        private readonly ClubMedDBContexts _context;
        private readonly RestaurantsController _controller;
        private IDataRepository<Restaurant> _dataRepository;
        public RestaurantsControllerTests()
        {
            var builder = new DbContextOptionsBuilder<ClubMedDBContexts>().UseNpgsql("Server=saeclubmed.postgres.database.azure.com;port=5432;Database=clubmed;uid=clubmed;password=z9)Seb{62WU,;SearchPath=clubmed;");
            _context = new ClubMedDBContexts(builder.Options);
            _dataRepository = new RestaurantManager(_context);
            _controller = new RestaurantsController(_dataRepository);
        }

        [TestMethod()]
        public async Task GetRestaurantsTest()
        {
            ActionResult<IEnumerable<Restaurant>> restaurants = await _controller.GetRestaurants();
            var restaurant1 = restaurants.Value.ToList();
            restaurant1 = restaurant1.OrderBy(ap => ap.RestaurantId).ToList();

            var restaurant2 = _context.Restaurants.ToList();
            restaurant2 = restaurant2.OrderBy(ap => ap.RestaurantId).ToList();

            CollectionAssert.AreEqual(restaurant2, restaurant1, "La liste renvoyée n'est pas la bonne.");
        }


        [TestMethod()]
        public void GetRestaurants_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Restaurant restaurant1 = new Restaurant
            {
                RestaurantId = 99999,
                PhotoId = 1,
                Description = "restaurant 5*,...",
                Nom = "Nouveau Restaurant",
                ResortId = 2,
            };

            Restaurant restaurant2 = new Restaurant
            {
                RestaurantId = 99998,
                PhotoId = 2,
                Description = "restaurant 5*,...2",
                Nom = "Nouveau Restaurant2",
                ResortId = 2,
            };

            List<Restaurant> lesRestaurants = new List<Restaurant>();
            lesRestaurants.Add(restaurant1);
            lesRestaurants.Add(restaurant2);


            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            mockRepository.Setup(x => x.GetAllAsync().Result).Returns(lesRestaurants);
            var restaurantController = new RestaurantsController(mockRepository.Object);
            // Act
            var actionResult = restaurantController.GetRestaurants().Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            CollectionAssert.AreEqual(lesRestaurants, actionResult.Value as List<Restaurant>, "Restaurant différent");
        }

        [TestMethod()]
        public async Task GetRestaurantTest()
        {
            ActionResult<Restaurant> restaurant = await _controller.GetRestaurant(1);
            Assert.AreEqual(_context.Restaurants.Where(c => c.RestaurantId == 1).FirstOrDefault(), restaurant.Value, "Restaurant différent");
        }

        [TestMethod()]
        public void GetRestaurantById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Restaurant restaurant = new Restaurant
            {
                RestaurantId = 99999,
                PhotoId = 2,
                Description = "restaurant 5*,...",
                Nom = "Nouveau Restaurant",
                ResortId = 2,
            };

            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(restaurant);
            var restaurantController = new RestaurantsController(mockRepository.Object);
            // Act
            var actionResult = restaurantController.GetRestaurant(99999).Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Renvoie null");
            Assert.AreEqual(restaurant, actionResult.Value as Restaurant);
        }

        [TestMethod]
        public void GetRestaurantById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            var restaurantController = new RestaurantsController(mockRepository.Object);
            // Act
            var actionResult = restaurantController.GetRestaurant(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }

        [TestMethod()]
        public async Task GetRestaurantByNameTest()
        {
            ActionResult<Restaurant> restaurant = await _controller.GetRestaurantsByName("Le provence");
            Restaurant leRestaurant = _context.Restaurants.Where(c => c.Nom == "Le provence").FirstOrDefault();
            Assert.IsNotNull(restaurant);
            Assert.AreEqual(leRestaurant, restaurant.Value, "Restaurant différent");
        }

        [TestMethod()]
        public void GetRestaurantByName_ExistingNamePassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Restaurant restaurant = new Restaurant
            {
                RestaurantId = 99999,
                PhotoId = 2,
                Nom = "Nouveau Restaurant",
                Description = "restaurant 5*,...",
                ResortId = 2,
            };

            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            mockRepository.Setup(x => x.GetByStringAsync("Nouveau Restaurant").Result).Returns(restaurant);
            var restaurantController = new RestaurantsController(mockRepository.Object);
            // Act
            var actionResult = restaurantController.GetRestaurantsByName("Nouveau Restaurant").Result;
            // Assert
            Assert.IsNotNull(actionResult, "Renvoie null");
            Assert.IsNotNull(actionResult.Value, "Revoie null");
            Assert.AreEqual(restaurant, actionResult.Value as Restaurant, "Restaurant différent");
        }

        [TestMethod]
        public void GetRestaurantByName_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            var restaurantController = new RestaurantsController(mockRepository.Object);
            // Act
            var actionResult = restaurantController.GetRestaurantsByName("").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Ne revoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PutRestaurantTest()
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(2);

            string nomOriginal = restaurant.Nom;

            restaurant.Nom = "Le test";
            await _controller.PutRestaurant(restaurant.RestaurantId, restaurant);
            Restaurant modifie = await _context.Restaurants.FindAsync(2);
            Assert.AreEqual(restaurant, modifie, "Restaurant différents");


            //restauration des modification
            modifie.Nom = nomOriginal;
            await _controller.PutRestaurant(modifie.RestaurantId, modifie);
        }

        [TestMethod]
        public void PutRestaurantTest__ReturnsNoContent_AvecMoq()
        {
            Restaurant restaurant = new Restaurant()
            {
                RestaurantId = 99999,
                PhotoId = 2,
                Description = "restaurant 5*,...",
                Nom = "Nouveau Restaurant",
                ResortId = 2,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(restaurant);
            var restaurantController = new RestaurantsController(mockRepository.Object);


            restaurant.Nom = "a";
            var res = restaurantController.PutRestaurant(restaurant.RestaurantId, restaurant);

            var actionResult = restaurantController.GetRestaurant(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Restaurant>), "Pas un ActionResult<Restaurant>");
            var result = actionResult.Value;
            Assert.IsInstanceOfType(result, typeof(Restaurant), "Pas un Restaurant");
            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
            restaurant.RestaurantId = ((Restaurant)result).RestaurantId;
            Assert.AreEqual(restaurant, (Restaurant)result, "Restaurants pas identiques");
        }

        [TestMethod]
        public void PutRestaurant_UnvalidIdPassed_BadRequestResult_AvecMoq()
        {
            Restaurant restaurant = new Restaurant()
            {
                RestaurantId = 99999,
                PhotoId = 2,
                Description = "restaurant 5*,...",
                Nom = "Nouveau Restaurant",
                ResortId = 2,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(restaurant);
            var restaurantController = new RestaurantsController(mockRepository.Object);

            var res = restaurantController.PutRestaurant(15, restaurant);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult), "Ne revoie pas BadRequestResult");
        }

        [TestMethod]
        public void PutRestaurant_UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            Restaurant restaurant = new Restaurant()
            {
                RestaurantId = 99999,
                PhotoId = 2,
                Description = "restaurant 5*,...",
                Nom = "Nouveau Restaurant",
                ResortId = 2,
            };

            // Act
            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(restaurant);
            var restaurantController = new RestaurantsController(mockRepository.Object);

            var res = restaurantController.PutRestaurant(99999, restaurant);

            // Assert
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Ne renvoie pas NotFoundResult");
        }


        [TestMethod()]
        public async Task PostRestaurantTest()
        {
            Restaurant restaurant = new Restaurant()
            {
                RestaurantId = 99999,
                PhotoId = 2,
                Description = "restaurant 5*,...",
                Nom = "Nouveau Restaurant",
                ResortId = 2,
            };

            var res = await _controller.PostRestaurant(restaurant);
            Restaurant add = _context.Restaurants.Where(c => c.RestaurantId == 99999).FirstOrDefault();
            Assert.IsNotNull(add, "Pas de Restaurant ajouté");
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Ne revoie pas CreatedAtActionResult");

            await _controller.DeleteRestaurant(add.RestaurantId);
        }

        [TestMethod()]
        public async Task PostRestaurantTest__ReturnsCreateAtAction_AvecMoq()
        {

            Restaurant restaurant = new Restaurant()
            {
                RestaurantId = 99999,
                PhotoId = 2,
                Description = "restaurant 5*,...",
                Nom = "Nouveau Restaurant",
                ResortId = 2,
            };

            // Arrange
            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            var restaurantController = new RestaurantsController(mockRepository.Object);
            // Act
            var actionResult = restaurantController.PostRestaurant(restaurant).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Restaurant>), "Pas un ActionResult<Restaurant>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Restaurant), "Pas un Restaurant");
            restaurant.RestaurantId = ((Restaurant)result.Value).RestaurantId;
            Assert.AreEqual(restaurant, (Restaurant)result.Value, "Restaurants pas identiques");
        }

        [TestMethod()]
        public async Task DeleteRestaurantTest()
        {
            Restaurant restaurant = new Restaurant()
            {
                RestaurantId = 99999,
                PhotoId = 2,
                Description = "restaurant 5*,...",
                Nom = "Nouveau Restaurant",
                ResortId = 2,
            };

            EntityEntry<Restaurant> res = _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            IActionResult result = await _controller.DeleteRestaurant(res.Entity.RestaurantId);

            Restaurant restaurant1 = _context.Restaurants.Where(u => u.RestaurantId == res.Entity.RestaurantId).FirstOrDefault();

            Assert.IsNull(restaurant1, "Pas null donc pas delete");
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Ne revoie pas NoContentResult");
        }

        [TestMethod]
        public void DeleteRestaurantTest_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            Restaurant restaurant = new Restaurant()
            {
                RestaurantId = 99999,
                PhotoId = 2,
                Description = "restaurant 5*,...",
                Nom = "Nouveau Restaurant",
                ResortId = 2,
            };

            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            mockRepository.Setup(x => x.GetByIdAsync(99999).Result).Returns(restaurant);
            var userController = new RestaurantsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteRestaurant(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult donc pas de supression");

        }

        [TestMethod]
        public void DeleteRestaurantTest__UnknowIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Restaurant>>();
            var userController = new RestaurantsController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteRestaurant(99999).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult donc suppression n'ayant pas échoué");
        }
    }
}
