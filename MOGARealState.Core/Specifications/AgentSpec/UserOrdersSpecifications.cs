using MOGARealState.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Specifications.AgentSpec
{
    public class UserOrdersSpecifications : BaseSpecification<UserOrders>
    {
        public UserOrdersSpecifications(int agentId)
            :base(o=> o.Property.AgentId == agentId)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(o => o.Property);
            Includes.Add(o => o.Property.Agent);
            Includes.Add(o => o.AppUser);
        }
    }
}
