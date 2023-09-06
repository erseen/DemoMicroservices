using CustomerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _customerDbContext;

        public CustomerController(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers() 
        { 
        return _customerDbContext.Customers;
        
        }
        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetById(int customerId)
        {
            var customer = await _customerDbContext.Customers.FindAsync(customerId);    
            return customer;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok();    
        }
        [HttpPut]
        public async Task<ActionResult> Update(Customer customer)
        {
            if (customer == null) 
            {
                return NotFound(); 
            }
            _customerDbContext.Customers.Update(customer);
            _customerDbContext.SaveChanges();   
            return Ok();
        }
        [HttpDelete("{customerId:int}")]
        public async Task<ActionResult> Delete(int customerId)
        {
             var customer =await _customerDbContext.Customers.FindAsync(customerId);
            if(customer==null)
            {
                return NotFound();
            }
            _customerDbContext.Remove(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
