namespace TournamentNET.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<Matches> Matches { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Statistics> Statistics { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Tournaments> Tournaments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matches>()
                .Property(e => e.game_result)
                .IsUnicode(false);

            modelBuilder.Entity<Matches>()
                .Property(e => e.game_points)
                .IsUnicode(false);

            modelBuilder.Entity<Matches>()
                .HasMany(e => e.Statistics)
                .WithRequired(e => e.Matches)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Players>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Players>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<Players>()
                .HasMany(e => e.Statistics)
                .WithRequired(e => e.Players)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teams>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Teams>()
                .HasMany(e => e.Matches)
                .WithRequired(e => e.Teams)
                .HasForeignKey(e => e.team1_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teams>()
                .HasMany(e => e.Matches1)
                .WithRequired(e => e.Teams1)
                .HasForeignKey(e => e.team2_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teams>()
                .HasMany(e => e.Players)
                .WithRequired(e => e.Teams)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournaments>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Tournaments>()
                .HasMany(e => e.Matches)
                .WithRequired(e => e.Tournaments)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournaments>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Tournaments)
                .WillCascadeOnDelete(false);
        }
    }
}
