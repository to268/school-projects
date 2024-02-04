using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_activiteenfantcarte_aec")]
    public class ActiviteEnfantCarte : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("aec_id")]
        public int ActiviteEnfantCarteId { get; set; }

        [Column("aec_titre")]
        [StringLength(255)]
        public string Titre { get; set; }

        [Column("aec_duree")]
        public int Duree { get; set; }

        [Column("aec_description")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Column("aec_frequence")]
        [StringLength(255)]
        public string Frequence { get; set; }

        [Column("aec_prix", TypeName = "numeric(8,2)")]
        public decimal Prix { get; set; }


        [InverseProperty("LesActiviteEnfantCartes")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;

        [InverseProperty("LesActiviteEnfantCartes")]
        public virtual ICollection<TrancheAge>? LesTrancheAges { get; set; } = null!;

        [InverseProperty("LesActiviteEnfantCartes")]
        public virtual ICollection<Reservation>? LesReservations { get; set; } = null!;
    }
}
