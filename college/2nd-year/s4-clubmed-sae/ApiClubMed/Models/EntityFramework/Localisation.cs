using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_localisation_loc")]
    public class Localisation : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("loc_id")]
        public int LocalisationId { get; set; }

        [Column("loc_nom")]
        [StringLength(255)]
        public string Nom { get; set; }

        [InverseProperty("LaLocalisation")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;

        [InverseProperty("LesLocalisations")]
        public virtual ICollection<SousLocalisation>? LesSousLocalisations { get; set; } = null!;
    }
}
