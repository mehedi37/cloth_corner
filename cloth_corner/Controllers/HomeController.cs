using cloth_corner.Areas.Identity.Data;
using cloth_corner.Data;
using cloth_corner.Models;
using cloth_corner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace cloth_corner.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ProductService _productService;
        private readonly CustomerService _customerService;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, UserManager<AppUser> userManager, ProductService productService, CustomerService customerService)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _productService = productService;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = await _context.Products
                .Where(p => p.UserId != userId)
                .ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = string.IsNullOrEmpty(query)
                ? await _context.Products
                    .Where(p => p.UserId != userId)
                    .ToListAsync()
                : await _context.Products
                    .Where(p => (p.ProductName.Contains(query) || p.ProductDescription.Contains(query)) && p.UserId != userId)
                    .ToListAsync();

            return PartialView("_ProductList", products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var otherProducts = await _context.Products
                .Where(p => p.UserId != userId && p.ProductId != id)
                .Take(4)
                .ToListAsync();

            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                OtherProducts = otherProducts
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Sell()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(); // or handle the null case appropriately
            }

            var itemsForSale = await _productService.GetItemsForSaleByUserIdAsync(userId);
            var customers = await _customerService.GetCustomersBySellerIdAsync(userId);

            var model = new SellerViewModel
            {
                ItemsForSale = itemsForSale,
                Customers = customers
            };

            return View(model);
        }

        public IActionResult AddItem()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(new Products
            {
                ProductId = new int(),
                ProductName = string.Empty,
                ProductDescription = string.Empty,
                ProductImage = string.Empty,
                ProductPrice = 0.0M,
                UserId = userId,
                Stock = 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(Products product)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                product.UserId = userId ?? throw new InvalidOperationException("User ID cannot be null");
                await _productService.AddItemAsync(product);
                return RedirectToAction("Sell");
            }
            return View(product);
        }

        public async Task<IActionResult> EditItem(int id)
        {
            var product = await _productService.GetItemByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(Products product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateItemAsync(product);
                return RedirectToAction("Sell");
            }
            return View(product);
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            var product = await _productService.GetItemByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (product.UserId != userId)
            {
                return Unauthorized();
            }

            await _productService.DeleteItemAsync(id);
            return RedirectToAction("Sell");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
