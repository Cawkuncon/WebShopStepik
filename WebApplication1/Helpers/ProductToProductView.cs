using OnlineShop.DB.Models;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class ProductToProductView
    {
        public static List<ProductViewModel> TransformList(List<Product> products)
        {
            var listProductView = new List<ProductViewModel>();
            foreach (var prod in products)
            {
                var prodView = new ProductViewModel();
                prodView.Id = prod.Id;
                prodView.Name = prod.Name;
                prodView.Cost = prod.Cost;
                prodView.Description = prod.Description;
                listProductView.Add(prodView);
            }
            return listProductView;
        }
        public static ProductViewModel Transform(Product product)
        {
            var productView = new ProductViewModel();
            productView.Id = product.Id;
            productView.Name = product.Name;
            productView.Cost = product.Cost;
            productView.Description = product.Description;
            return productView;
        }
    }
}
