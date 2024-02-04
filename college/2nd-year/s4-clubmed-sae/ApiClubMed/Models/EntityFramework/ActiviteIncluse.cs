using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_activiteincluse_aci")]
    public class ActiviteIncluse : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("aci_id")]
        public int ActiviteIncluseId { get; set; }

        [ForeignKey("TypeActiviteId")]
        [Column("tac_id")]
        public int TypeActiviteId { get; set; }

        [Column("aci_titre")]
        [StringLength(255)]
        public string Titre { get; set; } = null!;

        [Column("aci_duree")]
        public int? Duree { get; set; }

        [Column("aci_description")]
        public string? Description { get; set; } = null!;

        [Column("aci_frequence")]
        public string Frequence { get; set; } = null!;

        [Column("aci_agemin")]
        public int? AgeMin { get; set; }

        [InverseProperty("LesActiviteIncluses")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;

        [InverseProperty("LesActiviteIncluses")]
        public virtual TypeActivite? LeTypeActivite { get; set; } = null!;
    }
}
