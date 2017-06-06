using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECatalogRecommendations.Extensions
{
    [Serializable]
    public struct CustomKeyValuePair<K, V>
    {
        public K Key { get; set; }

        public V Value { get; set; }

        public CustomKeyValuePair(K k, V v)
        {
            Key = k;
            Value = v;
        }

        public KeyValuePair<K, V> ToKeyValuePair()
        {
            KeyValuePair<K, V> result = new KeyValuePair<K, V>(Key, Value);
            return result;
        }
    }
}
