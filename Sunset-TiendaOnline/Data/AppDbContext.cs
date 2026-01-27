using Microsoft.EntityFrameworkCore;
using Sunset_TiendaOnline.Models;

namespace Sunset_TiendaOnline.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}

    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<StockPorTalle> StockPorTalle => Set<StockPorTalle>();

}
