using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using ECatalogRecommendations.DbModels;
using ECatalogRecommendations.Enums;
using ECatalogRecommendations.Models;

namespace ECatalogRecommendations.Proxy
{
    public static class ExternalCatalogProxy
    {
        public static Dictionary<char, HashSet<ExternalCatalogBook>> index;

        public static List<ExternalCatalogBook> GetBooks(int reportValue,
            ref BackgroundWorker worker, ref DoWorkEventArgs e)
        {
            index = new Dictionary<char, HashSet<ExternalCatalogBook>>();
            List<ExternalCatalogBook> books = new List<ExternalCatalogBook>();
            int count = 0;
            try
            {
                using (var db = new ExternalCatalogModel())
                {
                    var table = db.ExternalCatalog.AsNoTracking();
                    worker.ReportProgress(table.Count(), BackgroundWorkerState.ExternalCatalogInitialize);
                    foreach (var row in table)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                        if (!CatalogProxy.IsPresentInCatalog(row.Title))
                        {
                            ExternalCatalogBook book = new ExternalCatalogBook(row.Id.ToString(), row.Title, row.Author, row.Keywords);
                            books.Add(book);
                            char t = book.Title[0];
                            char a = book.Author[0];
                            if (index.ContainsKey(t))
                            {
                                index[t].Add(book);
                            }
                            else
                            {
                                index[t] = new HashSet<ExternalCatalogBook>();
                                index[t].Add(book);
                            }
                            if (index.ContainsKey(a))
                            {
                                index[a].Add(book);
                            }
                            else
                            {
                                index[a] = new HashSet<ExternalCatalogBook>();
                                index[a].Add(book);
                            }
                        }
                        count++;
                        if (count % reportValue == 0)
                        {
                            worker.ReportProgress(count, BackgroundWorkerState.ExternalCatalogReportProgress);
                        }
                    }
                }
            }
            catch (Exception ignore)
            {
                
            }
            return books;
        }

        public static List<ExternalCatalogBook> GetBooks(string xmlPath, int reportValue,
            ref BackgroundWorker worker, ref DoWorkEventArgs e)
        {
            index = new Dictionary<char, HashSet<ExternalCatalogBook>>();
            List<ExternalCatalogBook> books = new List<ExternalCatalogBook>();
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);
            XmlNode root = xml.LastChild;
            XmlNode shopNode = root.LastChild;
            XmlNode offersNode = shopNode.LastChild;
            int count = 0;
            if (offersNode.HasChildNodes)
            {
                worker.ReportProgress(offersNode.ChildNodes.Count, BackgroundWorkerState.ExternalCatalogInitialize);
                for (int i = 0; i < offersNode.ChildNodes.Count; i++)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    XmlNode offerNode = offersNode.ChildNodes[i];
                    string id = offerNode.Attributes["id"].Value;
                    string title = null, author = null;
                    foreach (XmlNode node in offerNode.ChildNodes)
                    {
                        if (node.Name.Equals("author"))
                        {
                            author = node.InnerText;
                        }
                        if (node.Name.Equals("name"))
                        {
                            title = node.InnerText;
                        }
                        if (title != null && author != null)
                        {
                            break;
                        }
                    }
                    if (isDocumentValid(title, author) && !CatalogProxy.IsPresentInCatalog(title))
                    {
                        ExternalCatalogBook book = new ExternalCatalogBook(id, title, author, null);
                        books.Add(book);
                        foreach (var word in GetWords(book.Title))
                        {
                            char t = word[0];
                            if (index.ContainsKey(t))
                            {
                                index[t].Add(book);
                            }
                            else
                            {
                                index[t] = new HashSet<ExternalCatalogBook>();
                                index[t].Add(book);
                            }
                        }
                        foreach (var word in GetWords(book.Author))
                        {
                            char t = word[0];
                            if (index.ContainsKey(t))
                            {
                                index[t].Add(book);
                            }
                            else
                            {
                                index[t] = new HashSet<ExternalCatalogBook>();
                                index[t].Add(book);
                            }
                        }
                    }
                    if (count % reportValue == 0)
                    {
                        worker.ReportProgress(count, BackgroundWorkerState.ExternalCatalogReportProgress);
                    }
                    count++;
                    if (count == 400)
                    {
                        break;
                    }
                }
            }
            return books;
        }

        private static IEnumerable<string> GetWords(string s)
        {
            return s.Split(new[] { ' ', ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static bool isDocumentValid(string title, string author)
        {
            bool result = false;
            foreach (var ch in title)
            {
                if (char.IsLetter(ch))
                {
                    result = true;
                    break;
                }
            }
            if (result)
            {
                result = false;
                foreach (var ch in author)
                {
                    if (char.IsLetter(ch))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
