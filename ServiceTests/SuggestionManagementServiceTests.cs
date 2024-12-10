using Moq;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Web.ViewModels.Suggestions;
using UrbanSystem.Services.Data;
using System.Linq.Expressions;

namespace ServiceTests
{
    [TestFixture]
    public class SuggestionManagementServiceTests
    {
        private Mock<IRepository<Suggestion, Guid>> _mockSuggestionRepository;
        private Mock<IRepository<Location, Guid>> _mockLocationRepository;
        private SuggestionManagementService _suggestionService;

        [SetUp]
        public void SetUp()
        {
            _mockSuggestionRepository = new Mock<IRepository<Suggestion, Guid>>();
            _mockLocationRepository = new Mock<IRepository<Location, Guid>>();
            _suggestionService = new SuggestionManagementService(_mockSuggestionRepository.Object, _mockLocationRepository.Object);
        }

        [Test]
        public async Task GetAllSuggestionsAsync_ShouldReturnSuggestions()
        {
            // Arrange
            var suggestions = new List<Suggestion>
            {
                new Suggestion
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Suggestion 1",
                    Category = "Category 1",
                    UsersSuggestions = new List<ApplicationUserSuggestion> { new ApplicationUserSuggestion { User = new ApplicationUser { UserName = "User1" } } },
                    AttachmentUrl = "http://example.com",
                    Description = "Test description",
                    UploadedOn = DateTime.UtcNow,
                    Status = "Open",
                    Priority = "High",
                    SuggestionsLocations = new List<SuggestionLocation> { new SuggestionLocation { Location = new Location { CityName = "City1" } } }
                }
            };

            _mockSuggestionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(suggestions);

            // Act
            var result = await _suggestionService.GetAllSuggestionsAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Title, Is.EqualTo("Test Suggestion 1"));
            Assert.That(result.First().OrganizerName, Is.EqualTo("User1"));
            Assert.That(result.First().LocationNames.First(), Is.EqualTo("City1"));
        }

        [Test]
        public async Task GetSuggestionByIdAsync_ShouldReturnNullIfNotFound()
        {
            // Arrange
            var suggestionId = Guid.NewGuid();
            _mockSuggestionRepository?.Setup(repo => repo.GetByIdAsync(suggestionId))!.ReturnsAsync((Suggestion?)null);

            // Act
            var result = await _suggestionService.GetSuggestionByIdAsync(suggestionId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetSuggestionByIdAsync_ShouldReturnSuggestionIfFound()
        {
            // Arrange
            var suggestionId = Guid.NewGuid();
            var suggestion = new Suggestion
            {
                Id = suggestionId,
                Title = "Test Suggestion",
                Category = "Category",
                UsersSuggestions = new List<ApplicationUserSuggestion> { new ApplicationUserSuggestion { User = new ApplicationUser { UserName = "User1" } } },
                AttachmentUrl = "http://example.com",
                Description = "Test description",
                UploadedOn = DateTime.UtcNow,
                Status = "Open",
                Priority = "High",
                SuggestionsLocations = new List<SuggestionLocation> { new SuggestionLocation { Location = new Location { CityName = "City1" } } },
                Comments = new List<Comment> { new Comment { Content = "Test comment", User = new ApplicationUser { UserName = "User1" }, AddedOn = DateTime.UtcNow } }
            };
            _mockSuggestionRepository.Setup(repo => repo.GetByIdAsync(suggestionId)).ReturnsAsync(suggestion);

            // Act
            var result = await _suggestionService.GetSuggestionByIdAsync(suggestionId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Title, Is.EqualTo("Test Suggestion"));
            Assert.That(result.Comments.First().Content, Is.EqualTo("Test comment"));
        }

        [Test]
        public async Task CreateSuggestionAsync_ShouldReturnTrueWhenSuggestionIsCreated()
        {
            // Arrange
            var model = new SuggestionFormViewModel
            {
                Title = "New Suggestion",
                Category = "Category",
                AttachmentUrl = "http://example.com",
                Description = "Test description",
                Status = "Open",
                Priority = "High",
                CityName = "City1",
                StreetName = "Street1"
            };

            // Mock the location repository to return a location matching the city and street
            _mockLocationRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Location, bool>>>()))
                                   .ReturnsAsync(new List<Location> { new Location { CityName = "City1", StreetName = "Street1" } });

            // Mock the suggestion repository to simulate the addition of a new suggestion
            _mockSuggestionRepository.Setup(repo => repo.AddAsync(It.IsAny<Suggestion>())).Returns(Task.CompletedTask);

            // Act
            var result = await _suggestionService.CreateSuggestionAsync(model);

            // Assert
            Assert.IsTrue(result);
            _mockSuggestionRepository.Verify(repo => repo.AddAsync(It.IsAny<Suggestion>()), Times.Once);
        }

        [Test]
        public async Task GetSuggestionForEditAsync_ShouldReturnNullIfNotFound()
        {
            // Arrange
            var suggestionId = Guid.NewGuid();
            _mockSuggestionRepository.Setup(repo => repo.GetByIdAsync(suggestionId))!.ReturnsAsync((Suggestion?)null);

            // Act
            var result = await _suggestionService.GetSuggestionForEditAsync(suggestionId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateSuggestionAsync_ShouldReturnTrueIfUpdated()
        {
            // Arrange
            var suggestionId = Guid.NewGuid();
            var model = new SuggestionFormViewModel
            {
                Title = "Updated Suggestion",
                Category = "Updated Category",
                AttachmentUrl = "http://updated.com",
                Description = "Updated description",
                Status = "Closed",
                Priority = "Low",
                CityName = "Updated City",
                StreetName = "Updated Street"
            };

            var suggestion = new Suggestion { Id = suggestionId };
            _mockSuggestionRepository.Setup(repo => repo.GetByIdAsync(suggestionId)).ReturnsAsync(suggestion);

            // Mock the location repository to return a matching location
            _mockLocationRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Location, bool>>>()))
                                   .ReturnsAsync(new List<Location> { new Location { CityName = "Updated City", StreetName = "Updated Street" } });

            _mockSuggestionRepository.Setup(repo => repo.UpdateAsync(suggestion)).ReturnsAsync(true);

            // Act
            var result = await _suggestionService.UpdateSuggestionAsync(suggestionId, model);

            // Assert
            Assert.IsTrue(result);
            _mockSuggestionRepository.Verify(repo => repo.UpdateAsync(suggestion), Times.Once);
        }

        [Test]
        public async Task DeleteSuggestionAsync_ShouldReturnTrueIfDeleted()
        {
            // Arrange
            var suggestionId = Guid.NewGuid();
            _mockSuggestionRepository.Setup(repo => repo.DeleteAsync(suggestionId)).ReturnsAsync(true);

            // Act
            var result = await _suggestionService.DeleteSuggestionAsync(suggestionId);

            // Assert
            Assert.IsTrue(result);
            _mockSuggestionRepository.Verify(repo => repo.DeleteAsync(suggestionId), Times.Once);
        }

        [Test]
        public async Task UpdateSuggestionStatusAsync_ShouldReturnFalseIfNotFound()
        {
            // Arrange
            var suggestionId = Guid.NewGuid();
            _mockSuggestionRepository.Setup(repo => repo.GetByIdAsync(suggestionId))!.ReturnsAsync((Suggestion?)null);

            // Act
            var result = await _suggestionService.UpdateSuggestionStatusAsync(suggestionId, "Closed");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateSuggestionPriorityAsync_ShouldReturnTrueIfUpdated()
        {
            // Arrange
            var suggestionId = Guid.NewGuid();
            var suggestion = new Suggestion { Id = suggestionId };
            _mockSuggestionRepository.Setup(repo => repo.GetByIdAsync(suggestionId)).ReturnsAsync(suggestion);
            _mockSuggestionRepository.Setup(repo => repo.UpdateAsync(suggestion)).ReturnsAsync(true);

            // Act
            var result = await _suggestionService.UpdateSuggestionPriorityAsync(suggestionId, "Low");

            // Assert
            Assert.IsTrue(result);
            _mockSuggestionRepository.Verify(repo => repo.UpdateAsync(suggestion), Times.Once);
        }
    }
}
