namespace YovevElectric.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using YovevElectric.Data.Models;
    using YovevElectric.Web.ViewModels.Discounts;

    public interface IDiscountsService
    {
        Task<ApplyDiscountModel> ApplyDiscountIfNeedAsync(decimal totalPrice);

        Task<IEnumerable<Discount>> GetDiscountsAsync();

        Task DeleteDiscountByIdAsync(int id);

        Task AddNewDiscountAsync(string percents, decimal overPrice);
    }
}
