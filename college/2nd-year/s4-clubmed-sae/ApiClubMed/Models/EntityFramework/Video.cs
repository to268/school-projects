using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_video_vid")]
    public class Video : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("vid_id")]
        public int VideoId { get; set; }

        [Column("vid_lien")]
        [StringLength(255)]
        public string Lien { get; set; } = "";

        [InverseProperty("LesVideos")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;
    }
}
