using NasaPhoto_WinApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaPhoto_WinApp.Application.Interfaces
{
    public interface IApodRepository
    {
        Task<List<Apod>> GetApodsAsync(DateTime startDate, DateTime endDate);
    }
}
