namespace ECatalogRecommendations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FrontOfficeAction")]
    public partial class FrontOfficeAction
    {
        [Key]
        [StringLength(36)]
        public string FrontOfficeActionId { get; set; }

        [StringLength(36)]
        public string FrontOfficeSessionId { get; set; }

        public DateTime? ActionDateTime { get; set; }

        public int? ActionCode { get; set; }

        [StringLength(250)]
        public string ActionParameter { get; set; }

        [Column(TypeName = "ntext")]
        public string ActionComment { get; set; }

        public int? version { get; set; }

        public int? year { get; set; }

        public int? month { get; set; }

        public int? day { get; set; }

        public int? hour { get; set; }
    }
}
