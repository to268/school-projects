using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_payement_pyt")]
public class Payement : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("pyt_id")]
    public int PayementId { get; set; }

    [Column("rsv_reservationid")]
    [ForeignKey("ReservationId")]
    public int ReservationId { get; set; }

    // montant
    [Column("pyt_montant")]
    public double Montant { get; set; }

    [ForeignKey("ReservationId")]
    [InverseProperty("LesPayements")]
    public virtual Reservation? LaReservation { get; set; }
}
