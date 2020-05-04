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

    public class ShoppingCardService : IShoppingCardService
    {
        private readonly IDeletableEntityRepository<ShoppingCard> shoppingCardRepository;
        private readonly IDeletableEntityRepository<ProductQuantity> productQuantityRepository;

        public ShoppingCardService(
            IDeletableEntityRepository<ShoppingCard> shoppingCardRepository,
            IDeletableEntityRepository<ProductQuantity> productQuantityRepository)
        {
            this.shoppingCardRepository = shoppingCardRepository;
            this.productQuantityRepository = productQuantityRepository;
        }

        public async Task AddProductToShoppingCardAsync(string shoppingCardId, int quantity, string productId)
        {
            await this.CreateShoppingCardAsync(shoppingCardId);

            var newAddProduct = new ProductQuantity
            {
                ShoppingCardId = shoppingCardId,
                Quantity = quantity,
                ProductId = productId,
            };

            await this.productQuantityRepository.AddAsync(newAddProduct);
            await this.productQuantityRepository.SaveChangesAsync();
        }

        public async Task MakeOrderAsync(string shoppingCardId)
        {
            var shoppingCard = await this.shoppingCardRepository.All().FirstOrDefaultAsync(x => x.Id == shoppingCardId);
            shoppingCard.Sent = true;

            this.shoppingCardRepository.Update(shoppingCard);
            await this.shoppingCardRepository.SaveChangesAsync();
        }

        public async Task<ICollection<ShoppingCard>> GetAllOrders()
            => await this.shoppingCardRepository.All().Where(x => x.Sent == true).ToListAsync();

        public async Task<ShoppingCard> GetShoppingCardAsync(string id)
            => await this.shoppingCardRepository.All().FirstOrDefaultAsync(x => x.Id == id && x.Sent == false);

        public async Task IsNewShoppingCardAsync(string id)
        {
            var shoppingCard = await this.GetShoppingCardAsync(id);

            if (shoppingCard.IsNew)
            {
                shoppingCard.IsNew = false;

                this.shoppingCardRepository.Update(shoppingCard);
                await this.shoppingCardRepository.SaveChangesAsync();
            }
        }

        private async Task CreateShoppingCardAsync(string id)
        {
            var shoppingCard = await this.shoppingCardRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (shoppingCard == null)
            {
                var newShoppingCard = new ShoppingCard
                {
                    Sent = false,
                    IsNew = true,
                };

                await this.shoppingCardRepository.AddAsync(newShoppingCard);
                await this.shoppingCardRepository.SaveChangesAsync();
            }
        }
    }
}
