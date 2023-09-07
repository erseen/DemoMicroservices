using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderApi.Models;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderController()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";


            var mongoUrl = MongoUrl.Create(connectionString);
            MongoClient mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _orderCollection = database.GetCollection<Order>("order");

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }
        [HttpGet("orderId")]
        public async Task<ActionResult<Order>> GetById(string orderId)
        {
            var filterDefination = Builders<Order>.Filter.Eq(x=>x.OrderId, orderId); 
            if (filterDefination == null)
            {
                return NotFound();
            }
            return await _orderCollection.Find(filterDefination).SingleOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            await _orderCollection.InsertOneAsync(order);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Order order)
        {
            var filterDefination=Builders<Order>.Filter.Eq(x=>x.OrderId, order.OrderId);
            if (filterDefination == null)
            {
                return NotFound();
            }
            await _orderCollection.ReplaceOneAsync(filterDefination, order);
            return Ok();    
        }
        [HttpDelete("{orderId}")]
        public async Task<ActionResult> Delete(string orderId)
        {
            var filterDefination=Builders<Order>.Filter.Eq(x=> x.OrderId, orderId);
            if (filterDefination == null) 
            { 
            return NotFound();
            }
            await _orderCollection.DeleteOneAsync(filterDefination);
            return Ok();    
        }

    }
}
