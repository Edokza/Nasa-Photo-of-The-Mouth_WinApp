using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasaPhoto_WinApp.Application.Common
{
    public class ServiceResult<T>
    {
        public bool Success { get; }
        public string? ErrorMessage { get; }
        public T? Data { get; }

        private ServiceResult(bool success, T? data, string? errorMessage)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static ServiceResult<T> Ok(T data)
            => new(true, data, null);

        public static ServiceResult<T> Fail(string error)
            => new(false, default, error);
    }
}
