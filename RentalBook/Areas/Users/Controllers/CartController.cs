﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalBook.Data;
using RentalBook.Models;
using RentalBook.Models.Authentication;
using RentalBook.Models.ViewModels;
using Stripe.Checkout;

namespace RentalBook.Areas.Customer.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _db;

        [BindProperty]
        public CartVM CartVM { get; set; }
        public CartController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration, AppDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            var UId = HttpContext.Session.GetString("UserId");

            var cartData = (from s in _db.ShoppingCarts
                            join p in _db.Products on s.ProductId equals p.Id
                            where s.UserId == UId
                            select new CartVM
                            {
                                Name = p.Title,
                                Description = p.Description,
                                Price = s.Price,
                                Quantity = s.Quantity,
                                ImageUrl = p.ImageUrl,
                                UserId = s.UserId,
                                ProductId = s.ProductId,
                                Id = s.Id,
                            }).ToList();
            double P = 0;
            foreach (var item in cartData)
            {
                double TP = item.Price * item.Quantity;
                P += TP;
                ViewBag.OrderTotal = P;
            }
            return View(cartData);
        }

        public IActionResult Plus(int cartId)
        {
            var cartItem = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            cartItem.Quantity++;
            _db.ShoppingCarts.Update(cartItem);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartItem = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            if (cartItem.Quantity <= 1)
            {
                _db.ShoppingCarts.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity--;
                _db.ShoppingCarts.Update(cartItem);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartItem = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            _db.ShoppingCarts.Remove(cartItem);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var UId = HttpContext.Session.GetString("UserId");

            CartVM = new CartVM()
            {
                CartItem = _db.ShoppingCarts.Where(u => u.UserId == UId).ToList(),
                Product = new(),
                OrderHeader = new(),
            };
            CartVM.OrderHeader.ApplicationUser = _db.Users.FirstOrDefault(u => u.Id == UId);
            CartVM.OrderHeader.Name = CartVM.OrderHeader.ApplicationUser.UserName;
            CartVM.OrderHeader.PhoneNumber = CartVM.OrderHeader.ApplicationUser.PhoneNumber;
            CartVM.OrderHeader.Area = CartVM.OrderHeader.ApplicationUser.Area;
            CartVM.OrderHeader.City = CartVM.OrderHeader.ApplicationUser.City;
            CartVM.OrderHeader.State = CartVM.OrderHeader.ApplicationUser.State;
            CartVM.OrderHeader.PostalCode = CartVM.OrderHeader.ApplicationUser.PinCode;

            ViewBag.cartData = (from s in _db.ShoppingCarts
                                join p in _db.Products on s.ProductId equals p.Id
                                where s.UserId == UId
                                select new CartVM
                                {
                                    Name = p.Title,
                                    Price = s.Price,
                                    Quantity = s.Quantity,
                                }).ToList();
            double P = 0;
            foreach (var item in CartVM.CartItem)
            {
                double TP = item.Price * item.Quantity;
                P += TP;
                ViewBag.OrderTotal = P;
            }
            return View(CartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            var UId = HttpContext.Session.GetString("UserId");

            CartVM.CartItem = _db.ShoppingCarts.Where(u => u.UserId == UId).ToList();

            CartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            CartVM.OrderHeader.OrderStatus = SD.StatusPending;
            CartVM.OrderHeader.OrderDate = DateTime.Now;
            CartVM.OrderHeader.ApplicationUserId = UId;

            double P = 0;
            foreach (var item in CartVM.CartItem)
            {
                double TP = item.Price * item.Quantity;
                P += TP;
                CartVM.OrderHeader.OrderTotal = P;
            }
            _db.OrderHeaders.Add(CartVM.OrderHeader);
            _db.SaveChanges();

            foreach (var item in CartVM.CartItem)
            {

                OrderDetail orderDetail = new()
                {
                    ProductId = item.ProductId,
                    OrderHeaderId = CartVM.OrderHeader.Id,
                    Price = item.Price,
                    Quantity = item.Quantity,
                };
                _db.OrderDetails.Add(orderDetail);
                _db.SaveChanges();
            }


            //Stripe Settings
            var domain = "https://localhost:44374/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"Users/Cart/OrderConfirmation?id={CartVM.OrderHeader.Id}",
                CancelUrl = domain + $"Users/Cart/Index",
            };
            foreach (var item in CartVM.CartItem)
            {
                var product = _db.Products.First(u => u.Id == item.ProductId);
                if (product != null)
                {
                    int qty = product.Quantity - item.Quantity;
                    product.IsOrdered = true;
                    product.Quantity = qty;
                    _db.Products.Update(product);
                    _db.SaveChanges();
                }
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100), //20.00 -> 2000
                        Currency = "inr",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Title,
                        },
                    },
                    Quantity = item.Quantity,
                };
                options.LineItems.Add(sessionLineItem);
            }


            var service = new SessionService();
            Session session = service.Create(options);
            CartVM.OrderHeader.SessionId = session.Id;
            CartVM.OrderHeader.PaymentIntentId = session.PaymentIntentId;
            _db.OrderHeaders.Update(CartVM.OrderHeader);
            _db.SaveChanges();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);

            //check Stripe status
            if (session.PaymentStatus.ToLower() == "paid")
            {
                orderHeader.OrderStatus = SD.StatusApproved;
                orderHeader.PaymentStatus = SD.PaymentStatusApproved;

                _db.OrderHeaders.Update(orderHeader);
                _db.SaveChanges();
            }
            List<ShoppingCart> cartList = _db.ShoppingCarts.Where(x => x.UserId == orderHeader.ApplicationUserId).ToList();
            _db.ShoppingCarts.RemoveRange(cartList);
            _db.SaveChanges();
            return View(id);
        }
    }
}
