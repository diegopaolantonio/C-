using SistemaGestionData.DataAccess;
using SistemaGestionEntities;

namespace SistemaGestionBussiness.Services;

public class VentasService
{
    private VentasDataAccess _ventasDataAccess;

    public VentasService(VentasDataAccess ventasDataAccess)
    {
        _ventasDataAccess = ventasDataAccess;
    }

    public async Task<List<Venta>> GetVentas()
    {
        return await _ventasDataAccess.GetVentas();
    }

    public async Task<Venta?> GetOneVenta(int id)
    {
        return await _ventasDataAccess.GetOneVenta(id);
    }

    public async Task<Venta> InsertVenta(Venta venta)
    {
        return await _ventasDataAccess.InsertVenta(venta);
    }

    public async Task UpdateVenta(int id, Venta venta)
    {
        await _ventasDataAccess.UpdateVenta(id, venta);
    }

    public async Task DeleteVenta(int id)
    {
        await _ventasDataAccess.DeleteVenta(id);
    }

}