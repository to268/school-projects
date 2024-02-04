using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.Json.Serialization;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_domaineskiable_dms")]
    public class DomaineSkiable : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("dms_id")]
        public int DomaineSkiableId { get; set; }

        [ForeignKey("PhotoId")]
        [Column("pht_id")]
        public int PhotoId { get; set; }

        [Column("dms_titre")]
        [StringLength(255)]
        public string Titre { get; set; }

        [Column("dms_nom")]
        [StringLength(255)]
        public string Nom { get; set; }

        [Column("dms_altitudeclub")]
        public int AltitudeClub { get; set; }

        [Column("dms_altitudestation")]
        public int AltitudeStation { get; set; }

        [Column("dms_nbpiste")]
        public int NbPiste { get; set; }

        [Column("dms_infoskiaupied")]
        public bool InfoSkiAuPied { get; set; }

        [Column("dms_description")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Column("dms_longueurdespistes", TypeName = "numeric(8,2)")]
        public decimal LongueurDesPistes { get; set; }

        [InverseProperty("LesDomaineSkiables")]
        public virtual Photo? LaPhoto { get; set; } = null!;

        [InverseProperty("LeDomaineSkiable")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            DomaineSkiable autreDomaineSkiable = obj as DomaineSkiable;
            if (autreDomaineSkiable != null)
                return this.DomaineSkiableId.CompareTo(autreDomaineSkiable.DomaineSkiableId);
            else
                throw new ArgumentException("Object is not a DomaineSkiable");
        }
    }
}
