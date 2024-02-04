using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_activiteenfantincluse_aei")]
    public class ActiviteEnfantIncluse : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("aei_id")]
        public int ActiviteEnfantIncluseId { get; set; }

        [Column("aei_titre")]
        [StringLength(255)]
        public string Titre { get; set; }

        [Column("aei_duree")]
        public int Duree { get; set; }

        [Column("aei_description")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Column("aei_frequence")]
        [StringLength(255)]
        public string Frequence { get; set; }

        [InverseProperty("LesActiviteEnfantIncluses")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;

        [InverseProperty("LesActiviteEnfantIncluses")]
        public virtual ICollection<TrancheAge>? LesTrancheAges { get; set; } = null!;
    }
}
