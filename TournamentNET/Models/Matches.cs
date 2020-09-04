namespace TournamentNET.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Matches
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Matches()
        {
            Statistics = new HashSet<Statistics>();
        }

        [Key]
        public int match_id { get; set; }

        public int tournament_id { get; set; }

        public int team1_id { get; set; }

        public int team2_id { get; set; }

        [StringLength(50)]
        public string game_result { get; set; }

        [StringLength(50)]
        public string game_points { get; set; }

        public int? ladder_position { get; set; }

        public virtual Teams Teams { get; set; }

        public virtual Teams Teams1 { get; set; }

        public virtual Tournaments Tournaments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Statistics> Statistics { get; set; }
    }
}
