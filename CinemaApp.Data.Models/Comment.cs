using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid SuggestionId { get; set; }
        public Suggestion Suggestion { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime PostedOn { get; set; } = DateTime.UtcNow.Date;
    }
}
