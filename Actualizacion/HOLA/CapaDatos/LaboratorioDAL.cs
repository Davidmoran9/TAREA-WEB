﻿using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class LaboratorioDAL:CadenaDAL
    {

        public List<LaboratorioCLS> listarLaboratorios()
        {
            List<LaboratorioCLS> lista = new List<LaboratorioCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new LaboratorioCLS
                                {
                                    idLaboratorio = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? null : dr.GetString(1),
                                    direccion = dr.IsDBNull(2) ? null : dr.GetString(2),
                                    personaContacto = dr.IsDBNull(3) ? null : dr.GetString(3)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en listarLaboratorios: {ex.Message}");
                    lista = new List<LaboratorioCLS>(); // Devolver lista vacía en vez de null
                }
            }
            return lista;
        }


        public List<LaboratorioCLS> filtrarLaboratorios(LaboratorioCLS obj)
        {
            List<LaboratorioCLS> lista = new List<LaboratorioCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", (object)obj.nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@direccion", (object)obj.direccion ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@personacontacto", (object)obj.personaContacto ?? DBNull.Value);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new LaboratorioCLS
                                {
                                    idLaboratorio = dr.GetInt32(0),
                                    nombre = dr.IsDBNull(1) ? null : dr.GetString(1),
                                    direccion = dr.IsDBNull(2) ? null : dr.GetString(2),
                                    personaContacto = dr.IsDBNull(3) ? null : dr.GetString(3)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en filtrarLaboratorios: {ex.Message}");
                    lista = new List<LaboratorioCLS>(); // Devolver lista vacía en vez de null
                }
            }
            return lista;
        }
        public int GuardarLaboratorio(LaboratorioCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarLaboratorio", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idlaboratorio", obj.idLaboratorio);
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@direccion", obj.direccion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@personacontacto", obj.personaContacto ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@numerocontacto", obj.numeroContacto ?? (object)DBNull.Value);

                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en GuardarLaboratorio: {ex.Message}");
                    rpta = 0;
                }
            }
            return rpta;
        }



    }
}
