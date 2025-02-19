using System.Collections.Specialized;
using System.Data.SqlClient;
using CapaEntidad;
using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class LaboratorioDAL : CadenaDAL
    {
        public List<LaboratorioCLS> ListarLaboratorios()
        {
            List<LaboratorioCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("uspListarLaboratorio", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr != null)
                        {
                            LaboratorioCLS oLaboratorioCLS;
                            lista = new List<LaboratorioCLS>();

                            int idLaboratorioOrdinal = dr.GetOrdinal("IIDLABORATORIO");
                            int nombreOrdinal = dr.GetOrdinal("NOMBRE");
                            int direccionOrdinal = dr.GetOrdinal("DIRECCION");
                            int personaContactoOrdinal = dr.GetOrdinal("PERSONACONTACTO");

                            while (dr.Read())
                            {
                                oLaboratorioCLS = new LaboratorioCLS();
                                oLaboratorioCLS.idLaboratorio = dr.GetInt32(idLaboratorioOrdinal);
                                oLaboratorioCLS.nombre = dr.GetString(nombreOrdinal);
                                oLaboratorioCLS.direccion = dr.GetString(direccionOrdinal);
                                oLaboratorioCLS.personaContacto = dr.GetString(personaContactoOrdinal);
                                lista.Add(oLaboratorioCLS);
                            }
                        }
                    }
                    cn.Close();
                    cn.Dispose();
                }
                catch (Exception ex)
                {
                    lista = null;
                }
            }
            return lista;
        }

        public List<LaboratorioCLS> filtrarLaboratorios(LaboratorioCLS obj)
        {
            List<LaboratorioCLS> lista = null;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarLaboratorio", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre == null ? "" : obj.nombre);
                        cmd.Parameters.AddWithValue("@direccion", obj.direccion == null ? "" : obj.direccion);
                        cmd.Parameters.AddWithValue("@personacontacto", obj.personaContacto == null ? "" : obj.personaContacto);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr != null)
                        {
                            LaboratorioCLS oLaboratorioCLS;
                            lista = new List<LaboratorioCLS>();

                            int idLaboratorioOrdinal = dr.GetOrdinal("IIDLABORATORIO");
                            int nombreOrdinal = dr.GetOrdinal("NOMBRE");
                            int direccionOrdinal = dr.GetOrdinal("DIRECCION");
                            int personaContactoOrdinal = dr.GetOrdinal("PERSONACONTACTO");

                            while (dr.Read())
                            {
                                oLaboratorioCLS = new LaboratorioCLS();
                                oLaboratorioCLS.idLaboratorio = dr.GetInt32(idLaboratorioOrdinal);
                                oLaboratorioCLS.nombre = dr.GetString(nombreOrdinal);
                                oLaboratorioCLS.direccion = dr.GetString(direccionOrdinal);
                                oLaboratorioCLS.personaContacto = dr.GetString(personaContactoOrdinal);
                                lista.Add(oLaboratorioCLS);
                            }
                        }
                    }
                    cn.Close();
                    cn.Dispose();
                }
                catch (Exception ex)
                {
                    lista = null;
                }
            }
            return lista;
        }
    }
}
