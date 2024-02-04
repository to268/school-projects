using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_avis_avi")]
public class Avis : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("avi_id")]
    public int AvisId { get; set; }

    [Column("res_resortid")]
    public int? ResortId { get; set; }

    [Column("cli_clientid")]
    public int? ClientId { get; set; }

    [Column("pht_photoid")]
    public int? PhotoId { get; set; }

    [Column("avi_note")]
    [Range(0, 5)]
    public int Note { get; set; }

    [Column("avi_commentaire")]
    [StringLength(1000)]
    public string Commentaire { get; set; }

    [Column("avi_date", TypeName = "date")]
    public DateTime Date { get; set; }

    [InverseProperty("LesAvis")]
    [ForeignKey("ResortId")]
    public virtual Resort? LeResort { get; set; }

    [InverseProperty("LesAvis")]
    [ForeignKey("ClientId")]
    public virtual Client? LeClient { get; set; }

    // la photo
    [InverseProperty("LesAvis")]
    [ForeignKey("PhotoId")]
    public virtual Photo? LaPhoto { get; set; }
}
