namespace Cart.API.Entities {
    public class ShoppingCart {
        public string UserName {get; set;}
        public List<LineItem> Items {get; set;} = new List<LineItem>();

        public decimal TotalPrice {
            get {
                decimal totalPrice = 0;
                // Iterate line items and calculate total price of cart
                foreach(var item in Items) {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }

        public ShoppingCart() {

        }
        
        public ShoppingCart(string userName) {
            this.UserName = userName;
        }
    }
}
