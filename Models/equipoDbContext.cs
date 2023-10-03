using Microsoft.EntityFrameworkCore;
using PractNET_2023.Models.Clases;

namespace PractNET_2023.Models
{
    public class equipoDbContext : DbContext
    {
        public equipoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Usuarios> Usuarios { get; set; }
        //public DbSet<Personas> Personas { get; set; }
    }
}
