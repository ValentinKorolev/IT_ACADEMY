using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.DataJson
{
    internal class JsonOrderRepository : IRepository<Order>
    {
        public void Create(Order item)
        {
            ListOrders model = new ListOrders();

            if (File.Exists(Observer.FileNameOrders))
            {
                var fileName = File.ReadAllText(Observer.FileNameOrders);
                var ordersJson = JsonConvert.DeserializeObject<ListOrders>(fileName);

                model.Orders = ordersJson.Orders;

                model.Orders.Add(item);

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameOrders, _jsonObject);
            }
            else
            {
                model.Orders.Add(item);

                string _jsonObject = JsonConvert.SerializeObject(model);

                File.AppendAllText(Observer.FileNameOrders, _jsonObject);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Order GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetItemList()
        {
            var fileName = File.ReadAllText(Observer.FileNameOrders);
            var objectJson = JsonConvert.DeserializeObject<ListOrders>(fileName);

            var orders = objectJson.Orders;

            return orders.ToList();
        }

        public IEnumerable<Order> GetItemList(StatusOrder status)
        {
            var fileName = File.ReadAllText(Observer.FileNameOrders);
            var objectJson = JsonConvert.DeserializeObject<ListOrders>(fileName);

            var orders = objectJson.Orders.FindAll(_ => _.Status == status);

            return orders.ToList();
        }

        public void Update(Order item)
        {
            ListOrders model = new ListOrders();

            if (File.Exists(Observer.FileNameOrders))
            {
                var fileName = File.ReadAllText(Observer.FileNameOrders);
                var ordersJson = JsonConvert.DeserializeObject<ListOrders>(fileName);

                model.Orders = ordersJson.Orders;

                int index = model.Orders.IndexOf(model.Orders.FirstOrDefault(_ => _.Id == item.Id));
                model.Orders[index] = item;
            }
            else
            {
                Clear();
                WriteLine("File Orders.json not found");
                Thread.Sleep(4000);
            }
        }
    }
}
