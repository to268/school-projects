using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_activitecarte_aca")]
    public class ActiviteCarte : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("aca_id")]
        public int ActiviteCarteId { get; set; }


        [ForeignKey("TypeActiviteId")]
        [Column("tac_id")]
        public int TypeActiviteId { get; set; }

        [Column("aca_titre")]
        [StringLength(255)]
        public string Titre { get; set; } = null!;

        [Column("aca_duree")]
        public int Duree { get; set; }

        [Column("aca_description")]
        public string Description { get; set; } = null!;

        [Column("aca_frequence")]
        public string Frequence { get; set; } = null!;

        [Column("aca_agemin")]
        public int AgeMin { get; set; }

        [Column("aca_prix", TypeName = "numeric(8,2)")]
        public decimal Prix { get; set; }

        [InverseProperty("LesActiviteCartes")]
        public virtual ICollection<Reservation>? LesReservations { get; set; }

        [InverseProperty("LesActiviteCartes")]
        public virtual ICollection<Resort>? LesResorts { get; set; }

        [InverseProperty("LesActiviteCartes")]
        public virtual TypeActivite? LeTypeActivite { get; set; }
    }
}
