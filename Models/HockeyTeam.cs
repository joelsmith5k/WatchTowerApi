using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WatchTowerApi.Models
{
    public class HockeyTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamID { get; set; }

        [Required]
        public int LeagueID { get; set; }

        [Required]
        [MaxLength(20)]
        public string TeamCode { get; set; }

        [Required]
        [MaxLength(6)]
        public string CityCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string MascotDescription { get; set; }

        [Required]
        public bool ActiveInd { get; set; }

        [Required]
        public int TeamInceptionYear { get; set; }

        public int? TeamFinalYear { get; set; }

        [Timestamp]
        public byte[] VersionCol { get; set; }

        // Navigation property to represent the relationship with HockeyLeague
        [ForeignKey("LeagueID")]
        public virtual HockeyLeague HockeyLeague { get; set; }

        // Navigation property to represent the relationship with HockeyGoalie
        [JsonIgnore]
        public virtual ICollection<HockeyGoalie> HockeyGoalies { get; set; }

        // // Navigation property for the one-to-many relationship with HockeyGoal
        // public virtual ICollection<HockeyGoal> HockeyGoals { get; set; } = new List<HockeyGoal>();

        // // Navigation property for the one-to-many relationship with HockeyAssist
        // public virtual ICollection<HockeyAssist> HockeyAssists { get; set; } = new List<HockeyAssist>();
    }
}