namespace ECatalogRecommendations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class book_search_event
    {
        [Key]
        [StringLength(36)]
        public string book_search_event_id { get; set; }

        public DateTime? event_date { get; set; }

        [StringLength(15)]
        public string ip_address { get; set; }

        [StringLength(100)]
        public string user_name { get; set; }

        [Column(TypeName = "ntext")]
        public string query { get; set; }
    }
}
