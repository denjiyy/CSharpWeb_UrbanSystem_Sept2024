using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;

namespace UrbanSystem.Services.Data
{
    public class FundingService : IFundingService
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<Funding, Guid> _fundingRepository;
        private readonly IRepository<ProjectFunding, Guid> _projectFundingRepository;

        public FundingService(IRepository<Project, Guid> projectRepository, IRepository<Funding, Guid> fundingRepository, IRepository<ProjectFunding, Guid> projectFundingRepository)
        {
            _projectRepository = projectRepository;
            _fundingRepository = fundingRepository;
            _projectFundingRepository = projectFundingRepository;
        }

        public async Task<bool> AddFundingToProjectAsync(Guid projectId, decimal fundingAmount)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project == null)
            {
                throw new Exception("Project not found!");
            }

            Funding funding = new Funding
            {
                Amount = fundingAmount,
            };

            await _fundingRepository.AddAsync(funding);

            var projectFunding = new ProjectFunding
            {
                ProjectId = projectId,
                FundingId = funding.Id,
            };

            await _projectFundingRepository.AddAsync(projectFunding);

            var totalFunding = project.ProjectFundings
                .Sum(pf => pf.Funding.Amount);

            if (totalFunding >= project.DesiredSum)
            {
                project.IsCompleted = true;
            }

            await _projectRepository.UpdateAsync(project);

            return totalFunding >= project.DesiredSum;
        }
    }
}
