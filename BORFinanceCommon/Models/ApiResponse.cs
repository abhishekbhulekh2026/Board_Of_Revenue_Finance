using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceCommon.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public IEnumerable<string>? Errors { get; set; }

        public string? TraceId { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
