namespace Sunset_TiendaOnline.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string Imagen { get; set; } = "";
    public List<StockPorTalle> StockPorTalle { get; set; } = new();
    public Categoria Categoria { get; set; }

}

