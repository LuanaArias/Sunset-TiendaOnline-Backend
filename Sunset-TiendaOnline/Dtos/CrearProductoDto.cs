using Sunset_TiendaOnline.Models;

namespace Sunset_TiendaOnline.Dtos;

public class CrearProductoDto
{
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public decimal Precio { get; set; }
    public string Imagen { get; set; } = "";

    public Categoria Categoria { get; set; }

    // no tiene sentido que por cada producto le tengamos que setear esto
    //public List<CrearStockPorTalleDto> StockPorTalle { get; set; } = new();
}
