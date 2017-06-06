using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using ECatalogRecommendations.Analyzers;
using ECatalogRecommendations.DbModels;
using ECatalogRecommendations.Enums;

namespace ECatalogRecommendations.Proxy
{
    public static class CatalogProxy
    {
        public static int Maximum = 0;

        public static void QueryLog(ref IActionAnalyzer actionAnalyzer, int reportValue, 
            ref BackgroundWorker worker, ref DoWorkEventArgs e)
        {
            try
            {
                using (var db = new LibraryLogModel())
                {
                    int count = 0;
                    var table = db.FrontOfficeAction.AsNoTracking()
                        .OrderBy(s => s.FrontOfficeSessionId)
                        .ThenBy(d => d.ActionDateTime);
                    if (Maximum > 0)
                    {
                        table = (IOrderedQueryable<DbEntities.FrontOfficeAction>) table.Take(Maximum);
                    }
                    foreach (var row in table)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }
                        count++;
                        actionAnalyzer.AnalyzeAction(row);
                        if (count % reportValue == 0)
                        {
                            worker.ReportProgress(count, BackgroundWorkerState.ActionAnalyzerReportProgress);
                        }
                    }
                }
            }
            catch (Exception ignore)
            {

            }
        }

        public static int GetLogSize()
        {
            int size;
            using (var db = new LibraryLogModel())
            {
                size = db.FrontOfficeAction.Count();
            }
            if (Maximum > 0 && Maximum < size)
            {
                size = Maximum;
            }
            return size;
        }

        public static bool IsPresentInCatalog(string title)
        {
            bool result;
            using (var db = new SearchArrayModel())
            {
                int count = db.description_title.Count(item => item.title.Equals(title));
                result = count > 0;
            }
            return result;
        }

        public static void ParseSearchRequest(string query, out bool isInitialized, ref Dictionary<string, string> parameters)
        {
            isInitialized = false;
            if (query != null && query.StartsWith("<query>"))
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(query);
                XmlNode root = xml.FirstChild;
                if (root.HasChildNodes)
                {
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        XmlNode node = root.ChildNodes[i];
                        string name = node.Name;
                        if (name != "RecordSource")
                        {
                            string value = node.InnerText;
                            parameters.Add(name, value);
                        }
                    }
                    isInitialized = true;
                }
            }
        }
    }
}
