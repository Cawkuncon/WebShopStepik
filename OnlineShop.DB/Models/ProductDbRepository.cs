

namespace OnlineShop.DB.Models
{
    public class ProductDbRepository : IProductRepository
    {
        private readonly DataBaseContext dataBaseContext;
        public ProductDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }
        public List<Product> GetAll()
        {
            return dataBaseContext.Products.ToList();
        }
        public void Delete(Guid id)
        {
            var prod = dataBaseContext.Products.FirstOrDefault(x => x.Id == id);
            dataBaseContext.Products.Remove(prod);
            dataBaseContext.SaveChanges();
        }

        public void AddProd(Product prod)
        {
            dataBaseContext.Products.Add(prod);
            dataBaseContext.SaveChanges();
        }

        public void UpdateProd(Product prod)
        {
            var product = dataBaseContext.Products.FirstOrDefault(x => x.Id == prod.Id);
            product.Name = prod.Name;
            product.Description = prod.Description;
            product.Cost = prod.Cost;
            dataBaseContext.SaveChanges();
        }

        public Product GetProduct(Guid id)
        {
            return dataBaseContext.Products.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateProdImage(Guid id, string path)
        {
            var prod = this.GetProduct(id);
            var image = new Image()
            {
                Product = prod,
                Path = path
            };
            dataBaseContext.Images.Add(image);
            dataBaseContext.SaveChanges();
        }
        public void DeleteImage(Guid id, string path)
        {
            var image = dataBaseContext.Images.FirstOrDefault(img => img.Path == path && img.Product.Id == id);
            dataBaseContext.Images.Remove(image);
            dataBaseContext.SaveChanges();
        }

        public List<Image> GetProductImages(Guid id)
        {
            return dataBaseContext.Images.Where(img => img.Product.Id == id).ToList();
        }
    }

    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product GetProduct(Guid id);
        public void Delete(Guid id);
        public void AddProd(Product prod);
        public void UpdateProd(Product prod);
        public void UpdateProdImage(Guid id, string path);
        public void DeleteImage(Guid id, string path);
        public List<Image> GetProductImages(Guid id);
    }
}
