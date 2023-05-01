using RentalBook.Models.Authentication;

namespace RentalBook.Models.ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public double OrderTotal { get; set; }
        public IEnumerable<OrderHeader> OrderList { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
