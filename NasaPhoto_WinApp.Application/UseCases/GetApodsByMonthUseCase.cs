using NasaPhoto_WinApp.Application.Common;
using NasaPhoto_WinApp.Application.Interfaces;
using NasaPhoto_WinApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaPhoto_WinApp.Application.UseCases
{
    public class GetApodsByMonthUseCase
    {
        private readonly IApodRepository _repository;

        public GetApodsByMonthUseCase(IApodRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<List<Apod>>> ExecuteAsync(int year, int month)
        {
            var minDate = new DateTime(1995, 6, 16);
            var maxDate = DateTime.Today;

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            if (startDate < minDate)
                return ServiceResult<List<Apod>>
                    .Fail("NASA APOD available from June 16, 1995.");

            if (startDate > maxDate)
                return ServiceResult<List<Apod>>
                    .Fail("Cannot select future date.");

            if (endDate > DateTime.Today)
            {
                endDate = DateTime.Today;
            }

            var result = await _repository.GetApodsAsync(startDate, endDate);

            return ServiceResult<List<Apod>>.Ok(result);
        }
    }
}
