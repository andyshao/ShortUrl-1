using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ShortUrl.Google
{
    public class UrlShortenerData
    {
        public string longUrl { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string shortUrl { get; set; }
        public string kind { get; set; }
    }

    public class ErrorViewModel
    {
        public Error error { get; set; }
    }
    public class Error
    {
        public List<ErrorDetail> errors { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
    public class ErrorDetail
    {
        public string domain { get; set; }
        public string reason { get; set; }
        public string message { get; set; }
        public string extendedHelp { get; set; }
    }
}
