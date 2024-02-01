using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class Bask : IBaskRepository
    {
        private List<ProductViewModel> prodCart = new List<ProductViewModel>()
        {

        };

        private List<ProductViewModel> resultProducts = new List<ProductViewModel>()
        {

        };

        public void ClearResultProducts()
        {
            resultProducts.Clear();
        }

        public void AddToCart(ProductViewModel product)
        {
            prodCart.Add(product);
        }

        public List<ProductViewModel> GetCart()
        {
            return prodCart;
        }

        public void AddToResultProducts(ProductViewModel prod)
        {
            resultProducts.Add(prod);
        }

        public List<ProductViewModel> GetResultProducts()
        {
            return resultProducts;
        }

        public void ClearCart()
        {
            prodCart.Clear();
        }

        public void RemoveFromCart(Guid Id)
        {
            var prod = prodCart.Where(x => x.Id == Id).First();
            prodCart.Remove(prod);
        }

        public int GetCount()
        {
            return prodCart.Count();
        }

        public int GetTotalCost()
        {
            return prodCart.Select(x => x.Cost).Sum();
        }
    }

    public interface IBaskRepository
    {
        public void ClearResultProducts();

        public void ClearCart();

        public void AddToCart(ProductViewModel product);
        public List<ProductViewModel> GetCart();
        public void AddToResultProducts(ProductViewModel prod);
        public void RemoveFromCart(Guid Id);
        public List<ProductViewModel> GetResultProducts();
        public int GetCount();

        public int GetTotalCost();
    }
}
