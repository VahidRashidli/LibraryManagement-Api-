using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModels.Models.Base;

namespace Repository.Repository.Abstarction
{
   public interface IRepository<T>where T:class,IEntity
    {
        public Task<T> GetAsync(int id);
        public Task<IList<T>> GetAllAsync();
        public Task<bool> AddAsync(T item);
        public Task<bool> AddRangeAsync(IList<T> items);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(T item);
    }
}
