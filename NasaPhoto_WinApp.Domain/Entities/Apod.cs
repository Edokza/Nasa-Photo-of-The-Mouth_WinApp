using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaPhoto_WinApp.Domain.Entities
{
    public class Apod
    {
        public DateTime Date { get; private set; }
        public string Title { get; private set; }
        public string Explanation { get; private set; }
        public string Url { get; private set; }
        public string HdUrl { get; private set; }
        public string Media_Type { get; private set; }
        public string? Copyright { get; private set; }
        public string ServiceVersion { get; private set; }

        public Apod(
            DateTime date,
            string title,
            string explanation,
            string url,
            string hdUrl,
            string media_Type,
            string? copyright,
            string serviceVersion)
        {
            Date = date;
            Title = title;
            Explanation = explanation;
            Url = url;
            HdUrl = hdUrl;
            Media_Type = media_Type;
            Copyright = copyright;
            ServiceVersion = serviceVersion;
        }
    }
}
