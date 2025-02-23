using MOGARealState.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Specifications.PropertySpec
{
    public class PropertyWithAgentSpecification : BaseSpecification<Property>
    {
        public PropertyWithAgentSpecification()
        {
            AddIncludes();
        }

        public PropertyWithAgentSpecification(int id)
            : base(p => p.Id == id)
        {
            AddIncludes();
        }



        private void AddIncludes()
        {
            Includes.Add(p => p.Agent);
        }
    }

}