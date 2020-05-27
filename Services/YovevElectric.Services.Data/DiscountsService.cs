using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YovevElectric.Data.Common.Repositories;
using YovevElectric.Data.Models;
using YovevElectric.Web.ViewModels.Discounts;

namespace YovevElectric.Services.Data
{
    public class DiscountsService : IDiscountsService
    {
        private readonly IDeletableEntityRepository<Discount> discountsRepository;

        public DiscountsService(IDeletableEntityRepository<Discount> discountsRepository)
        {
            this.discountsRepository = discountsRepository;
        }

        public async Task AddNewDiscountAsync(string percents, decimal overPrice)
        {
            var newDiscount = new Discount
            {
                OverPrice = overPrice,
                Percents = double.Parse(percents, CultureInfo.InvariantCulture),
            };

            await this.discountsRepository.AddAsync(newDiscount);
            await this.discountsRepository.SaveChangesAsync();
        }

        public async Task DeleteDiscountByIdAsync(int id)
        {
            var discount = await this.discountsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.discountsRepository.HardDelete(discount);
            await this.discountsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Discount>> GetDiscountsAsync()
            => await this.discountsRepository.All().OrderByDescending(x => x.CreatedOn).ToListAsync();

        public async Task<ApplyDiscountModel> ApplyDiscountIfNeedAsync(decimal totalPrice)
        {
            var discount = await this.discountsRepository.All().OrderByDescending(x => x.OverPrice).FirstOrDefaultAsync(x => x.OverPrice <= totalPrice);
            if (discount == null)
            {
                return null;
            }

            var output = new ApplyDiscountModel
            {
                Percent = discount.Percents,
                PriceWithDiscount = totalPrice * (100 - (decimal)discount.Percents) / 100,
                OverPrice = discount.OverPrice,
            };

            return output;
        }
    }
}
