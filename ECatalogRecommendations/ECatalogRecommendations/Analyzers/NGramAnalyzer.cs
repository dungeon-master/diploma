using System;
using System.Collections.Generic;
using System.Linq;
using ECatalogRecommendations.Models;

namespace ECatalogRecommendations.Analyzers
{
    public static class NGramAnalyzer
    {
        private const double MinimumSimilarity = 0.3;

        public static int N = 2;
        public static int M = 2;

        public static bool IsSimilar(SearchRequest request1, SearchRequest request2)
        {
            List<string> words1 = new List<string>();           
            List<string> words2 = new List<string>();
            if (!request1.IsInitialized || !request2.IsInitialized)
            {
                return false;
            }
            
            ParseRequest(request1, ref words1);
            ParseRequest(request2, ref words2);

            if (words1.SequenceEqual(words2))
            {
                return true;
            }

            List<string> ngrams1 = new List<string>();
            List<string> ngrams2 = new List<string>();

            GetAllNGrams(words1, ref ngrams1, N);
            GetAllNGrams(words2, ref ngrams2, N);

            double similarity = GetSimilarity(ngrams1, ngrams2);
            return similarity > MinimumSimilarity;
        }

        public static double GetDistance(SearchRequest request, ExternalCatalogBook book)
        {
            double distance = 0;

            List<string> requestTitle = new List<string>();
            List<string> requestAuthor = new List<string>();
            List<string> requestSubject = new List<string>();
            List<string> requestKeywords = new List<string>();
            AddWordsIfPresent(request, SearchFields.Title, ref requestTitle);
            AddWordsIfPresent(request, SearchFields.Responsible, ref requestAuthor);
            AddWordsIfPresent(request, SearchFields.SubjectText, ref requestSubject);
            AddWordsIfPresent(request, SearchFields.Keywords, ref requestKeywords);

            if (requestTitle.Count > 0 && requestSubject.Count > 0)
            {
                requestKeywords.AddRange(requestSubject);
            }
            else if (requestSubject.Count > 0)
            {
                requestTitle.AddRange(requestSubject);
            }
            requestSubject.Clear();
            if (requestTitle.Count == 0)
            {
                requestTitle.AddRange(requestKeywords);
                requestKeywords.Clear();
            }

            string title = book.Title;
            if (book.HasKeywords() && requestKeywords.Count > 0)
            {
                distance += Math.Pow(1.0 - GetSimilarityForFields(requestKeywords, book.Keywords), 2);
            }
            else if (requestKeywords.Count > 0)
            {
                requestTitle.AddRange(requestKeywords);
                requestKeywords.Clear();
            }

            if (book.HasAuthor() && requestAuthor.Count > 0)
            {
                distance += Math.Pow(1.0 - GetSimilarityForFields(requestAuthor, book.Author), 2);
            }
            else if (requestAuthor.Count > 0)
            {
                requestTitle.AddRange(requestAuthor);
                requestAuthor.Clear();
            }
            else if (book.HasAuthor())
            {
                title += " " + book.Author;
            }

            if (requestTitle.Count > 0)
            {
                distance += Math.Pow(1.0 - GetSimilarityForFields(requestTitle, title), 2);
            }

            if (distance == 0)
            {
                return 0;
            }

            return Math.Sqrt(distance);
        }

        private static double GetSimilarityForFields(List<string> requestField, string bookField)
        {
            List<string> bookWords = new List<string>();
            AddWords(bookField, ref bookWords);
            List<string> ngrams1 = new List<string>();
            List<string> ngrams2 = new List<string>();
            GetAllNGrams(requestField, ref ngrams1, M);
            GetAllNGrams(bookWords, ref ngrams2, M);
            return GetSimilarity(ngrams1, ngrams2);
        }

        private static double GetSimilarity(List<string> ngrams1, List<string> ngrams2)
        {
            int intersection = 0;
            int union = ngrams1.Count + ngrams2.Count;
            for (int i = 0; i < ngrams1.Count; i++)
            {
                string ngram1 = ngrams1[i];
                for (int j = 0; j < ngrams2.Count; j++)
                {
                    string ngram2 = ngrams2[j];
                    if (ngram1.Equals(ngram2))
                    {
                        intersection++;
                        ngrams2.RemoveAt(j);
                        break;
                    }
                }
            }
            return (2.0 * intersection) / union;
        }

        private static void ParseRequest(SearchRequest request, ref List<string> words)
        {
            foreach (var field in SearchFields.GetSearchFields())
            {
                AddWordsIfPresent(request, field, ref words);
            }
        }

        private static void AddWordsIfPresent(SearchRequest request, string key, ref List<string> words)
        {
            string s = "";
            if (request.GetParameter(key, out s))
            {
                AddWords(s, ref words);
            }
        }

        private static void AddWords(string s, ref List<string> words)
        {
            foreach (var word in GetWords(s.ToLower()))
            {
                if (!words.Contains(word))
                {
                    words.Add(word);
                }
            }
        }

        private static IEnumerable<string> GetWords(string s)
        {
            return s.Split(new[] { ' ', ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static void GetAllNGrams(List<string> words, ref List<string> ngrams, int n)
        {
            foreach (var word in words)
            {
                List<string> tr = GetNGrams(word, n);
                if (tr != null)
                {
                    ngrams.AddRange(tr);
                }
            }
        }

        private static List<string> GetNGrams(string s, int n)
        {
            if (s.Length < n)
            {
                return null;
            }
            List<string> result = new List<string>();
            for (int i = 0; i <= s.Length - n; i++)
            {
                result.Add(s.Substring(i, n));
            }
            return result;
        }
    }
}
