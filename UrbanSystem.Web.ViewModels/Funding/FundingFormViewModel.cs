using System;

namespace UrbanSystem.Web.ViewModels.Funding
{
    public class FundingFormViewModel
    {
        public Guid ProjectId { get; set; } 
        public decimal Amount { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
