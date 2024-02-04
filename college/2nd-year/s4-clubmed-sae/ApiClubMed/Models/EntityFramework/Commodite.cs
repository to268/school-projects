using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_commodite_com")]
    public class Commodite : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("com_id")]
        public int CommoditeId { get; set; }

        [Column("com_nom")]
        [StringLength(255)]
        public string Nom { get; set; } = "";

        [InverseProperty("LesCommodites")]
        public virtual ICollection<TypeChambre>? LesTypeChambres { get; set; } = null!;
    }
}
