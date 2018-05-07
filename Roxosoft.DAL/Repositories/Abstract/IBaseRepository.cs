namespace Roxosoft.DAL.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Roxosoft.Common;

    public interface IBaseRepository<T>
    {
        Task<Tuple<IList<T>, int>> GetList(PageSortInfo pageSort);

        Task<Tuple<IList<T>, int>> GetList(Expression<Func<T, bool>> predicate, PageSortInfo pageSort);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        Task Create(T model);

        void Update(T model);

        void Delete(T model);

        Task Save();
    }
}
