using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WatchTowerApi.Models
{
    public class HockeyPlayer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerID { get; set; }

        [Required]
        [StringLength(100)]
        public string PlayerCode { get; set; }

        [Required]
        public int DefaultTeamID { get; set; }

        [Required]
        public int DefaultPositionID { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        public int? DexterityID { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        public DateTime? DOB { get; set; }

        [Timestamp]
        public byte[] VersionCol { get; set; }

        // Navigation properties (optional, if you want to use them in your application)
        [ForeignKey("DefaultTeamID")]
        public virtual HockeyTeam DefaultTeam { get; set; }

        [ForeignKey("DefaultPositionID")]
        public virtual HockeyPosition DefaultPosition { get; set; }

        // Navigation property for the one-to-many relationship with HockeyGoal
        [JsonIgnore]
        public virtual ICollection<HockeyGoal> HockeyGoals { get; set; } = new List<HockeyGoal>();

        // Navigation property for the one-to-many relationship with HockeyAssist
        [JsonIgnore]
        public virtual ICollection<HockeyAssist> HockeyAssists { get; set; } = new List<HockeyAssist>();
    }
}