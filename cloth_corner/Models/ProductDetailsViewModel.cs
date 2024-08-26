namespace cloth_corner.Models
{
    public class ProductDetailsViewModel
    {
        public required Products Product { get; set; }
        public required List<Products> OtherProducts { get; set; }
    }
}
