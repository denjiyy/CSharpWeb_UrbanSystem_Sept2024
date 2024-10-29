using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Data.Models
{
    public class Province
    {
        public Province()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string CityName { get; set; } = null!;

        public string StreetName { get; set; } = null!;

        public ICollection<Suggestion> Suggestions { get; set; } = new HashSet<Suggestion>();
    }
}
