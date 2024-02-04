using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiClubMed.Models.Repository;

namespace ApiClubMed.Models.EntityFramework
{
    [Table("t_e_typeclub_tcl")]
    public class TypeClub : IEntity, IComparable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tcl_id")]
        public int TypeClubId { get; set; }

        [Column("tcl_libelle")]
        [StringLength(255)]
        public string Libelle { get; set; }

        [InverseProperty("LesTypeClubs")]
        public virtual ICollection<Resort>? LesResorts { get; set; } = null!;

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            TypeClub autreTypeClub = obj as TypeClub;
            if (autreTypeClub != null)
                return this.TypeClubId.CompareTo(autreTypeClub.TypeClubId);
            else
                throw new ArgumentException("Object is not a TypeClub");
        }
    }
}
