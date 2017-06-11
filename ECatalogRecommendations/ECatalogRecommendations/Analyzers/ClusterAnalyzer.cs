using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using ECatalogRecommendations.Enums;
using ECatalogRecommendations.Models;
using ECatalogRecommendations.Proxy;

namespace ECatalogRecommendations.Analyzers
{
    public class ClusterAnalyzer : IClusterAnalyzer
    {
        private const double MaximumDistance = 0.4;

        private readonly Dictionary<ExternalCatalogBook, double> _result = new Dictionary<ExternalCatalogBook, double>(); 

        public void Analyze(List<Tuple<SearchRequest, double>> requests, List<ExternalCatalogBook> books, 
            ref BackgroundWorker worker, ref DoWorkEventArgs e)
        {
            int count = 0;
            int reportValue = 10;
            foreach (var request in requests)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                double totalDistance = 0;
                List<Tuple<ExternalCatalogBook, double>> distances = new List<Tuple<ExternalCatalogBook, double>>();

                HashSet<ExternalCatalogBook> set = new HashSet<ExternalCatalogBook>();
                string s;
                if (request.Item1.GetParameter(SearchFields.Title, out s))
                {
                    foreach (var word in GetWords(s))
                    {
                        if (ExternalCatalogProxy.index.ContainsKey(word[0]))
                        {
                            foreach (var book in ExternalCatalogProxy.index[word[0]])
                            {
                                set.Add(book);
                            }
                        }
                    }
                }
                if (request.Item1.GetParameter(SearchFields.Responsible, out s))
                {
                    foreach (var word in GetWords(s))
                    {
                        if (ExternalCatalogProxy.index.ContainsKey(word[0]))
                        {
                            foreach (var book in ExternalCatalogProxy.index[word[0]])
                            {
                                set.Add(book);
                            }
                        }
                    }
                }
                if (request.Item1.GetParameter(SearchFields.Keywords, out s))
                {
                    foreach (var word in GetWords(s))
                    {
                        if (ExternalCatalogProxy.index.ContainsKey(word[0]))
                        {
                            foreach (var book in ExternalCatalogProxy.index[word[0]])
                            {
                                set.Add(book);
                            }
                        }
                    }
                }
                if (request.Item1.GetParameter(SearchFields.SubjectText, out s))
                {
                    foreach (var word in GetWords(s))
                    {
                        if (ExternalCatalogProxy.index.ContainsKey(word[0]))
                        {
                            foreach (var book in ExternalCatalogProxy.index[word[0]])
                            {
                                set.Add(book);
                            }
                        }
                    }
                }

                foreach (var book in set)
                {
                    double distance = NGramAnalyzer.GetDistance(request.Item1, book);
                    if (distance <= MaximumDistance)
                    {
                        totalDistance += distance;
                        distances.Add(new Tuple<ExternalCatalogBook, double>(book, distance));
                    }
                }
                foreach (var distance in distances)
                {
                    double p;
                    if (totalDistance > 0)
                    {
                        p = distance.Item2 / totalDistance;
                    }
                    else
                    {
                        p = 1;
                    }
                    double weight = p * request.Item2;
                    if (_result.ContainsKey(distance.Item1))
                    {
                        _result[distance.Item1] += Math.Round(weight, 3);
                    }
                    else
                    {
                        _result.Add(distance.Item1, Math.Round(weight, 3));
                    }
                }
                count++;
                if (count % reportValue == 0)
                {
                    worker.ReportProgress(count, BackgroundWorkerState.ClusterAnalyzerReportProgress);
                }
            }
        }

        public List<KeyValuePair<ExternalCatalogBook, double>> GetResult()
        {
            return _result.OrderByDescending(pair => pair.Value).ToList();
        }

        private static IEnumerable<string> GetWords(string s)
        {
            return s.Split(new[] { ' ', ',', '-' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
