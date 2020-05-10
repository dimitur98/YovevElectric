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
        private readonly IDeletableEntityRepository<OrderData> orderDataRepository;

        public BagService(
            IDeletableEntityRepository<Bag> bagRepository,
            IDeletableEntityRepository<ProductQuantity> productQuantityRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<OrderData> orderDataRepository)
        {
            this.bagRepository = bagRepository;
            this.productQuantityRepository = productQuantityRepository;
            this.userRepository = userRepository;
            this.orderDataRepository = orderDataRepository;
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

        public async Task MakeOrderAsync(string bagId, MakeOrderInputModel input)
        {
            var bag = await this.bagRepository.All().FirstOrDefaultAsync(x => x.Id == bagId);
            bag.Sent = true;
            bag.DateOfSent = DateTime.UtcNow;
            var orderData = new OrderData
            {
                Adress = input.Adress,
                Bulstad = input.Bulstad,
                City = input.City,
                FirmName = input.FirmName,
                FirstName = input.FirstName,
                LastName = input.LastName,
                MobileNumber = input.MobileNumber,
                MOL = input.MOL,
                PostCode = input.PostCode,
            };
            await this.orderDataRepository.AddAsync(orderData);
            await this.orderDataRepository.SaveChangesAsync();

            bag.OrderDataId = orderData.Id;

            this.bagRepository.Update(bag);
            await this.bagRepository.SaveChangesAsync();
        }

        public async Task<Bag> GetBagByIdAsync(string id)
            => await this.bagRepository.All().FirstOrDefaultAsync(x => x.Id == id && x.Sent == false);

        public async Task<Bag> GetSentBagByIdAsync(string id)
        {
            var bag = await this.bagRepository.All().FirstOrDefaultAsync(x => x.Id == id && x.Sent == true);
            bag.IsNew = false;

            this.bagRepository.Update(bag);
            await this.bagRepository.SaveChangesAsync();
            return bag;
        }

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

        public async Task<ICollection<AllSentBagViewModel>> GetAllSentBags() =>
            await this.bagRepository.All().Where(x => x.Sent == true).OrderByDescending(x => x.IsNew).ThenByDescending(x => x.DateOfSent).To<AllSentBagViewModel>().ToListAsync();

        public int GetCountOfProductsInBagByIdAsync(string id)
            => this.productQuantityRepository.All().Where(x => x.BagId == id).Count();

        public async Task<int> GetProductsCountInBagAsync(string id)
            => await this.productQuantityRepository.All().Where(x => x.BagId == id).CountAsync();

        private async Task<string> CreateBagAsync(string userId)
        {
            var user = await this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId);
            var bag = await this.bagRepository.All().FirstOrDefaultAsync(x => x.Id == user.BagId && x.Sent == false);
            if (bag == null)
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
