using System;
using System.Collections.Generic;

namespace WishCreator.Models
{
    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string displayUrl { get; set; }
        public string snippet { get; set; }
        public DateTime dateLastCrawled { get; set; }
    }

    public class RootObject
    {
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public List<Value> value { get; set; }
    }
}