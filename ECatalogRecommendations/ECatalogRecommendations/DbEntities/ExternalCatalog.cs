using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECatalogRecommendations.DbEntities
{
    [Table("ExternalCatalog")]
    public partial class ExternalCatalog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [StringLength(256)]
        public string Author { get; set; }

        public string Keywords { get; set; }
    }
}
