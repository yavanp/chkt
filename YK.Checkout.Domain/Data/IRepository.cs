using System.Collections.Generic;

namespace YK.Checkout.Domain.Data
{
    public interface IRepository<T> where T : class
    {
        void Add(string key, T entity);

        void Remove(string key);

        T Get(string key);

        IDictionary<string, T> GetAll();
    }
}
