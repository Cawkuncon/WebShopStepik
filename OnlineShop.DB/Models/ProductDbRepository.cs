

using Microsoft.EntityFrameworkCore;

namespace OnlineShop.DB.Models
{
    public class ProductDbRepository : IProductRepository
    {
        private readonly DataBaseContext dataBaseContext;
        public ProductDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await dataBaseContext.Products.ToListAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var prod = await dataBaseContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            dataBaseContext.Products.Remove(prod);
            await dataBaseContext.SaveChangesAsync();
        }

        public async Task AddProdAsync(Product prod)
        {
            await dataBaseContext.Products.AddAsync(prod);
            await dataBaseContext.SaveChangesAsync();
        }

        public async Task UpdateProdAsync(Product prod)
        {
            var product = await dataBaseContext.Products.FirstOrDefaultAsync(x => x.Id == prod.Id);
            product.Name = prod.Name;
            product.Description = prod.Description;
            product.Cost = prod.Cost;
            await dataBaseContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(Guid id) => await dataBaseContext.Products.FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateProdImageAsync(Guid id, string path)
        {
            var prod = await this.GetProductAsync(id);
            var image = new Image()
            {
                Product = prod,
                Path = path
            };
            await dataBaseContext.Images.AddAsync(image);
            await dataBaseContext.SaveChangesAsync();
        }
        public async Task DeleteImageAsync(Guid id, string path)
        {
            var image = await dataBaseContext.Images.FirstOrDefaultAsync(img => img.Path == path && img.Product.Id == id);
            dataBaseContext.Images.Remove(image);
            await dataBaseContext.SaveChangesAsync();
        }

        public async Task<List<Image>> GetProductImagesAsync(Guid id) => await dataBaseContext.Images.Where(img => img.Product.Id == id).ToListAsync();

        public List<Image> GetProductImages(Guid id)
        {
            return dataBaseContext.Images.Where(img => img.Product.Id == id).ToList();
        }
    }

    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();
        public Task<Product> GetProductAsync(Guid id);
        public Task DeleteAsync(Guid id);
        public Task AddProdAsync(Product prod);
        public Task UpdateProdAsync(Product prod);
        public Task UpdateProdImageAsync(Guid id, string path);
        public Task DeleteImageAsync(Guid id, string path);
        public Task<List<Image>> GetProductImagesAsync(Guid id);
        public List<Image> GetProductImages(Guid id);

    }
}
