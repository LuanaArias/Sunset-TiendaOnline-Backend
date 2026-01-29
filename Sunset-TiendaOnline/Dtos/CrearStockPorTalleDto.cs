namespace Sunset_TiendaOnline.Dtos;

using Sunset_TiendaOnline.Models;

public class CrearStockPorTalleDto
{
    public Talle Talle { get; set; }
    public int Cantidad { get; set; }

    public int ProductoId { get; set; }

}
