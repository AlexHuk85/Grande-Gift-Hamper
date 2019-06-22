using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TafeGrandeGift.Data;
using TafeGrandeGift.Models;
using TafeGrandeGift.Models.HamperViewModels;

namespace TafeGrandeGift.Controllers
{
    [Authorize]
    public class HampersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HampersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Hampers
        public async Task<IActionResult> Index(string searchInput, string searchCategory, int? MaxPrice, int? MinPrice)
        {
            HamperIndexViewModel hamper = new HamperIndexViewModel();

            hamper.Hampers =  _context.Hamper.Where(r => r.IsRemove != true).ToList();

            IQueryable<string> categoriesList = _context.Category.OrderBy(n => n.CategoryName).Select(n => n.CategoryName);

            if (!String.IsNullOrEmpty(searchInput))
            {
                hamper.Hampers = _context.Hamper.Where(n => n.HamperName.Contains(searchInput)).ToList();
            }

            if (!String.IsNullOrEmpty(searchCategory))
            {
                var selectedCatogoryId = _context.Category.Where(n => n.CategoryName == searchCategory).Select(n => n.CategoryId).FirstOrDefault();
                hamper.Hampers = _context.Hamper.Where(n => n.CategoryId == selectedCatogoryId).ToList();
            }


            hamper.MinPrice = MinPrice.ToString();
            hamper.MaxPrice = MaxPrice.ToString();
            if (MaxPrice != null || MinPrice != null) 
            {
                if (MaxPrice != null && MinPrice == null)
                {
                    hamper.Hampers = _context.Hamper.Where(p => p.HamperPrice <= MaxPrice).OrderBy(p => p.HamperPrice).ToList();
                }
                else if (MinPrice != null && MaxPrice == null)
                {
                    hamper.Hampers = _context.Hamper.Where(p => p.HamperPrice >= MinPrice).OrderBy(p => p.HamperPrice).ToList();
                }
                else
                {
                    hamper.Hampers = _context.Hamper.Where(p => p.HamperPrice <= MaxPrice && p.HamperPrice >= MinPrice).OrderBy(p => p.HamperPrice).ToList();
                }
            }
            

            hamper.CatogoryForSelect = new SelectList(await categoriesList.Distinct().ToListAsync());
            hamper.CategoryId = hamper.Hampers.Select(i => i.CategoryId).ToList();
            hamper.DisplayHamper = hamper.Hampers.ToList();

            hamper.Categories = _context.Category.ToList();
            hamper.CategoryName = _context.Category.Select(i => i.CategoryName).ToList();

            return View(hamper);
        }

        [HttpGet]
        // GET: Hampers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HamperDetailViewModel hamperDetailViewModel = new HamperDetailViewModel();

            var hamper = await _context.Hamper
                .SingleOrDefaultAsync(m => m.HamperId == id);
            if (hamper == null)
            {
                return NotFound();
            }
            hamperDetailViewModel.Hamper = hamper;
            hamperDetailViewModel.HamperId = hamper.HamperId;

            hamperDetailViewModel.HamperPrice = hamper.HamperPrice;
            hamperDetailViewModel.HamperName = hamper.HamperName;

            //hamperDetailViewModel.HamperProducts = _context.hamperProducts;

            //hamperDetailViewModel.ProductId = _context.hamperProducts.Where(i => i.HamperId == id).Select(i => i.ProductId).ToList();

            //hamperDetailViewModel.ProductNameList = _context.Product
            //    .Where(p => hamperDetailViewModel.ProductId.Contains(p.ProductId))
            //    .Select(n => n.ProductName).ToList();

            hamperDetailViewModel.Category = _context.Category.Where(i => i.CategoryId == hamper.CategoryId).Select(i => i.CategoryName).FirstOrDefault();
            hamperDetailViewModel.HamperDetail = hamper.HamperDetail;

            hamperDetailViewModel.FileContent = hamper.FileContent;
            hamperDetailViewModel.ContentType = hamper.ContentType;

            //User feedback
            //User.Identity.Name => to get current user name
            hamperDetailViewModel.UserFeedBack = _context.hamperFeedBacks.Where(i => i.HamperId == hamperDetailViewModel.HamperId).ToList();
            hamperDetailViewModel.UserName = User.Identity.Name;

            return View(hamperDetailViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Feedback(int id,HamperDetailViewModel hamperDetailViewModel)
        {
            var hamper = await _context.Hamper.SingleOrDefaultAsync(m => m.HamperId == id);
            HamperFeedBack hamperFeedBack = new HamperFeedBack();

            hamperFeedBack.UserName = User.Identity.Name;
            hamperFeedBack.UserFeedBack = hamperDetailViewModel.UserFeedback;
            hamperFeedBack.HamperId = hamper.HamperId;

            _context.Add(hamperFeedBack);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles ="Admin")]
        // GET: Hampers/Create
        public IActionResult Create()
        {
            HamperCreateViewModel hamperCreateViewModel = new HamperCreateViewModel();
            //hamperCreateViewModel.ProductList = _context.Product.ToList();
            hamperCreateViewModel.CategoryList = _context.Category.ToList();
            return View(hamperCreateViewModel);
        }

        // POST: Hampers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(HamperCreateViewModel hamperProductViewModel)
        {
            Hamper hamper = new Hamper();
            if (ModelState.IsValid)
            {
                hamper.HamperName = hamperProductViewModel.HamperName;
                hamper.HamperPrice = hamperProductViewModel.HamperPrice;
                hamper.CategoryId = hamperProductViewModel.CategoryId;
                hamper.HamperDetail = hamperProductViewModel.HamperDetail;

                //File upload
                BinaryReader binaryReader = new BinaryReader(hamperProductViewModel.Upload.OpenReadStream());
                byte[] fileData = binaryReader.ReadBytes((int)hamperProductViewModel.Upload.Length);
                var fileName = Path.GetFileName(hamperProductViewModel.Upload.FileName);

                hamper.FileName = fileName;
                hamper.ContentSize = hamperProductViewModel.Upload.Length;
                hamper.ContentType = hamperProductViewModel.Upload.ContentType;
                hamper.FileContent = fileData;
                _context.Add(hamper);

                //for (int i = 0; i < hamperProductViewModel.Selected.Count(); i++)
                //{
                //    HamperProduct hamperProduct = new HamperProduct();
                //    hamperProduct.HamperId = hamper.HamperId;
                //    hamperProduct.ProductId = hamperProductViewModel.Selected[i];
                //    _context.Add(hamperProduct);
                //}

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hamper);
        }

        [Authorize(Roles = "Admin")]
        // GET: Hampers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HamperEditViewModel hamperEditViewModel = new HamperEditViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var hamper = await _context.Hamper.SingleOrDefaultAsync(m => m.HamperId == id);
            if (hamper == null)
            {
                return NotFound();
            }

            hamperEditViewModel.HamperId = hamper.HamperId;
            hamperEditViewModel.HamperName = hamper.HamperName;
            hamperEditViewModel.HamperPrice = hamper.HamperPrice;
            hamperEditViewModel.HamperDetail = hamper.HamperDetail;

            //hamperEditViewModel.ProductList = _context.Product.ToList();
            //hamperEditViewModel.CategoryName = _context.Category.Select(i => i.CategoryName).ToList();
            hamperEditViewModel.CategoriesList = _context.Category.ToList();
            hamperEditViewModel.CategoryId = hamper.CategoryId;
            //hamperEditViewModel.ProductId = _context.hamperProducts.Where(i => i.HamperId == id).Select(i => i.ProductId).ToList();


            //hamperEditViewModel.ProductNameList = _context.Product
            //    .Where(p => hamperEditViewModel.ProductId.Contains(p.ProductId))
            //    .Select(n => n.ProductName).ToList();
            //hamperEditViewModel.ProductNameList = _context.Product.Where(n => n.ProductCategory == hamperEditViewModel.CategoryName).Select(n => n.ProductName).ToList();

            return View(hamperEditViewModel);
        }

        // POST: Hampers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, HamperEditViewModel hamperEditViewModel)
        {
            hamperEditViewModel.HamperId = _context.Hamper.Where(i => i.HamperId == id).Select(i => i.HamperId).FirstOrDefault();
            if (id != hamperEditViewModel.HamperId)
            {
                return NotFound();
            }

            var hamper = await _context.Hamper.SingleOrDefaultAsync(m => m.HamperId == id);

            if (ModelState.IsValid)
            {
                
                try
                {
                    hamper.HamperId = id;
                    hamper.HamperName = hamperEditViewModel.HamperName;
                    hamper.HamperPrice = hamperEditViewModel.HamperPrice;
                    hamper.HamperDetail = hamperEditViewModel.HamperDetail;
                    hamper.CategoryId = hamperEditViewModel.CategoryId;
                    _context.Update(hamper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HamperExists(hamper.HamperId))
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "CategoryId", hamper.CategoryId);
            return View(hamper);
        }

        [Authorize(Roles = "Admin")]
        // GET: Hampers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hamper = await _context.Hamper
                .Include(h => h.Category)
                .SingleOrDefaultAsync(m => m.HamperId == id);
            if (hamper == null)
            {
                return NotFound();
            }

            return View(hamper);
        }

        [Authorize(Roles = "Admin")]
        // POST: Hampers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var hamper = await _context.Hamper.SingleOrDefaultAsync(m => m.HamperId == id);
        //    _context.Hamper.Remove(hamper);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hamper = await _context.Hamper.SingleOrDefaultAsync(m => m.HamperId == id);
            hamper.IsRemove = true;
            _context.Update(hamper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RestoreIndex()
        {
            HamperIndexViewModel hamper = new HamperIndexViewModel();

            hamper.Hampers = _context.Hamper.Where(r => r.IsRemove == true).ToList();

            return View(hamper);
        }
        public async Task<IActionResult> RestoreDetail(int id)
        {
            HamperDetailViewModel hamperDetailViewModel = new HamperDetailViewModel();
            var hamper = await _context.Hamper
                .SingleOrDefaultAsync(m => m.HamperId == id);
            hamperDetailViewModel.Hamper = hamper;
            hamperDetailViewModel.HamperId = hamper.HamperId;
            hamperDetailViewModel.HamperName = hamper.HamperName;
            hamperDetailViewModel.IsRemove = hamper.IsRemove;

            return View(hamperDetailViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> RestoreDetail(int? id, HamperIndexViewModel hamperViewModel)
        {
            var hamper = await _context.Hamper.SingleOrDefaultAsync(m => m.HamperId == id);
            hamper.IsRemove = hamperViewModel.IsRemove;
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
        private bool HamperExists(int id)
        {
            return _context.Hamper.Any(e => e.HamperId == id);
        }
    }
}
