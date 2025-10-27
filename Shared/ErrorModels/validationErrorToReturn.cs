using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class validationErrorToReturn
    {
        public int StatusCode { get; set; } =(int) HttpStatusCode.BadRequest;
        public String Message { get; set; }="Validation Error";
        public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];
    }
}
