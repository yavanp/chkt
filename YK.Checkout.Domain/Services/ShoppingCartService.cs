using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using YK.Checkout.Domain.Data;
using YK.Checkout.Domain.Entities;

namespace YK.Checkout.Domain.Services
{
    public class ShoppingCartService
    {
        private readonly Object _thisLock = new Object();

        private readonly IRepository<ShoppingItem> _repo;

        public ShoppingCartService(IRepository<ShoppingItem> repository)
        {
            _repo = repository;
        }

        public void Add(string name, int quantity)
        {
            ValidateInputFields(name, quantity);

            var normalizedName = NormalizeName(name);

            lock (_thisLock)
            {
                if (!_repo.GetAll().Any(x => x.Key.Equals(normalizedName)))
                {
                    _repo.Add(normalizedName, new ShoppingItem
                                {
                                    Name = name,
                                    Quantity = quantity
                                });
                }
            }
        }
        public void Update(string name, int quantity)
        {
            ValidateInputFields(name, quantity);

            var normalizedName = NormalizeName(name);

            lock (_thisLock)
            {
                var item = _repo.Get(normalizedName);

                if (item != null && !String.IsNullOrWhiteSpace(item.Name))
                {
                    var newItem = new ShoppingItem
                    {
                        Name = item.Name,
                        Quantity = quantity
                    };

                    using (var scope = new TransactionScope())
                    {
                        _repo.Remove(normalizedName);

                        _repo.Add(normalizedName, newItem);

                        scope.Complete();
                    }
                }
            }
        }

        public void Remove(string name)
        {
            var normalizedName = NormalizeName(name);

            lock (_thisLock)
            {
                var item = _repo.Get(normalizedName);
                if (item != null && !String.IsNullOrWhiteSpace(item.Name))
                    _repo.Remove(normalizedName);
            }
        }

        public IQueryable<ShoppingItem> GetAll()
        {
            IQueryable<ShoppingItem> resultList = Enumerable.Empty<ShoppingItem>().AsQueryable();

            var result = _repo.GetAll();

            if (result != null && result.Any())
            {
                resultList = result.Values.AsQueryable();
            }

            return resultList;
        }

        public IQueryable<ShoppingItem> Get(string name)
        {
            var returnList = new List<ShoppingItem> {_repo.Get(NormalizeName(name))};

            return returnList.AsQueryable();
        }

        #region private methods

        private string NormalizeName(string name)
        {
            return name.ToLowerInvariant();
        }

        private void ValidateInputFields(string name, int quantity)
        {
            // validation
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(@"Name is not set");

            if (quantity < 1) throw new ArgumentOutOfRangeException(@"Quantity can not be less than 1");
        }

        #endregion
    }
}
