namespace Sunset_TiendaOnline.Models;

public class StockPorTalle
{
    public int Id { get; set; }

    public Talle Talle { get; set; }

    public int Cantidad { get; set; }

    public int ProductoId { get; set; }

    //public Producto Producto { get; set; } = null!;
}
