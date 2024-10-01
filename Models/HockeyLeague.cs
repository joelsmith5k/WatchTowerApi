using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<HockeyTeam> HockeyTeams { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<HockeyGoalie> HockeyGoalies { get; set; }
    }
}