using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECatalogRecommendations.DbEntities
{
    public partial class description_title
    {
        [Key]
        public int id { get; set; }

        public int description_id { get; set; }

        public int frequency { get; set; }

        public string title { get; set; }
    }
}
