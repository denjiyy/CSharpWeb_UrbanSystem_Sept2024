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
        public string Description { get; set; } = null!; 
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;
        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;
        public virtual ICollection<ProjectFunding> ProjectFundings { get; set; } = new HashSet<ProjectFunding>(); 
    }
}
