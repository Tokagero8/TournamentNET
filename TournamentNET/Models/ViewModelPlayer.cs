using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentNET.Models
{
    public class ViewModelPlayer
    {
        public int player_id { get; set; }
        public string name_lastname { get; set; }

        public ViewModelPlayer(int player_id, string name_lastname)
        {
            this.player_id = player_id;
            this.name_lastname = name_lastname;
        }

    }
}