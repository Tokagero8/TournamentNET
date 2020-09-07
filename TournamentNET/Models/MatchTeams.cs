using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentNET.Models
{
    public class MatchTeams
    {
        public int match_id { get; set; }
        public string team1_and_team2 { get; set; }

        public MatchTeams(int match_id, string team1_and_team2)
        {
            this.match_id = match_id;
            this.team1_and_team2 = team1_and_team2;
        }
    }
}