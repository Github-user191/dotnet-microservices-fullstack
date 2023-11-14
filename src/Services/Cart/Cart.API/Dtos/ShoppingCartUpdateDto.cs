using Cart.API.Entities;

namespace Cart.API.Dtos {
    public class ShoppingCartUpdateDto {
        public string UserName {get; set;}
        public List<LineItem> Items {get; set;} = new List<LineItem>();
        public decimal TotalPrice {get; set;}
    }
}