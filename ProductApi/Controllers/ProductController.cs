using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() 
        {
          return  _context.Products;

        }
        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Product>> GetById(int productId)
        {
            var products=await _context.Products.FindAsync(productId);
            if (products==null) return NotFound();
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();  
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok();    
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int productId)
        {
            var product= await _context.Products.FindAsync(productId);
            if (product==null) return NotFound();   
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
