namespace ECatalogRecommendations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class book_distribution_event
    {
        [Key]
        [StringLength(36)]
        public string book_distribution_event_id { get; set; }

        public DateTime? event_date { get; set; }

        [StringLength(100)]
        public string user_name { get; set; }

        public int? action_code { get; set; }

        public int? reader_id { get; set; }

        public int? item_id { get; set; }

        public int? room_id { get; set; }

        public bool? reader_attended { get; set; }

        [StringLength(36)]
        public string SessionId { get; set; }
    }
}
