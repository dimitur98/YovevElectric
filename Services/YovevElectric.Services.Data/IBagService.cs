namespace YovevElectric.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using YovevElectric.Data.Models;
    using YovevElectric.Web.ViewModels.Bag;

    public interface IBagService
    {

        Task IsNewBagAsync(string id);

        Task<Bag> GetBagByIdAsync(string id);

        Task<ICollection<AllSentBagViewModel>> GetAllSentBags();

        Task MakeOrderAsync(string bagId, MakeOrderInputModel input);

        Task AddProductToBagAsync(string bagId, int quantity, string productId);

        Task<ICollection<ProductInBagViewModel>> GetProductsFromBagByIdAsync(string id);

        Task ClearBagByIdAsync(string id);

        Task<decimal> TotalPriceOfBagAsync(string id);

        Task DeleteProductFromBagByIdAsync(int id);

        Task<Bag> GetSentBagByIdAsync(string id);

        int GetCountOfProductsInBagByIdAsync(string id);
    }
}
