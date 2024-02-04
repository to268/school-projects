using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_photo_pht")]
    public class Photo : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("pht_id")]
        public int PhotoId { get; set; }

        [Column("pht_lien")]
        [StringLength(255)]
        public string Lien { get; set; } = "";

        [InverseProperty("LaPhoto")]
        public virtual ICollection<Bar>? LesBars { get; set; } = null!;

        [InverseProperty("LaPhoto")]
        public virtual ICollection<Restaurant>? LesRestaurants { get; set; } = null!;

        [InverseProperty("LaPhoto")]
        public virtual ICollection<TypeActivite>? LesTypeActivites { get; set; } = null!;

        [InverseProperty("LaPhoto")]
        public virtual ICollection<Avis>? LesAvis { get; set; } = null!;

        [InverseProperty("LesPhotos")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;

        [InverseProperty("LaPhoto")]
        public virtual ICollection<DomaineSkiable>? LesDomaineSkiables { get; set; } = null!;
    }
}
