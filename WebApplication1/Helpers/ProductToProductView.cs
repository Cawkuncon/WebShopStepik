using OnlineShop.DB.Models;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class ProductToProductView
    {
        public static List<ProductViewModel> Transform(List<Product> products)
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
    }
}
