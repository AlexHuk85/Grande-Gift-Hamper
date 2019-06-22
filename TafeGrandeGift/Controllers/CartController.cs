using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TafeGrandeGift.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TafeGrandeGift.Helpers;
using TafeGrandeGift.Data;

namespace TafeGrandeGift.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("index")]
        public IActionResult Index()
        {

            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                return RedirectToAction("Index", "Hampers");
            }
            else
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Hamper.HamperPrice * item.Quantity);
            }
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int? id)
        {
            //Hamper productModel = new Hamper();
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Hamper = _context.Hamper.Where(i => i.HamperId == id).FirstOrDefault(), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                var hamper = _context.Hamper.Where(i => i.HamperId == id).FirstOrDefault();
                var carthamper = cart.Select(n => n.Hamper);
                foreach (var item in cart)
                {
                    if (item.Hamper.HamperId == id)
                    {
                        item.Quantity++;
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        cart.Add(new CartItem { Hamper = _context.Hamper.Where(i => i.HamperId == id).FirstOrDefault(), Quantity = 1 });
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        return RedirectToAction("Index");
                    }
                }

                //SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        
        public IActionResult Remove(int id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var index = cart.Where(i => i.Hamper.HamperId == id).FirstOrDefault();

            cart.Remove(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Hamper.HamperId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}