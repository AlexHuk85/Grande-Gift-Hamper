using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TafeGrandeGift.Data;
using TafeGrandeGift.Helpers;
using TafeGrandeGift.Models;
using TafeGrandeGift.Models.OrderViewModel;

namespace TafeGrandeGift.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            OrderIndexViewModel viewModel = new OrderIndexViewModel();
            var user = await _userManager.GetUserAsync(User);
            viewModel.UserName = user.UserName;
            viewModel.Orders = _context.Order.Where(i => i.UserId == user.Id).ToList();
            //var orderId = viewModel.Orders.Select(i => i.OrderId).FirstOrDefault();
            viewModel.OrderHampers = _context.OrderHamper.ToList();
            return View(viewModel);
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            OrderIndexViewModel viewModel = new OrderIndexViewModel();
            var user = await _userManager.GetUserAsync(User);
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            viewModel.UserName = user.UserName;
            viewModel.UserId = user.Id;
            viewModel.Addresses = _context.UserAddress.Where(i => i.ApplicationUserId == user.Id).ToList();
            viewModel.OrderDate = DateTime.Now.Date;
            viewModel.CartItems = cart;
            //viewModel.Price = cart.Select(p => p.Hamper.HamperPrice).FirstOrDefault();
            viewModel.Total = cart.Sum(item => item.Hamper.HamperPrice * item.Quantity);
            viewModel.TotalWithShipping = viewModel.Total + (decimal)7.5;
            return View(viewModel);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderIndexViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            Order order = new Order();
            
            if (ModelState.IsValid)
            {

                order.UserId = user.Id;
                order.UserName = user.UserName;
                order.OrderDate = DateTime.Now.Date;
                order.UserAddress = _context.UserAddress.Where(i => i.UserAddressId == viewModel.AddressId).Select(n => n.Address).FirstOrDefault();
                order.Total = cart.Sum(item => item.Hamper.HamperPrice * item.Quantity);
                order.TotalWithShipping = order.Total + (decimal)7.5;
                _context.Add(order);
                

                foreach (var hamper in cart)
                {
                    OrderHamper orderHamper = new OrderHamper();
                    orderHamper.OrderId = order.OrderId;
                    orderHamper.HamperName = hamper.Hamper.HamperName;
                    orderHamper.Qty = hamper.Quantity;
                    _context.Add(orderHamper);
                    
                }
                //_context.Add(orderHamper);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,UserId,UserName,UserAddress,OrderDate,Total")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderId == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
