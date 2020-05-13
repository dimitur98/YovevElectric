using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YovevElectric.Data.Models;
using YovevElectric.Web.ViewModels.Bag;

namespace YovevElectric.Services.Data
{
    public interface IOrderDataService
    {
        Task<string> CreateNewOrderData(MakeOrderInputModel input);

        Task<OrderData> GetOrderDataByIdAsync(string id);
    }
}
