namespace Roxosoft.Common.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class IQueryableExtension
    {
        private const string Asc = "OrderByProperty";
        private const string Desc = "OrderByPropertyDescending";

        public static IQueryable<TModel> Sort<TModel>(this IQueryable<TModel> q, PageSortInfo pageSort)
        {
            string orderDist = pageSort.SortOrder != null && pageSort.SortOrder.Value == SortOrderEnum.Asc ? Asc : Desc;
            Type entityType = typeof(TModel);
            PropertyInfo p = entityType.GetProperty(pageSort.SortField);

            MethodInfo m = typeof(IQueryableExtension).GetMethod(orderDist).MakeGenericMethod(entityType, p.PropertyType);
            return (IQueryable<TModel>)m.Invoke(null, new object[] { q, p });
        }

        public static IQueryable<TModel> OrderByPropertyDescending<TModel, TRet>(IQueryable<TModel> q, PropertyInfo p)
        {
            ParameterExpression pe = Expression.Parameter(typeof(TModel));
            Expression se = Expression.Convert(Expression.Property(pe, p), typeof(TRet));
            return q.OrderByDescending(Expression.Lambda<Func<TModel, TRet>>(se, pe));
        }

        public static IQueryable<TModel> OrderByProperty<TModel, TRet>(IQueryable<TModel> q, PropertyInfo p)
        {
            ParameterExpression pe = Expression.Parameter(typeof(TModel));
            Expression se = Expression.Convert(Expression.Property(pe, p), typeof(TRet));
            return q.OrderBy(Expression.Lambda<Func<TModel, TRet>>(se, pe));
        }
    }
}
