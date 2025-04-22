using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int statusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string errorMessage { get; set; } = "Validation Error";

        public IEnumerable<ValidationErrors> Errors { get; set; } = [];

    }
        public class ValidationErrors
        {
            public string Field { get; set; }  = default!;
            public IEnumerable<string> Errors { get; set; } = [];
        }
}
