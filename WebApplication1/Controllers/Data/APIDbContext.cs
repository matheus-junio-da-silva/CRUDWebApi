using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers.Models;

namespace WebApplication1.Controllers.Data;

public class APIDbContext : DbContext
{
    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
    {
        
    }
    public DbSet<State> State { get; set; }
}
