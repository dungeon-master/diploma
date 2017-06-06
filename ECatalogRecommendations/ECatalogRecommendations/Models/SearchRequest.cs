using System.Collections.Generic;
using System.Linq;
using ECatalogRecommendations.Proxy;

namespace ECatalogRecommendations.Models
{
    public class SearchRequest
    {
        public Dictionary<string, string> Parameters = new Dictionary<string, string>();
        public bool IsInitialized;
        public string Session;

        public SearchRequest(string query, string session)
        {
            Session = session;
            CatalogProxy.ParseSearchRequest(query, out IsInitialized, ref Parameters);
        }

        public SearchRequest(SearchRequest request)
        {
            Parameters = new Dictionary<string, string>(request.Parameters);
            Session = request.Session;
            IsInitialized = request.IsInitialized;
        }

        public bool GetParameter(string key, out string result)
        {
            if (Parameters.TryGetValue(key, out result))
            {
                result = result.Trim();
                if (result.Length >= 3)
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
            foreach (var field in SearchFields.GetValidSearchFields())
            {
                string s = "";
                if (GetParameter(field, out s))
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            string[] searchFields = SearchFields.GetSearchFields();
            string result = "";
            foreach (var parameter in Parameters)
            {
                if (searchFields.Contains(parameter.Key))
                {
                    result += parameter.Key + ": " + parameter.Value + " ";
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            return Session.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool result = true;
            SearchRequest request = obj as SearchRequest;
            if (request == null)
            {
                result = false;
            }
            else
            {
                foreach (var parameter in request.Parameters)
                {
                    if (!Parameters.ContainsKey(parameter.Key) || !Parameters[parameter.Key].Equals(parameter.Value))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
