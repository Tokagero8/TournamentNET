using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentNET.Models
{
    public class ViewModelMatches
    {
        public Matches matches { get; set; }
        public List<Statistics> statistics { get; set; }
    }
}