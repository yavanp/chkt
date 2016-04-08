using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YK.Checkout.Domain.Data;
using YK.Checkout.Exercise.Controllers;
using YK.Checkout.Domain.Entities;

namespace YK.Checkout.Exercise.Tests.Controllers
{
    [TestClass]
    public class ShoppingCartControllerTest
    {
        private readonly ShoppingCartController _controller;
        
        public ShoppingCartControllerTest()
        {
            _controller = new ShoppingCartController(
                new ShoppingCartRepository(new Dictionary<string, ShoppingItem>()));
        }

        #region GetAll

        [TestMethod]
        public void GetAll_Should_Return_Empty_List_Before_Setting_Item_In_Cart()
        {
            // Act
            IQueryable<ShoppingItem> result = _controller.Get(null);

            // Assert
            Assert.IsNotNull(result);

            // Output
            Console.WriteLine(result);
        }

        [TestMethod]
        public void GetAll_Should_Return_2_Items()
        {
            // Arrange
            InitializeShoppingCart();

            // Act
            IQueryable<ShoppingItem> result = _controller.Get(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);

            // Output
            Console.WriteLine(result.Count());
        }

        #endregion

        #region Get

        [TestMethod]
        public void Get_With_Parameter_Should_Return_1_Item_Pepsi_Quantity_2()
        {
            // Arrange
            InitializeShoppingCart();
            var item = new ShoppingItem {Name = "pepsi"};

            // Act
            IQueryable<ShoppingItem> result = _controller.Get(item);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 1);
            Assert.AreEqual(result.First().Name, "Pepsi");
            Assert.IsTrue(result.First().Quantity == 2);

            // Output
            Console.WriteLine(result.Count());
            Console.WriteLine(result.First().Name);
            Console.WriteLine(result.First().Quantity);
        }

        [TestMethod]
        public void Get_Without_Parameter_Should_Return_Full_List()
        {
            // Arrange
            InitializeShoppingCart();
            ShoppingItem item = null;

            // Act
            IQueryable<ShoppingItem> result = _controller.Get(item);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.First().Name, "Pepsi");
            Assert.IsTrue(result.First().Quantity == 2);

            // Output
            Console.WriteLine(result.Count());
            Assert.AreEqual(result.First().Name, "Pepsi");
        }

        #endregion

        #region Put

        [TestMethod]
        public void Put_With_Parameter_Should_Update_Pepsi_Quantity_5()
        {
            // Arrange
            InitializeShoppingCart();
            var item = new ShoppingItem { Name = "pepsi", Quantity = 5};

            // update pepsi
            _controller.Put(item);

            // Act
            IQueryable<ShoppingItem> result = _controller.Get(item);

            // Assert
            Assert.IsTrue(result.First().Quantity == 5);

            // Output
            Console.WriteLine(result.First().Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Put_With_Quantity_0_Should_Throw_ArgumentOutOfRangeException()
        {
            // Arrange
            InitializeShoppingCart();
            var item = new ShoppingItem { Name = "pepsi", Quantity = 0 };

            // update pepsi
            _controller.Put(item);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Put_With_Quantity_Negative_Should_Throw_ArgumentOutOfRangeException()
        {
            // Arrange
            InitializeShoppingCart();
            var item = new ShoppingItem { Name = "pepsi", Quantity = -1 };

            // update pepsi
            _controller.Put(item);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Put_Without_Parameter_Should_Throw_ArgumentNullException()
        {
            // update pepsi
            _controller.Put(null);
        }

        #endregion

        #region Post

        [TestMethod]
        public void Post_With_Parameter_Should_Create_Pepsi_Quantity_1()
        {
            // Arrange
            //InitializeShoppingCart();
            var item = new ShoppingItem { Name = "Pepsi", Quantity = 1 };

            // create pepsi
            _controller.Post(item);

            // Act
            IQueryable<ShoppingItem> result = _controller.Get(item);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 1);
            Assert.AreEqual(result.First().Name, "Pepsi");
            Assert.IsTrue(result.First().Quantity == 1);

            // Output
            Console.WriteLine(result.Count());
            Console.WriteLine(result.First().Name);
            Console.WriteLine(result.First().Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Post_With_Negativ_Parameter_Should_Throw_ArgumentOutOfRangeException()
        {
            // Arrange
            var item = new ShoppingItem { Name = "Pepsi", Quantity = -1 };

            // create pepsi
            _controller.Post(item);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Post_Without_Parameter_Should_Throw_ArgumentNullException()
        {
            // create pepsi
            _controller.Post(null);
        }

        #endregion


        #region Private methods

        private void InitializeShoppingCart()
        {
            var items = new List<ShoppingItem>();
            items.Add(new ShoppingItem
            {
                Name = "Pepsi",
                Quantity = 2
            });
            items.Add(new ShoppingItem
            {
                Name = "Coke",
                Quantity = 3
            });

            foreach (var shoppingItem in items)
            {
                _controller.Post(shoppingItem);
            }

        }
        
        #endregion
    }
}
