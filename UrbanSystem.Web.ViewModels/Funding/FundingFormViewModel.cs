using System;
using System.ComponentModel.DataAnnotations;

namespace UrbanSystem.Web.ViewModels.Funding
{
    public class FundingFormViewModel
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public bool IsConfirmed { get; set; }
    }
}
