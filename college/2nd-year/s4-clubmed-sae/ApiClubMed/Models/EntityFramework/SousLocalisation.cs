using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_souslocalisation_slo")]
    public class SousLocalisation : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("slo_id")]
        public int SousLocalisationId { get; set; }

        [Column("slo_nom")]
        [StringLength(255)]
        public string Nom { get; set; }

        [InverseProperty("LesSousLocalisations")]
        public virtual ICollection<Localisation>? LesLocalisations { get; set; } = null!;

        [InverseProperty("LaSousLocalisation")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;
    }
}
