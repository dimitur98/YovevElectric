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

        public async Task<int> GetProductsCount(string category = null, string subCategory = null, string title = null)
        {
            if (category != null)
            {
                return await this.productsRepository.All().Where(x => x.Category == category).CountAsync();
            }
            else if (subCategory != null)
            {
                return await this.productsRepository.All().Where(x => x.SubCategory == subCategory).CountAsync();
            }
            else if (title != null)
            {
                title = title.ToLower().Replace(" ", string.Empty);
                return await this.productsRepository.All().Where(x => x.Title.ToLower().Replace(" ", string.Empty).Contains(title)).CountAsync();
            }
            else
            {
                return await this.productsRepository.All().CountAsync();
            }
        }

        public async Task<ICollection<Product>> GetAllProductsAsync(int skip = 0, string category = null, string subCategory = null, string title = null, string orderBy = null)
        {
            var products = this.productsRepository.All();

            switch (orderBy)
            {
                case "Име а-я": products = products.OrderBy(x => x.Title).ThenByDescending(x => x.CreatedOn); break;
                case "Име я-а": products = products.OrderByDescending(x => x.Title).ThenByDescending(x => x.CreatedOn); break;
                case "Ценa: най-висока": products = products.OrderByDescending(x => x.Price).ThenByDescending(x => x.CreatedOn); break;
                case "Ценa: най-ниска": products = products.OrderBy(x => x.Price).ThenByDescending(x => x.CreatedOn); break;
                default:
                    products = products.OrderByDescending(x => x.CreatedOn);
                    break;
            }

            var result = new List<Product>();
            if (subCategory != null)
            {
                result = await products.Where(x => x.SubCategory == subCategory).Skip(skip).Take(GlobalConstants.ItemsPerPage).ToListAsync();
            }
            else if (category != null)
            {
                result = await products.Where(x => x.Category == category).Skip(skip).Take(GlobalConstants.ItemsPerPage).ToListAsync();
            }
            else if (title != null)
            {
                title = title.ToLower().Replace(" ", string.Empty);
                result = await products.Where(x => x.Title.ToLower().Replace(" ", string.Empty).Contains(title)).Skip(skip).Take(GlobalConstants.ItemsPerPage).ToListAsync();
            }
            else
            {
                result = await products.Skip(skip).Take(GlobalConstants.ItemsPerPage).ToListAsync();
            }

            return result;
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
                SubCategory = input.SubCategory,
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
            product.SubCategory = input.SubCategory;
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
