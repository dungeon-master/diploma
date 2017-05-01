using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ECatalogRecommendations
{
    public class SearchRequest
    {
        public Dictionary<string, string> Parameters = new Dictionary<string, string>();
        public bool IsInitialized = false;

        public SearchRequest(string query)
        {
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
                            Parameters.Add(name, value);
                        }
                    }
                    IsInitialized = true;
                }
            }
        }

        public SearchRequest(SearchRequest request)
        {
            Parameters = new Dictionary<string, string>(request.Parameters);
            IsInitialized = request.IsInitialized;
        }

        public bool GetParameter(string key, out string result)
        {
            if (Parameters.TryGetValue(key, out result))
            {
                result = result.Trim();
                if (result.Length >= 2)
                {
                    double number;
                    bool isNumber = double.TryParse(result, out number);
                    if (!isNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsValid()
        {
            if (!IsInitialized)
            {
                return false;
            }
            foreach (var field in SearchFields.GetSearchFields())
            {
                string s = "";
                if (GetParameter(field, out s))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
