namespace TournamentNET.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Statistics
    {
        [Key]
        public int statistics_id { get; set; }

        public int match_id { get; set; }

        public int player_id { get; set; }

        public int? points { get; set; }

        public int? error { get; set; }

        public int? blocks { get; set; }

        public int? aces { get; set; }

        public virtual Matches Matches { get; set; }

        public virtual Players Players { get; set; }
    }
}
