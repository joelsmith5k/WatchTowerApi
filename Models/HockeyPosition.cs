using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTowerApi.Models
{
    public class HockeyPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HockeyPositionID { get; set; }

        [Required]
        [StringLength(5)]
        public string HockeyPositionCode { get; set; }

        [Required]
        [StringLength(50)]
        public string HockeyPositionDescription { get; set; }

        [Timestamp]
        public byte[] VersionCol { get; set; }
    }
}