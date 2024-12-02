using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data;
using UrbanSystem.Web.ViewModels.Locations;

namespace ServiceTests
{
    [TestFixture]
    public class BaseServiceTests
    {
        private Mock<IRepository<Location, Guid>> _mockLocationRepository;
        private BaseService _baseService;

        [SetUp]
        public void Setup()
        {
            _mockLocationRepository = new Mock<IRepository<Location, Guid>>();
            _baseService = new BaseService(_mockLocationRepository.Object);
        }

        [Test]
        public async Task GetCitiesAsync_ReturnsCorrectCityOptions()
        {
            // Arrange
            var testLocations = new List<Location>
            {
                new Location { Id = Guid.NewGuid(), CityName = "New York" },
                new Location { Id = Guid.NewGuid(), CityName = "Los Angeles" },
                new Location { Id = Guid.NewGuid(), CityName = "Chicago" }
            };

            _mockLocationRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(testLocations);

            // Act
            var result = await _baseService.GetCitiesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.All(c => testLocations.Any(l => l.Id.ToString() == c.Value && l.CityName == c.Text)));
        }

        [Test]
        public async Task GetCitiesAsync_ReturnsEmptySet_WhenNoLocationsExist()
        {
            // Arrange
            _mockLocationRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Location>());

            // Act
            var result = await _baseService.GetCitiesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetCitiesAsync_ReturnsUniqueSet_WhenDuplicateCitiesExist()
        {
            // Arrange
            var testLocations = new List<Location>
    {
        new Location { Id = Guid.NewGuid(), CityName = "New York" },
        new Location { Id = Guid.NewGuid(), CityName = "New York" },
        new Location { Id = Guid.NewGuid(), CityName = "Chicago" }
    };

            _mockLocationRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(testLocations);

            // Act
            var result = await _baseService.GetCitiesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(c => c.Text == "New York"));
            Assert.IsTrue(result.Any(c => c.Text == "Chicago"));
        }
    }
}