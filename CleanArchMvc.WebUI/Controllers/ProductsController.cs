using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId =
            new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO productDTO)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateAsync(productDTO);
            return RedirectToAction(nameof(Index));
        }
        return View(productDTO);
    }

    [HttpGet()]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var viewModel = await _productService.GetByIdAsync(id);

        if (viewModel == null) return NotFound();

        var categories = await _categoryService.GetCategoriesAsync();
        ViewBag.CategoryId = new SelectList(categories, "Id", "Name", viewModel.CategoryId);

        return View(viewModel);
    }

    [HttpPost()]
    public async Task<IActionResult> Edit(ProductDTO productDTO)
    {
        if (ModelState.IsValid)
        {
            await _productService.UpdateAsync(productDTO);
            return RedirectToAction(nameof(Index));
        }
        var categories = await _categoryService.GetCategoriesAsync();
        ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDTO.CategoryId);
        return View(productDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet()]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var viewModel = await _productService.GetByIdAsync(id);

        if (viewModel == null) return NotFound();
        return View(viewModel);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToAction("Index");
    }

    [HttpGet()]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var productDTO = await _productService.GetByIdAsync(id);

        if (productDTO == null) return NotFound();

        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + productDTO.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(productDTO);
    }

}
