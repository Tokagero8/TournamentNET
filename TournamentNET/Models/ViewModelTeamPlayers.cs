using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentNET.Models
{
    public class ViewModelTeamPlayers
    {
        public Teams teams { get; set; }
        public List<Players> players {get; set;}
    }
}