using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Data.Models
{
    public class Funding
    {
        public Funding()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public decimal Amount { get; set; }
        public DateTime FundedOn { get; set; } = DateTime.UtcNow;
    }
}
