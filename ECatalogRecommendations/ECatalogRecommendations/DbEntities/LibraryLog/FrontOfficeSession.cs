namespace ECatalogRecommendations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FrontOfficeSession")]
    public partial class FrontOfficeSession
    {
        [Key]
        [StringLength(36)]
        public string FrontOfficeSessionId { get; set; }

        [StringLength(15)]
        public string IpAddress { get; set; }

        [StringLength(100)]
        public string DomainName { get; set; }

        [StringLength(36)]
        public string FrontOfficeVisitorId { get; set; }

        [StringLength(250)]
        public string Browser { get; set; }

        public DateTime? SessionStarted { get; set; }

        public int? version { get; set; }

        [Column(TypeName = "ntext")]
        public string Referer { get; set; }

        [StringLength(50)]
        public string WebBrowser { get; set; }

        [StringLength(40)]
        public string OS { get; set; }

        [StringLength(40)]
        public string City { get; set; }
    }
}
