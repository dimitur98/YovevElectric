using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YovevElectric.Data.Common.Repositories;
using YovevElectric.Data.Models;
using YovevElectric.Web.ViewModels.Bag;

namespace YovevElectric.Services.Data
{
    public class OrderDataService : IOrderDataService
    {
        private readonly IDeletableEntityRepository<OrderData> orderDataRepository;

        public OrderDataService(IDeletableEntityRepository<OrderData> orderDataRepository)
        {
            this.orderDataRepository = orderDataRepository;
        }

        public async Task<string> CreateNewOrderData(MakeOrderInputModel input)
        {
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
                MoreInfo = input.MoreInfo,
            };
            await this.orderDataRepository.AddAsync(orderData);
            await this.orderDataRepository.SaveChangesAsync();

            return orderData.Id;
        }

        public async Task<OrderData> GetOrderDataByIdAsync(string id)
            => await this.orderDataRepository.All().FirstOrDefaultAsync(x => x.Id == id);
    }
}
