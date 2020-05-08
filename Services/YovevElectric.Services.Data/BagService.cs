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
    using YovevElectric.Services.Mapping;
    using YovevElectric.Web.ViewModels.Bag;

    public class BagService : IBagService
    {
        private readonly IDeletableEntityRepository<Bag> bagRepository;
        private readonly IDeletableEntityRepository<ProductQuantity> productQuantityRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public BagService(
            IDeletableEntityRepository<Bag> bagRepository,
            IDeletableEntityRepository<ProductQuantity> productQuantityRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.bagRepository = bagRepository;
            this.productQuantityRepository = productQuantityRepository;
            this.userRepository = userRepository;
        }

        public async Task AddProductToBagAsync(string userId, int quantity, string productId)
        {
            var bagId = await this.CreateBagAsync(userId);

            var newAddProduct = new ProductQuantity
            {
                BagId = bagId,
                Quantity = quantity,
                ProductId = productId,
            };

            await this.productQuantityRepository.AddAsync(newAddProduct);
            await this.productQuantityRepository.SaveChangesAsync();
        }

        public async Task MakeOrderAsync(string BagId)
        {
            var Bag = await this.bagRepository.All().FirstOrDefaultAsync(x => x.Id == BagId);
            Bag.Sent = true;

            this.bagRepository.Update(Bag);
            await this.bagRepository.SaveChangesAsync();
        }

        public async Task<ICollection<Bag>> GetAllSentBags()
            => await this.bagRepository.All().Where(x => x.Sent == true).ToListAsync();

        public async Task<Bag> GetBagByIdAsync(string id)
            => await this.bagRepository.All().FirstOrDefaultAsync(x => x.Id == id && x.Sent == false);

        public async Task IsNewBagAsync(string id)
        {
            var bag = await this.GetBagByIdAsync(id);

            if (bag.IsNew)
            {
                bag.IsNew = false;

                this.bagRepository.Update(bag);
                await this.bagRepository.SaveChangesAsync();
            }
        }

        public async Task<ICollection<ProductInBagViewModel>> GetProductsFromBagByIdAsync(string id)
            => await this.productQuantityRepository.All().Where(x => x.BagId == id).To<ProductInBagViewModel>().ToListAsync();

        public async Task ClearBagByIdAsync(string id)
        {
            var productsInBag = await this.productQuantityRepository.All().Where(x => x.BagId == id).ToListAsync();
            foreach (var product in productsInBag)
            {
                this.productQuantityRepository.HardDelete(product);
                await this.productQuantityRepository.SaveChangesAsync();
            }
        }

        public async Task<decimal> TotalPriceOfBagAsync(string id)
        {
            var products = await this.GetProductsFromBagByIdAsync(id);
            decimal sum = 0;
            foreach (var product in products)
            {
                sum += product.Quantity * product.ProductPrice;
            }
            return sum;
        }

        public async Task DeleteProductFromBagByIdAsync(int id)
        {
            var product = await this.productQuantityRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.productQuantityRepository.HardDelete(product);
            await this.productQuantityRepository.SaveChangesAsync();
        }

        private async Task<string> CreateBagAsync(string userId)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId);

            if (user.BagId == null)
            {
                var newBag = new Bag
                {
                    Sent = false,
                    IsNew = true,
                };

                await this.bagRepository.AddAsync(newBag);
                await this.bagRepository.SaveChangesAsync();

                user.BagId = newBag.Id;
                this.userRepository.Update(user);
                await this.userRepository.SaveChangesAsync();
            }

            return user.BagId;
        }
       

    }
}
