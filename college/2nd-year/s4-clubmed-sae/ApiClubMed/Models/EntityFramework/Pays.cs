using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_pays_pay")]
public class Pays : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("pay_id")]
    public int PaysId { get; set; }

    [Column("pay_nom")]
    [StringLength(255)]
    public string Nom { get; set; }

    // Les clients
    [InverseProperty("LePays")]
    public virtual ICollection<Client>? LesClients { get; set; }
}
