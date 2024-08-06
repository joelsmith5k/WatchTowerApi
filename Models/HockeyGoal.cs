using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTowerApi.Models
{
    public class HockeyGoal
    {
        [Key]
        public int GoalID { get; set; }

        [ForeignKey("HockeyGoalie")]
        public int GoalieID { get; set; }

        [ForeignKey("HockeyPlayer")]
        public int PlayerID { get; set; }

        [ForeignKey("HockeyTeam")]
        public int TeamID { get; set; }

        [ForeignKey("HockeyPosition")]
        public int? PositionID { get; set; }

        [ForeignKey("Dexterity")]
        public int? DexterityID { get; set; }

        [StringLength(3)]
        public string? PositionX { get; set; }

        [StringLength(3)]
        public string? PositionY { get; set; }

        public DateTime? Date { get; set; }

        [Timestamp]
        public byte[] VersionCol { get; set; }

        // Navigation properties
        public virtual HockeyGoalie HockeyGoalie { get; set; }
        public virtual HockeyPlayer HockeyPlayer { get; set; }
        public virtual HockeyTeam HockeyTeam { get; set; }
        public virtual HockeyPosition HockeyPosition { get; set; }
    }
}