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

        [ForeignKey(nameof(PlayerTeam))]
        public int PlayerTeamID { get; set; }

        [ForeignKey(nameof(GoalieTeam))]
        public int GoalieTeamID { get; set; }

        [ForeignKey("HockeyPosition")]
        public int? PositionID { get; set; }

        [ForeignKey("Dexterity")]
        public int? DexterityID { get; set; }

        [StringLength(3)]
        public string? PositionX { get; set; }

        [StringLength(3)]
        public string? PositionY { get; set; }

        public DateTime? GoalDate { get; set; }

        [Timestamp]
        public byte[] VersionCol { get; set; }

        // Navigation properties
        public virtual HockeyGoalie HockeyGoalie { get; set; }
        public virtual HockeyPlayer HockeyPlayer { get; set; }
        public virtual HockeyTeam PlayerTeam { get; set; }
        public virtual HockeyTeam GoalieTeam { get; set; }
        public virtual HockeyPosition HockeyPosition { get; set; }
    }
}