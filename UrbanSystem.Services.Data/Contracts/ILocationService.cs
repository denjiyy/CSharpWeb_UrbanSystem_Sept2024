using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Services.Data.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDetailsViewModel>> GetAllOrderedByNameAsync();

        Task AddLocationAsync(LocationFormViewModel model);

        Task<LocationDetailsViewModel> GetLocationDetailsByIdAsync(string? id);
    }
}
