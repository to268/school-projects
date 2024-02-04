using ApiClubMed.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_restaurant_rst")]
    public class Restaurant : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("rst_id")]
        public int RestaurantId { get; set; }


        [ForeignKey("PhotoId")]
        [Column("pht_photoid")]
        public int PhotoId { get; set; }

        [ForeignKey("ResortId")]
        [Column("res_id")]
        public int ResortId { get; set; }


        [Column("bar_nom")]
        [StringLength(255)]
        public string Nom { get; set; } = "";

        [Column("bar_description")]
        [StringLength(1000)]
        public string Description { get; set; } = "";

        [InverseProperty("LesRestaurants")]
        public virtual Photo? LaPhoto { get; set; } = null!;

        [InverseProperty("LesRestaurants")]
        public virtual Resort? LeResort { get; set; } = null!;
    }
}
