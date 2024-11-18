using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.SuggestionsLocations;

namespace UrbanSystem.Services.Data
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location, Guid> _locationRepository;

        public LocationService(IRepository<Location, Guid> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task AddLocationAsync(LocationFormViewModel model)
        {
            var location = new Location
            {
                CityName = model.CityName,
                StreetName = model.StreetName,
                CityPicture = model.CityPicture
            };

            await _locationRepository.AddAsync(location);
        }

        public async Task<IEnumerable<LocationDetailsViewModel>> GetAllOrderedByNameAsync()
        {
            var locations = await _locationRepository
                .GetAllAttached()
                .OrderBy(l => l.CityName)
                .ToListAsync();

            // Manual mapping
            var viewModel = locations.Select(l => new LocationDetailsViewModel
            {
                Id = l.Id.ToString(),
                CityName = l.CityName,
                StreetName = l.StreetName,
                CityPicture = l.CityPicture,
                Suggestions = new List<SuggestionLocationViewModel>() // Suggestions not included in this query
            });

            return viewModel;
        }

        public async Task<LocationDetailsViewModel?> GetLocationDetailsByIdAsync(Guid id)
        {
            var location = await _locationRepository
                .GetAllAttached()
                .Include(l => l.SuggestionsLocations)
                .ThenInclude(sl => sl.Suggestion)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (location == null)
            {
                return null;
            }

            // Manual mapping
            var viewModel = new LocationDetailsViewModel
            {
                Id = location.Id.ToString(),
                CityName = location.CityName,
                StreetName = location.StreetName,
                CityPicture = location.CityPicture,
                Suggestions = location.SuggestionsLocations.Select(sl => new SuggestionLocationViewModel
                {
                    Id = sl.Suggestion.Id.ToString(),
                    Title = sl.Suggestion.Title
                }).ToList()
            };

            return viewModel;
        }
    }
}
