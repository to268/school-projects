using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_bar_bar")]
    public class Bar : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("bar_id")]
        public int BarId { get; set; }


        [ForeignKey("PhotoId")]
        [Column("pht_photoid")]
        public int PhotoId { get; set; }

        [ForeignKey("ResortId")]
        [Column("res_id")]
        public int ResortId { get; set; }


        [Column("bar_nom")]
        [StringLength(255)]
        public string Nom { get; set; }

        [Column("bar_description")]
        [StringLength(1000)]
        public string Description { get; set; }

        [InverseProperty("LesBars")]
        public virtual Photo? LaPhoto { get; set; } = null!;

        [InverseProperty("LesBars")]
        public virtual Resort? LeResort { get; set; } = null!;
    }
}
