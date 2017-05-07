using System.Data.Entity;
using ECatalogRecommendations.DbEntities;

namespace ECatalogRecommendations.DbModels
{
    public partial class SearchArrayModel : DbContext
    {
        public SearchArrayModel()
            : base("name=SearchArrayModel")
        {
        }

        public virtual DbSet<description_title> description_title { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
