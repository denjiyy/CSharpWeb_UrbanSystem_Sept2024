using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task AddLocationAsync(LocationFormViewModel model)
        {
            Location location = new Location();
            AutoMapperConfig.MapperInstance.Map(model, location);

            await _locationRepository.AddAsync(location);
        }

        public async Task<IEnumerable<LocationDetailsViewModel>> GetAllOrderedByNameAsync()
        {
            var locations = await _locationRepository
                .GetAllAttached()
                .OrderBy(l => l.CityName)
                .To<LocationDetailsViewModel>()
                .ToListAsync();

            return locations;
        }

        public Task<LocationDetailsViewModel> GetLocationDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
