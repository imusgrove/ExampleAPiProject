using AutoFixture;
using RedOrderApi.Data;

namespace RedOrderApi.Data;

    public static class Seeder
    {
        public static void Seed(this OrderContext orderContext)
        {
            if (!orderContext.Orders.Any())
            {
                Fixture fixture = new Fixture();
                fixture.Customize<Order>(order => order.Without(p => p.OrderId));
               
                //--- The next two lines add 100 rows to your database
                List<Order> orders = fixture.CreateMany<Order>(100).ToList();

                foreach (var order in orders)
                {
                    order.OrderId = order.OrderId;
                    order.OrderType = order.OrderType;
                    order.CustomerName = order.CustomerName;
                    order.CreatedByUsername = order.CreatedByUsername;
                    order.CreatedDate = order.CreatedDate.ToUniversalTime();
                }
                orderContext.AddRange(orders);
                orderContext.SaveChanges();
            }
        }
    }

