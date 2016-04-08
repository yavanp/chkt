using System;
using System.Linq;
using System.Web.Http;
using YK.Checkout.Domain.Data;
using YK.Checkout.Domain.Entities;
using YK.Checkout.Domain.Services;

namespace YK.Checkout.Exercise.Controllers
{
    public class ShoppingCartController : ApiController
    {
        private readonly ShoppingCartService _service;

        private readonly IRepository<ShoppingItem> _repo;

        public ShoppingCartController()
            : this(new ShoppingCartRepository(WebApiApplication.DataContext))
        {
                
        }
        //private readonly 
        public ShoppingCartController(IRepository<ShoppingItem> repository)
        {
            _repo = repository;

            _service = new ShoppingCartService(_repo);
        }


        // GET api/shoppingcart/5
        public IQueryable<ShoppingItem> Get(ShoppingItem item)
        {
            var result = item == null ? _service.GetAll() : _service.Get(item.Name);

            return result;
        }
      

        public void Post(ShoppingItem item)
        {
            if (item != null && !string.IsNullOrWhiteSpace(item.Name))
            {
                _service.Add(item.Name, item.Quantity);
            }
            else
            {
                throw new ArgumentNullException("Shopping item's not set.");
            }
        }

        // PUT api/shoppingcart/5
        public void Put(ShoppingItem item)
        {
            if (item != null && !string.IsNullOrWhiteSpace(item.Name))
            {
                _service.Update(item.Name, item.Quantity);
            }
            else
            {
                throw new ArgumentNullException("Shopping item's not set.");
            }
        }

        // DELETE api/shoppingcart/5
        public void Delete(ShoppingItem item)
        {
            if (item != null && !string.IsNullOrWhiteSpace(item.Name))
            {
                _service.Remove(item.Name);
            }
            else
            {
                throw new ArgumentNullException("Shopping item's not set.");
            }
        }

    }
}
