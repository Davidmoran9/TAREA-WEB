using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace PrimeraAppNetCore.Controllers
{
    public class LaboratorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<LaboratorioCLS> ListarLaboratorios()
        {
            LaboratorioBL laboratorioBL = new LaboratorioBL();
            return laboratorioBL.ListarLaboratorios();
        }

        public List<LaboratorioCLS> FiltrarLaboratorio(LaboratorioCLS laboratorio)
        {
            LaboratorioBL laboratorioBL = new LaboratorioBL();
            return laboratorioBL.FiltrarLaboratorios(laboratorio);
        }
    }
}
