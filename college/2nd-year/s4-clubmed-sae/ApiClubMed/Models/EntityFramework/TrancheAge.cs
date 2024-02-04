using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_trancheage_tag")]
    public class TrancheAge : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tag_id")]
        public int TrancheAgeId { get; set; }

        [Column("tag_agemin")]
        public int AgeMin { get; set; }

        [Column("tag_agemax")]
        public int AgeMax { get; set; }

        [InverseProperty("LesTrancheAges")]
        public virtual ICollection<TypeActivite>? LesTypeActivites { get; set; } = null!;

        [InverseProperty("LesTrancheAges")]
        public virtual ICollection<ActiviteEnfantCarte>? LesActiviteEnfantCartes { get; set; } = null!;

        [InverseProperty("LesTrancheAges")]
        public virtual ICollection<ActiviteEnfantIncluse>? LesActiviteEnfantIncluses { get; set; } = null!;
    }
}
