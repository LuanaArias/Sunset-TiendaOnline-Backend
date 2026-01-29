using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sunset_TiendaOnline.Data;
using Sunset_TiendaOnline.Models;
using Sunset_TiendaOnline.Dtos;

namespace Sunset_TiendaOnline.Controllers;

[ApiController]
[Route("api/[controller]")] //Completa los corchetes con "productos" automaticamente
public class ProductosController : ControllerBase  //Hereda de ControllerBase
{
    private readonly AppDbContext _context; //Campo privado para usar la BD

    public ProductosController(AppDbContext context)
    { //.NET crea AppDbContext y lo inyecta acá
        _context = context;
    }

    // GET: api/productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
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

    // POST: api/productos/

    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto([FromBody] CrearProductoDto dto)
    {
        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Imagen = dto.Imagen,
            Categoria = dto.Categoria
            /*  StockPorTalle = dto.StockPorTalle.Select(s => new StockPorTalle
             {
                 Talle = s.Talle,
                 Cantidad = s.Cantidad
             }).ToList() */
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
    }


    // POST: api/productos/stock
    [HttpPost("stock")]
    public async Task<ActionResult<StockPorTalle>> PostStockPorProducto(CrearStockPorTalleDto dto)
    {
        var productoExiste = await _context.Productos
            .AnyAsync(p => p.Id == dto.ProductoId);

        if (!productoExiste)
            return NotFound("El producto no existe");

        var existeStock = await _context.StockPorTalle
            .AnyAsync(s => s.ProductoId == dto.ProductoId && s.Talle == dto.Talle);

        if (existeStock)
            return BadRequest("Ya existe stock para ese talle");

        var stockPorProducto = new StockPorTalle
        {
            ProductoId = dto.ProductoId,
            Talle = dto.Talle,
            Cantidad = dto.Cantidad
        };

        _context.StockPorTalle.Add(stockPorProducto);
        await _context.SaveChangesAsync();

        return Ok(stockPorProducto);
    }

}