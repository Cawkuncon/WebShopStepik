using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {

        public string Index(string id)
        {
            var listProducts = new List<Product>()
            {
                new Product(1, 11, "Name1", "Descr1"),
                new Product(2, 22, "Name2", "Descr2"),
                new Product(3, 33, "Name3", "Descr3"),
            };
            if (int.TryParse(id, out var productId))
            {
                try
                {
                    var result = listProducts.Where(x => x.Id == productId).Single();
                    return result.ToString();
                }
                catch (Exception eex)
                {
                    return $"{eex.Message}";
                }
            }
            else
            {
                return $"Неверный формат Id";
            }
            return string.Empty;
        }
    }
}
