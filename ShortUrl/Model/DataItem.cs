using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShortUrl.Model
{
    public enum EnumType
    {
        Shortener, Expand, List
    }

    public class DataItem
    {
        public DataItem(string longUrl)
        {
            
        }

        public string longUrl
        {
            get;
            set;
        }

        public string shortUrl
        {
            get;
            set;
        }

        public EnumType type
        {
            get;
            set;
        }

        public string errorMessage
        {
            get;
            set;
        }
    }
}
