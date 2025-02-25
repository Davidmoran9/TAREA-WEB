using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class TipoMedicamentoDAL : CadenaDAL
    {
        public List<TipoMedicamentoCLS> listarTipoMedicamento()
        {
            List<TipoMedicamentoCLS> lista = new List<TipoMedicamentoCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarTipoMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TipoMedicamentoCLS tipoMedicamento = new TipoMedicamentoCLS
                                {
                                    iidTipoMedicamento = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? string.Empty : dr.GetString(1),
                                    descripcion = dr.IsDBNull(2) ? string.Empty : dr.GetString(2)
                                };

                                lista.Add(tipoMedicamento);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lista = null;
                    throw;
                }
            }
            return lista;
        }

        public List<TipoMedicamentoCLS> filtrarTipoMedicamento(TipoMedicamentoCLS obj)
        {
            List<TipoMedicamentoCLS> lista = new List<TipoMedicamentoCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarTipoMedicamento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombretipomedicamento", (object)obj.nombre ?? "");
                        cmd.Parameters.AddWithValue("@descripcion", (object)obj.descripcion ?? "");

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TipoMedicamentoCLS tipoMedicamento = new TipoMedicamentoCLS
                                {
                                    iidTipoMedicamento = dr.GetInt32(0),
                                    nombre = dr.GetString(1),
                                    descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2)
                                };

                                lista.Add(tipoMedicamento);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lista = null;
                    throw;
                }
            }
            return lista;
        }

        public TipoMedicamentoCLS recuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoCLS obj = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDTIPOMEDICAMENTO, NOMBRE, DESCRIPCION FROM TipoMedicamento WHERE IIDTIPOMEDICAMENTO = @id", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", idTipoMedicamento);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                obj = new TipoMedicamentoCLS
                                {
                                    iidTipoMedicamento = dr.GetInt32(0),
                                    nombre = dr.GetString(1),
                                    descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al recuperar tipo de medicamento: " + ex.Message);
                }
            }

            return obj;
        }

        public int ActualizarTipoMedicamento(TipoMedicamentoCLS obj)
        {
            int resultado = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE TipoMedicamento SET NOMBRE = @nombre, DESCRIPCION = @descripcion WHERE IIDTIPOMEDICAMENTO = @id", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                        cmd.Parameters.AddWithValue("@descripcion", obj.descripcion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@id", obj.iidTipoMedicamento);

                        resultado = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar tipo de medicamento: " + ex.Message);
                }
            }
            return resultado;
        }
        public int GuardarTipoMedicamento(TipoMedicamentoCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(@"
                        IF EXISTS (SELECT 1 FROM TipoMedicamento WHERE IIDTIPOMEDICAMENTO = @id)
                        BEGIN
                            UPDATE TipoMedicamento 
                            SET NOMBRE = @nombre, DESCRIPCION = @descripcion
                            WHERE IIDTIPOMEDICAMENTO = @id;
                        END
                        ELSE
                        BEGIN
                            INSERT INTO TipoMedicamento (NOMBRE, DESCRIPCION, BHABILITADO) 
                            VALUES (@nombre, @descripcion, 1);
                        END", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", obj.iidTipoMedicamento);
                        cmd.Parameters.AddWithValue("@nombre", (object)obj.nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@descripcion", (object)obj.descripcion ?? DBNull.Value);
                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    rpta = 0;
                    throw;
                }
            }
            return rpta;
        }
    }
}
