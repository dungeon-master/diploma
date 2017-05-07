using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECatalogRecommendations.DbEntities
{
    [Table("FrontOfficeVisitor")]
    public partial class FrontOfficeVisitor
    {
        [Key]
        [StringLength(36)]
        public string FrontOfficeVisitorId { get; set; }

        public DateTime? FirstAppeared { get; set; }

        [StringLength(20)]
        public string Barcode { get; set; }

        public int? version { get; set; }

        public bool? IsBot { get; set; }
    }
}
