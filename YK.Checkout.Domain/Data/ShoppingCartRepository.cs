using System.Collections.Generic;
using System.Linq;
using YK.Checkout.Domain.Entities;

namespace YK.Checkout.Domain.Data
{
    public class ShoppingCartRepository : IRepository<ShoppingItem>
    {
        private readonly IDictionary<string, ShoppingItem> _shoppingCart;

        public ShoppingCartRepository(IDictionary<string, ShoppingItem> shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public void Add(string key, ShoppingItem entity)
        {
            _shoppingCart.Add(key, entity);
        }

        public void Remove(string key)
        {
            _shoppingCart.Remove(key);
        }

        public ShoppingItem Get(string key)
        {
            return (_shoppingCart.FirstOrDefault(x => x.Key.Equals(key))).Value;
        }

        public IDictionary<string, ShoppingItem> GetAll()
        {
            return _shoppingCart;
        }
    }
}
