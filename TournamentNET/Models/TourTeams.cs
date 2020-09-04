using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentNET.Models
{
    public class TourTeams
    {
        public Tournaments tournaments { get; set; }
        public List<Teams> teams { get; set; }
    }
}