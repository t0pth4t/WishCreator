using System.Collections.Generic;

namespace WishCreator.Models
{
    public class SearchResult
    {
        public string JsonResult { get; set; }
        public Dictionary<string, string> RelevantHeaders { get; set; }
    }
}