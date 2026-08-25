using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDTO
{
    public class DropdownItemDto<T>
    {
        public T Id { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
    }
}
