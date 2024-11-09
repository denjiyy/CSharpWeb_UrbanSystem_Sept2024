using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Web.ViewModels.Comments
{
    public class CommentViewModel
    {
        public string Username { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime AddedOn { get; set; }
    }
}
