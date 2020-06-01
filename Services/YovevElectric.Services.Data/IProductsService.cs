namespace YovevElectric.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using YovevElectric.Data.Models;
    using YovevElectric.Web.ViewModels.Product;

    public interface IProductsService
    {
        Task<ICollection<Product>> GetAllProductsAsync(int skip = 0, string category = null, string subCategory = null, string title = null, string orderBy = null);

        Task<Product> GetProductByIdAsync(string id);

        Task<string> CreateProductAsync(CreateProductInputModel input);

        Task EditProductAsync(string id, EditProductInputModel input);

        Task DeleteProductAsync(string id);

        Task<Product> GetProductWithDeletedByIdAsync(string id);

        Task<int> GetProductsCount(string category = null, string subCategory = null, string title = null);
    }
}
