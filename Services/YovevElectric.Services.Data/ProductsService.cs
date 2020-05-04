namespace YovevElectric.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task<ICollection<Product>> GetAllProductsAsync() => await this.productsRepository.All().ToListAsync();

        public async Task<Product> GetProductByIdAsync(string id)
            => await this.productsRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateProductAsync(CreateProductInputModel input)
        {
            var product = new Product
            {
                ImgPath = input.ImgPath,
                Price = input.Price,
                Title = input.Title,
                Category = input.Category,
                Description = input.Description,
            };
            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task EditProductAsync(EditProductInputModel input)
        {
            var product = await this.productsRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == input.Id);

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
