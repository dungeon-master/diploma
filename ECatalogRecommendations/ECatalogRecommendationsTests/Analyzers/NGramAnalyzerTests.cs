using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECatalogRecommendations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECatalogRecommendations.Analyzers.Tests
{
    [TestClass()]
    public class NGramAnalyzerTests
    {
        [TestMethod()]
        public void IsSimilar_Identical()
        {
            string query = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Защита информации</Title></query>";
            SearchRequest request1 = new SearchRequest(query, "s1");
            SearchRequest request2 = new SearchRequest(query, "s2");
            Assert.IsTrue(NGramAnalyzer.IsSimilar(request1, request2));
        }

        [TestMethod()]
        public void IsSimilar_SomeDiffChars()
        {
            string query1 = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Защита информации</Title></query>";
            string query2 = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Защита формации</Title></query>";
            SearchRequest request1 = new SearchRequest(query1, "s1");
            SearchRequest request2 = new SearchRequest(query2, "s2");
            Assert.IsTrue(NGramAnalyzer.IsSimilar(request1, request2));
        }

        [TestMethod()]
        public void IsSimilar_Same()
        {
            string query1 = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Защита информации</Title></query>";
            string query2 = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Информационная защита</Title></query>";
            SearchRequest request1 = new SearchRequest(query1, "s1");
            SearchRequest request2 = new SearchRequest(query2, "s2");
            Assert.IsTrue(NGramAnalyzer.IsSimilar(request1, request2));
        }

        [TestMethod()]
        public void IsSimilar_Different()
        {
            string query1 = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Защита информации</Title></query>";
            string query2 = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Методы верификации</Title></query>";
            SearchRequest request1 = new SearchRequest(query1, "s1");
            SearchRequest request2 = new SearchRequest(query2, "s2");
            Assert.IsFalse(NGramAnalyzer.IsSimilar(request1, request2));
        }

        [TestMethod()]
        public void GetDistance_Identical()
        {
            string query = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Защита информации</Title></query>";
            SearchRequest request = new SearchRequest(query, "s1");
            ExternalCatalogBook book = new ExternalCatalogBook("1", "Защита информации");
            Assert.AreEqual(NGramAnalyzer.GetDistance(request, book), 0);
        }

        [TestMethod()]
        public void GetDistance_SomeDiffChars()
        {
            string query = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Защита формации</Title></query>";
            SearchRequest request = new SearchRequest(query, "s1");
            ExternalCatalogBook book = new ExternalCatalogBook("1", "Защита информации");
            Assert.IsTrue(NGramAnalyzer.GetDistance(request, book) < 0.4);
        }

        [TestMethod()]
        public void GetDistance_Same()
        {
            string query = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Защита информации</Title></query>";
            SearchRequest request = new SearchRequest(query, "s1");
            ExternalCatalogBook book = new ExternalCatalogBook("1", "Информационная защита");
            Assert.IsTrue(NGramAnalyzer.GetDistance(request, book) < 0.4);
        }

        [TestMethod()]
        public void GetDistance_Different()
        {
            string query = "<query><RecordSource ComparisonType=\"one of\">ELibrary</RecordSource><Title ComparisonType=\"FreeTextSearchWithStemming\">Защита информации</Title></query>";
            SearchRequest request = new SearchRequest(query, "s1");
            ExternalCatalogBook book = new ExternalCatalogBook("1", "Методы верификации");
            Assert.IsTrue(NGramAnalyzer.GetDistance(request, book) > 0.4);
        }
    }
}