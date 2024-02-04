using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_transport_tra")]
public class Transport : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("tra_id")]
    public int TransportId { get; set; }

    [Column("tra_libelle")]
    [StringLength(255)]
    public string Libelle { get; set; }

    [InverseProperty("LeTransport")]
    public virtual ICollection<Reservation>? LesReservations { get; set; }
}
