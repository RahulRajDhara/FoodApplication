using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   public class OperationResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int TotalCount { get; set; }
    }
}
