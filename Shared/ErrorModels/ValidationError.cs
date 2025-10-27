using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationError
    {
        public String Filed { get; set; }=null!;
        public IEnumerable<String> Errors { get; set; } = [];
    }
}
