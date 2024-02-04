using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_cartebanquaire_cba")]
public class CarteBanquaire : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("cba_id")]
    public int CarteBanquaireId { get; set; }

    [Column("cli_clientid")]
    public int ClientId { get; set; }

    [Column("cba_numerocarte")]
    [StringLength(255)]
    public string NumeroCarte { get; set; }

    [Column("cba_dateexpiration", TypeName = "date")]
    public DateTime DateExpiration { get; set; }

    [Column("cba_cryptogramme")]
    [StringLength(255)]
    public string Cryptogramme { get; set; }

    [InverseProperty("LesCarteBanquaires")]
    [ForeignKey("ClientId")]
    public virtual Client? LeClient { get; set; }
}
