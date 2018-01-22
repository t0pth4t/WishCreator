using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.AzureAppServices.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WishCreator.Models;

namespace WishCreator.Services
{
    public class SearchService : ISearchService
    {
        private readonly string API_KEY;
        public SearchService(IConfiguration configuration)
        {
            API_KEY = configuration["SearchAPIKey"];
        }
        public SearchResult Search(string searchQuery)
        {
            
            var testdata = File.ReadAllText("testdata.json");
            var result = JObject.Parse(testdata);
            var jsonResult = result.SelectToken("jsonResult").ToString();
            JToken token = JToken.Parse(jsonResult);
            JObject data = JObject.Parse(token.ToString());
            var webPages = data.SelectToken("webPages");
            var resul = JsonConvert.DeserializeObject<RootObject>(webPages.ToString());
            return new SearchResult
            {
                JsonResult = testdata,
            };
            if(string.IsNullOrWhiteSpace(searchQuery))
                return new SearchResult();
            // Construct the URI of the search request
            var uriQuery = "https://api.cognitive.microsoft.com/bing/v7.0/search" + "?q=" + Uri.EscapeDataString(searchQuery);

            // Perform the Web request and get the response
            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = API_KEY;
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            string json = new StreamReader(response?.GetResponseStream()).ReadToEnd();

            // Create result object for return
            var searchResult = new SearchResult
            {
                JsonResult = json,
                RelevantHeaders = new Dictionary<string, string>()
            };

            // Extract Bing HTTP headers
            foreach (String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.RelevantHeaders[header] = response.Headers[header];
            }

            return searchResult;
        }


    }
}