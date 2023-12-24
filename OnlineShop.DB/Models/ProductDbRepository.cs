

namespace OnlineShop.DB.Models
{
    public class ProductDbRepository : IProductRepository
    {
        private readonly DataBaseContext dataBaseContext;
        public ProductDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        //public List<Product> listProducts = new List<Product>()
        //{
        //    new Product(1, 11, "Name1", "Descr1dfdfdfdfdfdfdfdfdf dfdfdfdfdfdfdfdfdfdfdf dfdfdfdfdfdfdfdfdfdfdf dfdfdfdfdfdfdfd fdfdfdfdfdfdfdfdfdfdfdfd dfdfdfdfdfdfdfdf dfdfdfdfdfdfdfdf"),
        //    new Product(2, 22, "Name2", "Descr2dddddddddddddd"),
        //    new Product(3, 33, "Name3", "Descr3sssssssee eeeeeeeee eeeeeeeeeeee eeeeeeeeeeeeeeeee eeeeeeeeeeee eeeeeeeeee eeeeeeeeeeeeee"),
        //    new Product(4, 44, "Name4", "Descr4ddddddd"),
        //    new Product(5, 55, "Name5", "Descr5eee"),
        //    new Product(6, 66, "Name6", "Descr6e eeeee eeeeee eeeeeeeeee eeeeeeeeeeeeeeee eeeeeeeeeeee eeeeeeeee eee eeeeeeeee eeeeee eee"),
        //};

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
    }

    public interface IProductRepository
    {
        public List<Product> GetAll();
        public void Delete(Guid id);
        public void AddProd(Product prod);
        public void UpdateProd(Product prod);
    }
}
