
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CapaDatos;

namespace AplicativoMejorado.Controllers
{
    public class TipoMedicamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<TipoMedicamentoCLS> listarTiposMedicamento()
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.listarTipoMedicamento();
        }

        public List<TipoMedicamentoCLS> filtrarTiposMedicamento(TipoMedicamentoCLS objTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.filtrarTipoMedicamento(objTipoMedicamento);
        }

        public int GuardarTipoMedicamento(TipoMedicamentoCLS objTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.GuardarTipoMedicamento(objTipoMedicamento);
        }
        public TipoMedicamentoCLS recuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            TipoMedicamentoCLS tipoMedicamento = obj.recuperarTipoMedicamento(idTipoMedicamento);

            if (tipoMedicamento == null)
            {
                return new TipoMedicamentoCLS(); // Evita errores al devolver un objeto vacío
            }

            return tipoMedicamento;
        }

        [HttpPost]
        public int ActualizarTipoMedicamento([FromBody] TipoMedicamentoCLS objTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.ActualizarTipoMedicamento(objTipoMedicamento);
        }
    }
}
