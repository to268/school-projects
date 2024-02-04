using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_reservation_rsv")]
public class Reservation : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("rsv_id")]
    public int ReservationId { get; set; }

    // client id
    [Column("cli_clientid")]
    public int ClientId { get; set; }

    // transport id
    [Column("tra_transportid")]
    public int TransportId { get; set; }

    // resort id
    [Column("res_resortid")]
    public int ResortId { get; set; }

    [Column("rsv_datedebut", TypeName = "date")]
    public DateTime DateDebut { get; set; } = DateTime.Now;

    [Column("rsv_datefin", TypeName = "date")]
    public DateTime DateFin { get; set; } = DateTime.Now.AddDays(1);

    [Column("rsv_prix")]
    public double Prix { get; set; }

    // confirmation
    [Column("rsv_confirmation")]
    public bool Confirmation { get; set; }

    // validation
    [Column("rsv_validation")]
    public bool Validation { get; set; }

    // resort
    [ForeignKey("ResortId")]
    [InverseProperty("LesReservations")]
    public virtual Resort? LeResort { get; set; }

    [InverseProperty("LesReservations")]
    public virtual ICollection<AutreParticipant>? LesAutreParticipants { get; set; }

    [InverseProperty("LaReservation")]
    public virtual ICollection<Payement>? LesPayements { get; set; }

    [InverseProperty("LesReservations")]
    public virtual ICollection<ActiviteEnfantCarte>? LesActiviteEnfantCartes { get; set; }

    [InverseProperty("LesReservations")]
    public virtual ICollection<ActiviteCarte>? LesActiviteCartes { get; set; }

    // client
    [ForeignKey("ClientId")]
    [InverseProperty("LesReservations")]
    public virtual Client? LeClient { get; set; }

    // transport
    [ForeignKey("TransportId")]
    [InverseProperty("LesReservations")]
    public virtual Transport? LeTransport { get; set; }
}
