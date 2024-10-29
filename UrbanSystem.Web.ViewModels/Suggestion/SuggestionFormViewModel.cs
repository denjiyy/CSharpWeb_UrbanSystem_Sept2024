using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;

namespace UrbanSystem.Web.ViewModels.Suggestions
{
    public class SuggestionFormViewModel
    {
        public Suggestion Suggestion { get; set; } = new Suggestion();

        public string StreetName { get; set; } = null!;
    }
}
