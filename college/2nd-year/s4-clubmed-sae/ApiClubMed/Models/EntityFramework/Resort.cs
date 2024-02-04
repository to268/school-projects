using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework;

[Table("t_e_resort_res")]
public class Resort : IEntity, IComparable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("res_id")]
    public int ResortId { get; set; }

    // domaine id
    [ForeignKey("DomaineId")]
    [Column("dms_domaineid")]
    public int? DomaineId { get; set; }

    // localisation id
    [ForeignKey("LocalisationId")]
    [Column("loc_localisationid")]
    public int LocalisationId { get; set; }

    // sous localisation id
    [ForeignKey("SousLocalisationId")]
    [Column("slo_souslocalisationid")]
    public int? SouslocalisationId { get; set; }

    [Column("res_nom")]
    [StringLength(255)]
    public string Nom { get; set; }

    // moyenne avis
    [Column("res_moyenneavis", TypeName = "numeric(8,2)")]
    public decimal? MoyenneAvis { get; set; }

    [Column("res_descriptiongen")]
    public string DescriptionGen { get; set; }

    // lienpdfdocclub
    [Column("res_lienpdfdocclub")]
    public string LienPdfDocClub { get; set; }

    // prixdepart
    [Column("res_prixdepart", TypeName = "numeric(8,2)")]
    public decimal PrixDepart { get; set; }

    // photos
    [InverseProperty("LesResorts")]
    public virtual ICollection<Photo>? LesPhotos { get; set; } = null!;

    // lestypechambres
    [InverseProperty("LeResort")]
    public virtual ICollection<TypeChambre>? LesTypeChambres { get; set; } = null!;

    // les videos
    [InverseProperty("LesResorts")]
    public virtual ICollection<Video>? LesVideos { get; set; } = null!;

    // LesAvis
    [InverseProperty("LeResort")]
    public virtual ICollection<Avis>? LesAvis { get; set; } = null!;

    //lestypeclubs
    [InverseProperty("LesResorts")]
    public virtual ICollection<TypeClub>? LesTypeClubs { get; set; } = null!;

    [InverseProperty("LesResorts")]
    public virtual ICollection<RegroupementClub>? LesRegroupementClubs { get; set; } = null!;

    // les reservations
    [InverseProperty("LeResort")]
    public virtual ICollection<Reservation>? LesReservations { get; set; } = null!;

    [InverseProperty("LesResorts")]
    public virtual ICollection<ActiviteCarte>? LesActiviteCartes { get; set; } = null!;

    [InverseProperty("LesResorts")]
    public virtual ICollection<ActiviteIncluse>? LesActiviteIncluses { get; set; } = null!;

    // les activite enfant cartes
    [InverseProperty("LesResorts")]
    public virtual ICollection<ActiviteEnfantCarte>? LesActiviteEnfantCartes { get; set; } = null!;

    // les activite enfant incluses
    [InverseProperty("LesResorts")]
    public virtual ICollection<ActiviteEnfantIncluse>? LesActiviteEnfantIncluses { get; set; } = null!;

    // Les bars
    [InverseProperty("LeResort")]
    public virtual ICollection<Bar>? LesBars { get; set; } = null!;

    // Les restaurants
    [InverseProperty("LeResort")]
    public virtual ICollection<Restaurant>? LesRestaurants { get; set; } = null!;

    // Domain
    [InverseProperty("LesResorts")]
    public virtual DomaineSkiable? LeDomaineSkiable { get; set; } = null!;

    // Location
    [InverseProperty("LesResorts")]
    public virtual Localisation? LaLocalisation { get; set; } = null!;

    // SubLocation
    [InverseProperty("LesResorts")]
    public virtual SousLocalisation? LaSousLocalisation { get; set; } = null!;

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;

        Resort autreResort = obj as Resort;
        if (autreResort != null)
            return this.ResortId.CompareTo(autreResort.ResortId);
        else
            throw new ArgumentException("Object is not a SousLocalisation");
    }
}
