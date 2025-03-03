using MOGARealState.Core.DTOs.Requests;
using MOGARealState.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Specifications.PropertySpec
{
    public class PropertyWithPaginationSpecification : BaseSpecification<Property>
    {
        public PropertyWithPaginationSpecification(PaginationDto paginationDto)
        {

            AddIncludes();

            ApplyOrderByDescending(b => b.Id);

            var pageIndexHelper = 0;

            if ((paginationDto.PageIndex - 1) < 0)
            {
                pageIndexHelper = 0;
            }
            else
            {
                pageIndexHelper = paginationDto.PageIndex - 1;
            }

            ApplyPagination(pageIndexHelper * paginationDto.PageSize, paginationDto.PageSize);
        }

        public PropertyWithPaginationSpecification()
        {
            
        }

        private void AddIncludes()
        {
            Includes.Add(p => p.Agent);
        }
    }
}
