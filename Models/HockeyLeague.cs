using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTowerApi.Models
{
    public class HockeyLeague
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeagueID { get; set; }

        [Required]
        [MaxLength(6)]
        public string LeagueCode { get; set; }

        [Required]
        [MaxLength(40)]
        public string LeagueDescription { get; set; }

        [Timestamp]
        public byte[] VersionCol { get; set; }

        // Navigation property to represent the relationship with HockeyTeam
        public virtual ICollection<HockeyTeam> HockeyTeams { get; set; }
    }
}