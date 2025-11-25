using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productsService;

    public ProductsController(IProductService productService)
    {
        _productsService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var products = await _productsService.GetProductsAsync();
        if (products == null)
            return NotFound("Products not found");

        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var product = await _productsService.GetByIdAsync(id);
        if (product == null)
            return NotFound("Product not found");

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
    {
        if (productDTO == null)
            return BadRequest("Invalid data");

        await _productsService.CreateAsync(productDTO);

        return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
    {
        if (id != productDTO.Id)
            return BadRequest("Invalid Id sent");

        if (productDTO == null)
            return BadRequest("Invalid data");

        await _productsService.UpdateAsync(productDTO);

        return Ok(productDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var product = await _productsService.GetByIdAsync(id);

        if (product == null)
            return NotFound("Product not found");

        await _productsService.DeleteAsync(id);

        return Ok(product);
    }
}
