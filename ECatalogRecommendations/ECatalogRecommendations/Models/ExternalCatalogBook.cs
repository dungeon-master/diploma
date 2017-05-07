namespace ECatalogRecommendations.Models
{
    public class ExternalCatalogBook
    {
        public string Id { get; }
        public string Title { get; }
        public string Author { get; }
        public string Keywords { get; }

        public ExternalCatalogBook(string id, string title)
        {
            Id = id;
            Title = title;
            Author = null;
            Keywords = null;
        }

        public ExternalCatalogBook(string id, string title, string author, string keyword)
        {
            Id = id;
            Title = title;
            Author = author;
            Keywords = keyword;
        }

        public bool HasAuthor()
        {
            return !string.IsNullOrEmpty(Author);
        }

        public bool HasKeywords()
        {
            return !string.IsNullOrEmpty(Keywords);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        private bool Equals(ExternalCatalogBook book)
        {
            bool equals = Title.Equals(book.Title);
            equals &= HasAuthor() && book.HasAuthor() || !HasAuthor() && !book.HasAuthor();
            equals &= HasKeywords() && book.HasKeywords() || !HasKeywords() && !book.HasKeywords();
            if (equals && HasAuthor())
            {
                equals &= Author.Equals(book.Author);
            }
            if (equals && HasKeywords())
            {
                equals &= Keywords.Equals(book.Keywords);
            }
            return equals;
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            ExternalCatalogBook book = obj as ExternalCatalogBook;
            if (book != null && (Id.Equals(book.Id) || Equals(book)))
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            return Title + " " + Author;
        }
    }
}
