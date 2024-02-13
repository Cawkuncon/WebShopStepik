using OnlineShop.DB.Models;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public static class OrderTransformation
    {
        public static OrderViewModel orderDBtoView(Order order, User us)
        {
            OrderViewModel orderInfo = new OrderViewModel();
            orderInfo.Id = order.Id;
            orderInfo.Status = order.Status;
            if (us != null)
            {
                orderInfo.Name = us.UserName;
                orderInfo.Number = us.PhoneNumber;
                orderInfo.Email = us.Email;
            }
            else
            {
                orderInfo.Name = order.Name;
                orderInfo.Number = order.Number;
                orderInfo.Email = order.Email;
            }
            orderInfo.Total = order.Total;
            orderInfo.CreationDate = order.CreationDate;
            orderInfo.CreationTime = order.CreationTime;
            orderInfo.Comments = order.Comments;
            orderInfo.Address = order.Address;
            return orderInfo;
        }
    }
}
