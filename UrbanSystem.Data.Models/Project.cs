using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Data.Models
{
    public class Project
    {
        public Project()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal FundsRaised { get; set; } = 0m;
        public decimal FundsNeeded { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime FundingDeadline { get; set; }
        public bool IsCompleted { get; set; } = false;

        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public ICollection<Funding> Fundings { get; set; } = new HashSet<Funding>();
    }
}
