using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_typeactivite_tac")]
    public class TypeActivite : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tac_id")]
        public int TypeActiviteId { get; set; }

        [ForeignKey("PhotoId")]
        [Column("pht_id")]
        public int PhotoId { get; set; }

        [Column("tac_titre")]
        [StringLength(255)]
        public string Titre { get; set; } = null!;

        [Column("tac_description")]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Column("tac_nbactiviteincluse")]
        public int NbActiviteIncluse { get; set; }

        [Column("tac_nbactivitecarte")]
        public int NbActiviteCarte { get; set; }

        [ForeignKey("PhotoId")]
        [InverseProperty("LesTypeActivites")]
        public virtual Photo? LaPhoto { get; set; } = null!;

        [InverseProperty("LesTypeActivites")]
        public virtual ICollection<TrancheAge>? LesTrancheAges { get; set; } = null!;

        [InverseProperty("LeTypeActivite")]
        public virtual ICollection<ActiviteIncluse>? LesActiviteIncluses { get; set; } = null!;

        [InverseProperty("LeTypeActivite")]
        public virtual ICollection<ActiviteCarte>? LesActiviteCartes { get; set; } = null!;
    }
}
