using System.Data.Entity;
using ECatalogRecommendations.DbEntities;

namespace ECatalogRecommendations.DbModels
{
    public partial class ExternalCatalogModel : DbContext
    {
        public ExternalCatalogModel()
            : base("name=ExternalCatalogModel")
        {
        }

        public virtual DbSet<ExternalCatalog> ExternalCatalog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
