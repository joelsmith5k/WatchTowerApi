using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WatchTowerApi.Models
{
    public class HockeyGoalie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GoalieID { get; set; }

        [Required]
        public int CurrentTeamID { get; set; }

        [Required]
        public int LeagueID { get; set; }

        [Required]
        [MaxLength(100)]
        public string GoalieCode { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string? Dexterity { get; set; }

        [MaxLength(1)]
        public string? GloveHand { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public DateTime? DOB { get; set; }

        [Timestamp]
        public byte[] VersionCol { get; set; }

        // Navigation property to represent the relationship with HockeyTeam
        [ForeignKey("CurrentTeamID")]
        public virtual HockeyTeam HockeyTeam { get; set; }

        // Navigation property to represent the relationship with HockeyLeague
        [ForeignKey("LeagueID")]
        public virtual HockeyLeague HockeyLeague { get; set; }

        // // Navigation property for the one-to-many relationship with HockeyGoal
        [JsonIgnore]
        public virtual ICollection<HockeyGoal> HockeyGoals { get; set; } = new List<HockeyGoal>();

        // Navigation property for the one-to-many relationship with HockeyAssist
        [JsonIgnore]
        public virtual ICollection<HockeyAssist> HockeyAssists { get; set; } = new List<HockeyAssist>();
    }
}