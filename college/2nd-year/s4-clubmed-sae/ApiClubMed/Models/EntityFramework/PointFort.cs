using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_pointfort_ptf")]
    public class PointFort : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ptf_id")]
        public int PointFortId { get; set; }

        [Column("ptf_libelle")]
        [StringLength(255)]
        public string Libelle { get; set; } = "";

        [InverseProperty("LesPointForts")]
        public virtual ICollection<TypeChambre>? LesTypeChambres { get; set; } = null!;
    }
}
