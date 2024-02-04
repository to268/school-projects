using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_civilite_cvl")]
public class Civilite : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("cvl_id")]
    public int CiviliteId { get; set; }

    [Column("cvl_libelle", TypeName = "char(6)")]
    [StringLength(255)]
    public string Libelle { get; set; }

    // Clients
    [InverseProperty("LaCivilite")]
    public virtual ICollection<Client>? LesClients { get; set; }

    // AutreParticipants
    [InverseProperty("LaCivilite")]
    public virtual ICollection<AutreParticipant>? LesAutreParticipants { get; set; }
}
