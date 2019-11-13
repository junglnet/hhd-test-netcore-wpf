using Hhb.Common.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hhb.Common.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);

        Task<T> GetByIdAsync(Identificator id, CancellationToken token = default);

        Task<IEnumerable<T>> GetByIdsAsync(Identificator[] ids, CancellationToken token = default);

        Task<Identificator> AddAsync(T item, CancellationToken token = default);

        Task<bool> UpdateAsync(T item, CancellationToken token = default);

        Task<bool> DeleteAsync(Identificator id, CancellationToken token = default);

        Task<bool> IsExistById(Identificator id, CancellationToken token = default);
    }
}
