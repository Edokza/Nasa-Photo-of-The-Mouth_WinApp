using NasaPhoto_WinApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaPhoto_WinApp.Wpf.ViewModels
{
    public class DetailViewModel
    {
        public string Title { get; }
        public DateTime Date { get; }
        public string Explanation { get; }
        public string HdUrl { get; }

        public DetailViewModel(Apod apod)
        {
            Title = apod.Title;
            Date = apod.Date;
            Explanation = apod.Explanation;
            HdUrl = apod.HdUrl;
        }
    }
}
