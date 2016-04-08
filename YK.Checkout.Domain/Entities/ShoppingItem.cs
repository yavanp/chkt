
namespace YK.Checkout.Domain.Entities
{
    public class ShoppingItem
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        //
        // for debugging purposes
        //
        //public override string ToString()
        //{
        //    return string.Format("Name: {0}, Quantity: {1}", this.Name, this.Quantity);
        //}
    }
}
