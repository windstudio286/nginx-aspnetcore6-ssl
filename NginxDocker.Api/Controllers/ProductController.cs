using Microsoft.AspNetCore.Mvc;

namespace NginxDocker.Api.Controllers;

using System;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NginxDocker.Api.Models;
[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController : ControllerBase
{

    private readonly WebApiDbContext _dBcontext;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, WebApiDbContext context)
    {
        _logger = logger;
        _dBcontext = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        return await _dBcontext.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _dBcontext.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product newProduct)
    {
        _dBcontext.Products.Add(newProduct);
        await _dBcontext.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = newProduct.ProductId }, newProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest();
        }

        _dBcontext.Entry(product).State = EntityState.Modified;

        try
        {
            await _dBcontext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    private bool ProductExists(int id)
    {
        return _dBcontext.Products.Any(e => e.ProductId == id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _dBcontext.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        _dBcontext.Products.Remove(product);
        await _dBcontext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    [Route("AddProduct")]
    [AllowAnonymous]
    public async Task<IActionResult> AddProduct()
    {

        var user_name = "congtt"; // Example user ID
        var user_password = "23071958"; // Example user name
        var userRecord = _dBcontext.Users.FromSqlRaw("SELECT * FROM create_or_get_user({0}, {1}) as (user_id int,user_name varchar,password varchar,refresh_token varchar,refresh_token_expiry_time timestamp)", user_name, user_password).FirstOrDefault();
        if (userRecord != null)
        {
            Console.WriteLine($"ID: {userRecord.UserId}, Name: {userRecord.Username}");
        }
        return Ok(userRecord);
    }
}

