using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace ShortUrl.Google
{
    public class URLShortener
    {
        const string GOOGLE_API_KEY = "AIzaSyDYJkWGkFQEPmcUqJgCtKvtdaUAA3qNrcc";
        const string BASE_API_URL = @"https://www.googleapis.com/urlshortener/v1/url";

        /// <summary>Create a url shortener from a long url.</summary>
        /// <param name="origin">Orginal long url</param>
        /// <returns>Shortener URL</returns>
        /// <remarks>POST=>INSERT</remarks>
        public string CreateURLShortener(string origin)
        {
            string result = string.Empty;
            //"https://docs.google.com/presentation/d/1YrI7wnDtGrWMBfylwJoLdPaxt5bAgrlGEGGYbvJKsSA/edit#slide=id.p"

            string response = PostWebRequest(origin);
            if (!string.IsNullOrEmpty(response))
            {
                result = response;//[id].value3

                UrlShortenerData obj = JsonConvert.DeserializeObject<UrlShortenerData>(result,
                    new JsonSerializerSettings
                    {
                        Error = (sender, args) =>
                        {
                            ErrorViewModel err = JsonConvert.DeserializeObject<ErrorViewModel>(result);
                            throw new Exception(err.error.message);
                        }
                    });
                if (obj.shortUrl == null)
                {
                    ErrorViewModel err = JsonConvert.DeserializeObject<ErrorViewModel>(result);
                    throw new Exception(err.error.message);
                }
                else
                {
                    result = obj.shortUrl;
                }
            }
            
            //return a new url
            return result;
        }

        //GET  GET
        public void GetURLShortener(string shortenerUrl)
        {
            //get report
        }

        #region Web Request Method
        
        private HttpWebRequest setWebRequest(string url, string method)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Timeout = 10000;
            request.Method = method;
            request.ContentType = "application/json";
            return request;
        }

        private string GetWebRequest()
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = setWebRequest(BASE_API_URL, "GET");
                using (WebResponse wr = request.GetResponse())
                {
                    //在這裡對接收到的頁面內容進行處理
                    using (StreamReader sr = new StreamReader(wr.GetResponseStream()))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }
            catch (WebException wEx)
            {
                using (StreamReader sr = new StreamReader(wEx.Response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        private string PostWebRequest(string longUrl)
        {
            string result = string.Empty;
            try
            {
                string POST_PATTERN = @"{{""longUrl"": ""{0}""}}";
                string param = string.Format(POST_PATTERN, longUrl);
                byte[] bs = Encoding.ASCII.GetBytes(param);

                HttpWebRequest request = setWebRequest(BASE_API_URL , "POST"); //+ "?key=" + GOOGLE_API_KEY
                request.ContentLength = param.Length;
                request.ContentType = "application/json";
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }

                using (WebResponse wr = request.GetResponse())
                {
                    //在這裡對接收到的頁面內容進行處理
                    using (StreamReader sr = new StreamReader(wr.GetResponseStream()))
                    {
                        result = sr.ReadToEnd();
                    }
                    Console.WriteLine(result);
                }
            }
            catch (WebException wEx)
            {
                using (StreamReader sr = new StreamReader(wEx.Response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception )
            {
                
            }
            return result;
        }

        #endregion
    }
}
