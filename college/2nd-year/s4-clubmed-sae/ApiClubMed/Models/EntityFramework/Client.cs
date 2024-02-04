using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_client_cli")]
public class Client : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("cli_id")]
    public int ClientId { get; set; }

    [Column("pay_paysid")]
    public int? PaysId { get; set; }

    [Column("cvl_civiliteid")]
    public int? CiviliteId { get; set; }

    [Column("cli_prenom")]
    [StringLength(255)]
    public string Prenom { get; set; } = null!;

    [Column("cli_nom")]
    [StringLength(255)]
    public string Nom { get; set; } = null!;

    [Column("cli_datenaissance", TypeName = "date")]
    public DateTime? DateNaissance { get; set; }

    [Column("cli_email")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    [EmailAddress]
    public string Email { get; set; }

    [Column("cli_tel")]
    [StringLength(255)]
    public string? Tel { get; set; } = null!;

    [Column("cli_numrue")]
    public int? NumRue { get; set; }

    [Column("cli_nomrue")]
    [StringLength(255)]
    public string? NomRue { get; set; } = null!;

    [Column("cli_codepostal")]
    [StringLength(255)]
    public string? CodePostal { get; set; } = null!;

    [Column("cli_ville")]
    [StringLength(255)]
    public string? Ville { get; set; } = null!;

    [Column("cli_password")]
    [StringLength(255)]
    public string Password { get; set; }

    [Column("cli_derniereconnexion", TypeName = "date")]
    public DateTime? DerniereConnexion { get; set; } = DateTime.Now;

    [Column("cli_tempsrestant")]
    [StringLength(40)]
    public string? TempsRestant { get; set; } = "0 ans 0 mois 0 jours";

    [InverseProperty("LeClient")]
    public virtual ICollection<Reservation>? LesReservations { get; set; }

    [InverseProperty("LeClient")]
    public virtual ICollection<CarteBanquaire>? LesCarteBanquaires { get; set; }

    [InverseProperty("LeClient")]
    public virtual ICollection<Avis>? LesAvis { get; set; }

    [ForeignKey("CiviliteId")]
    [InverseProperty("LesClients")]
    public virtual Civilite? LaCivilite { get; set; }

    [ForeignKey("PaysId")]
    [InverseProperty("LesClients")]
    public virtual Pays? LePays { get; set; }
}
