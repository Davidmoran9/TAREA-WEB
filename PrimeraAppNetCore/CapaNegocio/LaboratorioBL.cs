using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class LaboratorioBL
    {
        public List<LaboratorioCLS> ListarLaboratorios() {
            LaboratorioDAL laboratorioDAL = new LaboratorioDAL();
            return laboratorioDAL.ListarLaboratorios();
        }

        public List<LaboratorioCLS> FiltrarLaboratorios(LaboratorioCLS laboratorio)
        {
            LaboratorioDAL laboratorioDAL = new LaboratorioDAL();
            return laboratorioDAL.filtrarLaboratorios(laboratorio);
        }
    }
}
