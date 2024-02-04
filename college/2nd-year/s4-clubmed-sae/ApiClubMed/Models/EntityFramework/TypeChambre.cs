using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_typechambre_tpc")]
    public class TypeChambre : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tpc_id")]
        public int TypeChambreId { get; set; }


        [ForeignKey("ResortId")]
        [Column("res_id")]
        public int? ResortId { get; set; }

        [ForeignKey("CategorieTypeChambreId")]
        [Column("ctc_id")]
        public int? CategorieTypeChambreId { get; set; }

        [Column("tpc_surface", TypeName = "numeric(8,2)")]
        public decimal Surface { get; set; }

        [Column("tpc_capacite")]
        public int? Capacite { get; set; }

        [Column("tpc_presentation")]
        [StringLength(1000)]
        public string Presentation { get; set; }

        [Column("tpc_libellecatgorie")]
        [StringLength(255)]
        public string LibelleCatgorie { get; set; } = "";

        [Column("tpc_prixjournalier")]
        public int? PrixJournalier { get; set; }

        [InverseProperty("LesTypeChambres")]
        public virtual CategorieTypeChambre? LaCategorieTypeChambre { get; set; } = null!;

        [InverseProperty("LesTypeChambres")]
        public virtual ICollection<PointFort>? LesPointForts { get; set; } = null!;

        [InverseProperty("LesTypeChambres")]
        public virtual ICollection<Commodite>? LesCommodites { get; set; } = null!;


        [InverseProperty("LesTypeChambres")]
        public virtual Resort? LeResort { get; set; } = null!;
    }
}
