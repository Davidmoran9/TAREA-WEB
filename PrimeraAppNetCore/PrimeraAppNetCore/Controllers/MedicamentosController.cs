using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using CapaDatos;
using System.Collections.Generic;
using CapaNegocio;

namespace PrimeraAppNetCore.Controllers
{
    public class MedicamentosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SinMenu()
        {
            return View();
        }

        public string saludo()
        {
            return "Hola amigos";
        }

        public List<MedicamentoCLS> listarMedicamentos()
        {
            MedicamentoBL obj = new MedicamentoBL();
            return obj.ListarMedicamentos();
        }

        public List<MedicamentoCLS> filtrarMedicamentos(string descripcionFiltrar)
        {
            MedicamentoBL obj = new MedicamentoBL();
            return obj.FiltrarMedicamentos(descripcionFiltrar);
        }
    }
}
