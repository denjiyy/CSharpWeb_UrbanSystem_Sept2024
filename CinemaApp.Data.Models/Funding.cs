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
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public virtual ICollection<ProjectFunding> ProjectFundings { get; set; } = new HashSet<ProjectFunding>();
    }
}
