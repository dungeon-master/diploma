namespace ECatalogRecommendations
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LibraryLogModel : DbContext
    {
        public LibraryLogModel()
            : base("name=library_log")
        {
        }

        public virtual DbSet<book_distribution_event> book_distribution_event { get; set; }
        public virtual DbSet<book_search_event> book_search_event { get; set; }
        public virtual DbSet<FrontOfficeAction> FrontOfficeAction { get; set; }
        public virtual DbSet<FrontOfficeSession> FrontOfficeSession { get; set; }
        public virtual DbSet<FrontOfficeVisitor> FrontOfficeVisitor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<book_distribution_event>()
                .Property(e => e.book_distribution_event_id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<book_distribution_event>()
                .Property(e => e.SessionId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<book_search_event>()
                .Property(e => e.book_search_event_id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeAction>()
                .Property(e => e.FrontOfficeActionId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeAction>()
                .Property(e => e.FrontOfficeSessionId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeSession>()
                .Property(e => e.FrontOfficeSessionId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeSession>()
                .Property(e => e.IpAddress)
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeSession>()
                .Property(e => e.DomainName)
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeSession>()
                .Property(e => e.FrontOfficeVisitorId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeSession>()
                .Property(e => e.Browser)
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeSession>()
                .Property(e => e.WebBrowser)
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeSession>()
                .Property(e => e.OS)
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeSession>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<FrontOfficeVisitor>()
                .Property(e => e.FrontOfficeVisitorId)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
