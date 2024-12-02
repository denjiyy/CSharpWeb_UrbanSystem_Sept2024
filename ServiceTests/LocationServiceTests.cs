using Moq;
using MockQueryable.Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.SuggestionsLocations;
using Microsoft.EntityFrameworkCore;

namespace ServiceTests
{
    [TestFixture]
    public class LocationServiceTests
    {
        private Mock<IRepository<Location, Guid>> _mockLocationRepository;
        private LocationService _locationService;

        [SetUp]
        public void Setup()
        {
            _mockLocationRepository = new Mock<IRepository<Location, Guid>>();
            _locationService = new LocationService(_mockLocationRepository.Object);
        }

        [Test]
        public async Task AddLocationAsync_CallsRepositoryWithCorrectLocation()
        {
            // Arrange
            var model = new LocationFormViewModel
            {
                CityName = "New York",
                StreetName = "5th Avenue",
                CityPicture = "image.jpg"
            };

            Location? savedLocation = null;

            _mockLocationRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Location>()))
                .Callback<Location>(loc => savedLocation = loc)
                .Returns(Task.CompletedTask);

            // Act
            await _locationService.AddLocationAsync(model);

            // Assert
            _mockLocationRepository.Verify(repo => repo.AddAsync(It.IsAny<Location>()), Times.Once);
            Assert.IsNotNull(savedLocation);
            Assert.AreEqual(model.CityName, savedLocation.CityName);
            Assert.AreEqual(model.StreetName, savedLocation.StreetName);
            Assert.AreEqual(model.CityPicture, savedLocation.CityPicture);
        }

        [Test]
        public async Task GetAllOrderedByNameAsync_ReturnsCorrectlyOrderedLocations()
        {
            // Arrange
            var testLocations = new List<Location>
            {
                new Location { Id = Guid.NewGuid(), CityName = "Chicago", StreetName = "Main Street", CityPicture = "chicago.jpg" },
                new Location { Id = Guid.NewGuid(), CityName = "New York", StreetName = "5th Avenue", CityPicture = "newyork.jpg" },
                new Location { Id = Guid.NewGuid(), CityName = "Los Angeles", StreetName = "Sunset Blvd", CityPicture = "la.jpg" }
            };

            _mockLocationRepository.Setup(repo => repo.GetAllAttached())
                .Returns(testLocations.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _locationService.GetAllOrderedByNameAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Chicago", result.First().CityName);
            Assert.AreEqual("New York", result.Last().CityName);
        }

        [Test]
        public async Task GetLocationDetailsByIdAsync_ReturnsNull_WhenIdIsInvalid()
        {
            // Act
            var result = await _locationService.GetLocationDetailsByIdAsync("invalid-guid");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetLocationDetailsByIdAsync_ReturnsNull_WhenLocationDoesNotExist()
        {
            // Arrange
            var validId = Guid.NewGuid().ToString();
            _mockLocationRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Location>().AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _locationService.GetLocationDetailsByIdAsync(validId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetLocationDetailsByIdAsync_ReturnsCorrectDetails_WhenLocationExists()
        {
            // Arrange
            var locationId = Guid.NewGuid();
            var suggestionId = Guid.NewGuid();

            var testLocation = new Location
            {
                Id = locationId,
                CityName = "New York",
                StreetName = "5th Avenue",
                CityPicture = "image.jpg",
                SuggestionsLocations = new List<SuggestionLocation>
                {
                    new SuggestionLocation
                    {
                        Suggestion = new Suggestion { Id = suggestionId, Title = "Suggestion 1" }
                    }
                }
            };

            _mockLocationRepository.Setup(repo => repo.GetAllAttached())
                .Returns(new List<Location> { testLocation }.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _locationService.GetLocationDetailsByIdAsync(locationId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(locationId.ToString(), result.Id);
            Assert.AreEqual("New York", result.CityName);
            Assert.AreEqual("5th Avenue", result.StreetName);
            Assert.AreEqual("image.jpg", result.CityPicture);
            Assert.AreEqual(1, result.Suggestions.Count());
            Assert.AreEqual(suggestionId.ToString(), result.Suggestions.First().Id);
            Assert.AreEqual("Suggestion 1", result.Suggestions.First().Title);
        }
    }
}
