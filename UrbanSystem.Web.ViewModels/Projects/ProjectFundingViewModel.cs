using System;

namespace UrbanSystem.Web.ViewModels.Projects
{
    public class ProjectFundingViewModel
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime FundedOn { get; set; }
    }
}
