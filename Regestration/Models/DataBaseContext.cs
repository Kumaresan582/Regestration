using Microsoft.EntityFrameworkCore;

namespace Regestration.Models
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {

        }
        public DbSet<UserRegister> UserRegistration { get; set; }
    }
}
