using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_autreparticipant_apt")]
public class AutreParticipant : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("apt_id")]
    public int AutreParticipantId { get; set; }

    // civilite_id
    [Column("cvl_civiliteid")]
    public int CiviliteId { get; set; }

    [Column("apt_prenom")]
    [StringLength(255)]
    public string Prenom { get; set; }

    [Column("apt_nom")]
    [StringLength(255)]
    public string Nom { get; set; }

    [Column("apt_datenaissance", TypeName = "date")]
    public DateTime? DateNaissance { get; set; } = DateTime.Now;

    [InverseProperty("LesAutreParticipants")]
    [ForeignKey("CiviliteId")]
    public virtual Civilite? LaCivilite { get; set; }

    [InverseProperty("LesAutreParticipants")]
    public virtual ICollection<Reservation>? LesReservations { get; set; }
}
