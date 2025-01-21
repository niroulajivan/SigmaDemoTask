using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCandidateManagement.Core.Entities.Configuration
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }

        public ApiResponse(int statusCode, string status, T data = default, string error = "")
        {
            StatusCode = statusCode;
            Status = status;
            Data = data;
            Error = error;
        }
    }
}
