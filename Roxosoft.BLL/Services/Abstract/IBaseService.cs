namespace Roxosoft.BLL.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Roxosoft.Common;

    public interface IBaseService<T>
    {
        Task<Tuple<IList<T>, int>> GetList(PageSortInfo pageSort);

        Task<Tuple<IList<T>, int>> GetList(string terms, PageSortInfo pageSort);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        Task Create(T model, Guid? userUid);

        Task Update(T model, Guid? userUid);

        Task Delete(T model, Guid? userUid);
    }
}
