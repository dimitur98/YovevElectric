namespace YovevElectric.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using YovevElectric.Common;
    using YovevElectric.Data.Common.Repositories;
    using YovevElectric.Data.Models;
    using YovevElectric.Web.ViewModels.Product;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<int> GetProductsCount() => await this.productsRepository.All().CountAsync();

        public async Task<ICollection<Product>> GetAllProductsAsync( int skip = 0)
        {
            var products = await this.productsRepository.All().OrderByDescending(x => x.CreatedOn).Skip(skip).Take(GlobalConstants.ItemsPerPage).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(string id)
            => await this.productsRepository.All().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Product> GetProductWithDeletedByIdAsync(string id)
            => await this.productsRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<string> CreateProductAsync(CreateProductInputModel input)
        {
            var product = new Product
            {
                ImgPath = GlobalConstants.DefaultImgProduct,
                Price = input.Price,
                Title = input.Title,
                Category = input.Category,
                Description = input.Description,
            };
            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            return product.Id;
        }

        public async Task EditProductAsync(string id, EditProductInputModel input)
        {
            var product = await this.productsRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);

            product.Price = input.Price;
            product.Title = input.Title;
            product.Category = input.Category;
            product.Description = input.Description;

            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(string id)
        {
            var product = await this.GetProductByIdAsync(id);
            product.IsDeleted = true;

            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task UnDeleteProductAsync(string id)
        {
            var product = await this.GetProductByIdAsync(id);
            product.IsDeleted = false;

            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();
        }
    }
}
