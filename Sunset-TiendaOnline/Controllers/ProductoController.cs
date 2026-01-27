using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sunset_TiendaOnline.Data;
using Sunset_TiendaOnline.Models;

namespace Sunset_TiendaOnline.Controllers;

[ApiController]
[Route("api/[controller]")] //Completa los corchetes con "productos" automaticamente
public class ProductosController : ControllerBase  //Hereda de ControllerBase
{
    private readonly AppDbContext _context; //Campo privado para usar la BD

    public ProductosController(AppDbContext context) { //.NET crea AppDbContext y lo inyecta acá
        _context = context;
    }

    // GET: api/productos
    [HttpGet]  
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos(){ 
        //Asincrono
        //TASK ES PORQUE ES UNA PROMESA
        //IEnumerable<Producto> → lista de productos
        //ActionResult → respuesta HTTP

        return await _context.Productos //Va a la tabla productos de la base
            .Include(p => p.StockPorTalle) //Carga también la tabla StockPorTalle
            .ToListAsync(); //Devuelve la lista
    }

    // GET: api/productos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.StockPorTalle)
            .FirstOrDefaultAsync(p => p.Id == id); //Devuelve el primero o null

        if (producto == null)
            return NotFound();

        return producto;
    }
}