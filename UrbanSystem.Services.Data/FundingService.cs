using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;

namespace UrbanSystem.Services.Data
{
    public class FundingService : IFundingService
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<Funding, Guid> _fundingRepository;

        public FundingService(IRepository<Project, Guid> projectRepository, IRepository<Funding, Guid> fundingRepository)
        {
            _projectRepository = projectRepository;
            _fundingRepository = fundingRepository;
        }

        public async Task<bool> AddFundingAsync(Guid projectId, decimal amount)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null || amount <= 0)
            {
                return false;
            }

            var funding = new Funding
            {
                ProjectId = projectId,
                Amount = amount,
                FundedOn = DateTime.UtcNow
            };

            await _fundingRepository.AddAsync(funding);

            project.FundsRaised += amount;
            project.FundsNeeded -= amount;

            await _projectRepository.UpdateAsync(project);

            return true;
        }

        public async Task<decimal> GetTotalFundingAsync(Guid projectId)
        {
            return await _fundingRepository.GetAllAttached()
                                 .Where(f => f.ProjectId == projectId)
                                 .SumAsync(f => f.Amount);
        }
    }
}
