using Cart.API.Entities;

namespace Cart.API.Dtos {
    public class ShoppingCartReadDto {
        public string UserName {get; set;}
        public List<LineItem> Items {get; set;} = new List<LineItem>();
        public decimal TotalPrice;
    }
}