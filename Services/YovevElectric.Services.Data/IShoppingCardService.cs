namespace YovevElectric.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using YovevElectric.Data.Models;

    public interface IShoppingCardService
    {

        Task IsNewShoppingCardAsync(string id);

        Task<ShoppingCard> GetShoppingCardAsync(string id);

        Task<ICollection<ShoppingCard>> GetAllOrders();

        Task MakeOrderAsync(string shoppingCardId);

        Task AddProductToShoppingCardAsync(string shoppingCardId, int quantity, string productId);
    }
}
