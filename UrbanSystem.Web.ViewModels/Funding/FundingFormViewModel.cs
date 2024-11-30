using System;
using System.ComponentModel.DataAnnotations;

namespace UrbanSystem.Web.ViewModels.Funding
{
    public class FundingFormViewModel
    {
        [Required]
        public Guid ProjectId { get; set; }

        public string ProjectName { get; set; }

        public decimal FundsNeeded { get; set; }

        public decimal FundsRaised { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "I confirm this funding amount")]
        public bool IsConfirmed { get; set; }
    }
}

