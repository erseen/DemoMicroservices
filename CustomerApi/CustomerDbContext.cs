using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustomerApi
{
    public class CustomerDbContext:DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> dbContextOptions):base(dbContextOptions) 
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if(!databaseCreator.HasTables()) databaseCreator.CreateTables();


                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message, "Database Connection Problemmmm !!!!!!!!");
            }
            
        }
        public DbSet <Customer> Customers { get; set; }

    }
}
