using MOGARealState.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Specifications.PropertySpec
{
    public class AgentPropertiesSpecification : BaseSpecification<Property>
    {
        public AgentPropertiesSpecification(int agentId)
            : base(p => p.AgentId == agentId)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(p => p.Agent);
        }
    }
}
