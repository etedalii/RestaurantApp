using MehrWeb.Models;
using MehrWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MehrWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int productId)
        {
            var response = await _productService.GetAllProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int productId)
        {
            var response = await _productService.GetAllProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
