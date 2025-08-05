using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    // Implementation of PredicateBuilder
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> New<T>(Expression<Func<T, bool>> expr = null)
        {
            return expr ?? (x => true);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            var param = first.Parameters[0];
            var body = Expression.AndAlso(first.Body, Expression.Invoke(second, param));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            var param = first.Parameters[0];
            var body = Expression.OrElse(first.Body, Expression.Invoke(second, param));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
