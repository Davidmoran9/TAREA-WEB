using CapaDatos;
using CapaEntid;
using CapaEntidad;

namespace CapaNegocio
{
    public class SucursalBL
    {
        public List<SucursalCLS> ListarSucursales()
        {
            SucursalDAL sucursalDAL = new SucursalDAL();
            return sucursalDAL.ListarSucursales();
        }

        public List<SucursalCLS> FiltrarSucursales(string nombreSucursal, string direccion)
        {
            SucursalDAL sucursalDAL = new SucursalDAL();
            return sucursalDAL.FiltrarSucursales(nombreSucursal, direccion);  // Enviar los parámetros de filtrado
        }
    }

}
