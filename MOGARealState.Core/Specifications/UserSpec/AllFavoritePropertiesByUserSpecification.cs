using MOGARealState.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Core.Specifications.UserSpec
{
    public class AllFavoritePropertiesByUserSpecification : BaseSpecification<FavoriteUserProperties>
    {
        public AllFavoritePropertiesByUserSpecification(string userId)
            :base (f=> f.AppUserId == userId)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(f => f.Property);

        }
    }
}
