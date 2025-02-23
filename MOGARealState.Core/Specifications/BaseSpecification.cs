using LinqKit;
using System.Linq.Expressions;

namespace MOGARealState.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null!;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        // Ordering properties
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public Expression<Func<T, object>>? OrderByDescending { get; set; }

        public BaseSpecification()
        {
            // Criteria = NULL ;
        }

        public BaseSpecification(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression; // P=> P.Id == 10
        }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public void AddCriteria(Expression<Func<T, bool>> additionalCriteria)
        {
            if (Criteria == null)
            {
                Criteria = additionalCriteria;
            }
            else
            {
                Criteria = Criteria.And(additionalCriteria); // Combine criteria using AND logic
            }
        }

        public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }


    }
}
