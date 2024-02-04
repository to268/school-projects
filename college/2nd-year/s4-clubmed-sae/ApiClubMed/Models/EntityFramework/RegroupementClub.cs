using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_regroupementclub_rcl")]
    public class RegroupementClub : IEntity, IComparable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("rcl_id")]
        public int RegroupementClubId { get; set; }

        [Column("rcl_libelle")]
        [StringLength(255)]
        public string Libelle { get; set; }

        [InverseProperty("LesRegroupementClubs")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            RegroupementClub autreRegroupementClub = obj as RegroupementClub;
            if (autreRegroupementClub != null)
                return this.RegroupementClubId.CompareTo(autreRegroupementClub.RegroupementClubId);
            else
                throw new ArgumentException("Object is not a RegroupementClub");
        }
    }
}
