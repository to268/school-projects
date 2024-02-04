using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_categorietypechambre_ctc")]
    public class CategorieTypeChambre : IEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ctc_id")]
        public int CategorieTypeChambreId { get; set; }



        [Column("ctc_libelle")]
        [StringLength(255)]
        public string Libelle { get; set; } = "";

        [InverseProperty("LaCategorieTypeChambre")]
        public virtual ICollection<TypeChambre>? LesTypeChambres { get; set; } = null!;
    }
}
