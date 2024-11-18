using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Services.Data.Contracts
{
    public interface IFundingService
    {
        Task<bool> AddFundingToProjectAsync(Guid projectId, decimal fundingAmount);
    }
}
