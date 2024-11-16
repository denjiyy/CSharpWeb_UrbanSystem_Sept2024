using System;

namespace UrbanSystem.Data.Models
{
    public class ProjectFunding
    {
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; } = null!;
        public Guid FundingId { get; set; }
        public virtual Funding Funding { get; set; } = null!;
    }
}
