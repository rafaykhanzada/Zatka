using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.DTO
{
    public class FilterVM
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string? SearchBy { get; set; }
    }
    public class SearchParams
    {
        public string? Key { get; set; }
        public string? Value   { get; set; }
    }
    public class DateFilterVM
    {
        public DateTime? Start { get; set; }
        public DateTime? End   { get; set; }
    }
}
