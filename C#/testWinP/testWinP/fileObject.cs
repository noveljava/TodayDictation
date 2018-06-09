using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fileObjectClass
{
    public class fileObject
    {
        string title;
        string url;
        string year;
        string month;

        public fileObject()
        {
            this.title = "";
            this.url = "";
            this.year = "";
            this.month = "";
        }
        public void setTilte(string title)
        {
            this.title = title;
        }

        public void setUrl(string url)
        {
            this.url = url;
        }

        public void setYear(string year)
        {
            this.year = year;
        }

        public void setMonth(string month)
        {
            this.month = month;
        }

        public string getTitle()
        {
            return this.title;
        }

        public string getUrl()
        {
            return this.url;
        }

        public string getYear()
        {
            return this.year;
        }

        public string getMonth()
        {
            return this.month;
        }
    }
}
