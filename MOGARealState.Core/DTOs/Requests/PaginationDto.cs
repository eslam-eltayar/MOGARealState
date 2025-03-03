using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.DTOs.Requests
{
    public class PaginationDto
    {
        private const int maxPageSize = 12;
        private int pageSize = 12;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public int PageIndex { get; set; } = 1;
    }
}
