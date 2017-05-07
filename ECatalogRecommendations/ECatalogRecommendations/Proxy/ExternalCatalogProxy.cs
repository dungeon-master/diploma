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
        public static List<ExternalCatalogBook> GetBooks(int reportValue,
            ref BackgroundWorker worker, ref DoWorkEventArgs e)
        {
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
                        books.Add(new ExternalCatalogBook(row.Id.ToString(), row.Title, row.Author, row.Keywords));
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
                    books.Add(new ExternalCatalogBook(id, title, author, null));
                    if (count % reportValue == 0)
                    {
                        worker.ReportProgress(count, BackgroundWorkerState.ExternalCatalogReportProgress);
                    }
                    count++;
                }
            }
            return books;
        }
    }
}
