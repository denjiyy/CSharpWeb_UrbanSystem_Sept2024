using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Services.Mapping;
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Services.Data
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location, Guid> _locationRepository;

        public LocationService(IRepository<Location, Guid> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        // Add a new location to the database
        public async Task AddLocationAsync(LocationFormViewModel model)
        {
            // Map the form view model to the Location entity
            Location location = new Location();
            AutoMapperConfig.MapperInstance.Map(model, location);

            // Add the location to the repository
            await _locationRepository.AddAsync(location);
        }

        // Get all locations ordered by city name
        public async Task<IEnumerable<LocationDetailsViewModel>> GetAllOrderedByNameAsync()
        {
            var locations = await _locationRepository
                .GetAllAttached()
                .OrderBy(l => l.CityName) // Sorting locations by city name
                .To<LocationDetailsViewModel>() // Convert locations to view model
                .ToListAsync(); // Execute the query

            return locations;
        }

        // Get details of a location by its ID
        public async Task<LocationDetailsViewModel?> GetLocationDetailsByIdAsync(Guid id)
        {
            var location = await _locationRepository
                .GetAllAttached() // Ensure we're tracking the location
                .Include(l => l.SuggestionsLocations) // Include suggestions related to the location
                .ThenInclude(sl => sl.Suggestion) // Include suggestions details
                .FirstOrDefaultAsync(l => l.Id == id); // Find location by ID

            // Return null if location not found
            if (location == null)
            {
                return null;
            }

            // Map the location to the view model
            var viewModel = new LocationDetailsViewModel();
            AutoMapperConfig.MapperInstance.Map(location, viewModel);
            return viewModel;
        }
    }
}
