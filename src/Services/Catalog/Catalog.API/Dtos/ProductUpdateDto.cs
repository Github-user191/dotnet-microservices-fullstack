namespace Catalog.API.Dtos {
    public class ProductUpdateDto {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }

        public override string ToString() {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Category)}: {Category}, {nameof(Summary)}: {Summary}, {nameof(Description)}: {Description}, {nameof(ImageFile)}: {ImageFile}, {nameof(Price)}: {Price}";
        }
    }
}
