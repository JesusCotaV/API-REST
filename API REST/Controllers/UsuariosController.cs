using API_REST.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuariosController: ControllerBase
    {
        //Cadena de conexion
        
        SqlConnection con = new SqlConnection("Data Source="+ Environment.GetEnvironmentVariable("Host")+";Initial Catalog=ExamenNezter;Integrated Security=True;TrustServerCertificate=True");

        //Obtener usuarios
        [HttpGet]
        [Route("listar")]
        public IActionResult listarUsuarios()
        {
            using (con)
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("usp_ListarUsuarios", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    List<Dictionary<string, object>> usuarios = new List<Dictionary<string, object>>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> usuario = new Dictionary<string, object>();

                        foreach (DataColumn column in dt.Columns)
                        {
                            usuario[column.ColumnName] = row[column.ColumnName];
                        }

                        usuarios.Add(usuario);
                    }

                    return Ok(usuarios);
                }
                else
                {
                    return NotFound("No data found");
                }
            }
        }

        // Obtener usuarios por ID
        [HttpGet]
        [Route("listarxid")]
        public IActionResult listarUsuariosxID(int ID)
        {
            using (con)
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("usp_ListarUsuariosPorID ", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@id", ID);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    List<Dictionary<string, object>> usuarios = new List<Dictionary<string, object>>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> usuario = new Dictionary<string, object>();

                        foreach (DataColumn column in dt.Columns)
                        {
                            usuario[column.ColumnName] = row[column.ColumnName];
                        }

                        usuarios.Add(usuario);
                    }

                    return Ok(usuarios);
                }
                else
                {
                    return NotFound("No data found");
                }
            }
        }


        //Guardar usuarios
        [HttpPost]
        [Route("guardar")]
        public dynamic guardarUsuarios(Usuario usuario)
        {
            using (con)
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("usp_InsertarUsuario", con);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                da.InsertCommand.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                da.InsertCommand.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                da.InsertCommand.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                da.InsertCommand.Parameters.AddWithValue("@CodigoPostal", usuario.CodigoPostal);
                da.InsertCommand.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                da.InsertCommand.Parameters.AddWithValue("@Estado", usuario.Estado);
                da.InsertCommand.Parameters.AddWithValue("@Ciudad", usuario.Ciudad);
                da.InsertCommand.Parameters.AddWithValue("@Usuario", usuario.User);
                da.InsertCommand.Parameters.AddWithValue("@Pass", usuario.Pass);

                con.Open();
                da.InsertCommand.ExecuteNonQuery();
                con.Close();
            }

            return new
            {
                success = true,
                message = "usuario registrado",
                result = usuario
            };
        }

        //Actualiar usuarios
        [HttpPut]
        [Route("actualizar")]
        public dynamic actualizarUsuario(Usuario usuario)
        {
            using (con)
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("usp_ActualizarUsuario", con);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                da.UpdateCommand.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                da.UpdateCommand.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                da.UpdateCommand.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                da.UpdateCommand.Parameters.AddWithValue("@CodigoPostal", usuario.CodigoPostal);
                da.UpdateCommand.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                da.UpdateCommand.Parameters.AddWithValue("@Estado", usuario.Estado);
                da.UpdateCommand.Parameters.AddWithValue("@Ciudad", usuario.Ciudad);
                da.UpdateCommand.Parameters.AddWithValue("@Usuario", usuario.User);
                da.UpdateCommand.Parameters.AddWithValue("@Pass", usuario.Pass);
                da.UpdateCommand.Parameters.AddWithValue("@Id", usuario.Id);

                con.Open();
                da.UpdateCommand.ExecuteNonQuery();
                con.Close();
            }

            return new
            {
                success = true,
                message = "usuario actualizado",
                result = usuario
            };
        }

        //Eliminar usuarios
        [HttpDelete]
        [Route("eliminar")]
        public dynamic eliminarUsuarios(string Id)
        {
            using (con)
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.InsertCommand = new SqlCommand("usp_EliminarUsuario", con);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                da.InsertCommand.Parameters.AddWithValue("@id", Id);

                con.Open();
                da.InsertCommand.ExecuteNonQuery();
                con.Close();
            }

            return new
            {
                success = true,
                message = "usuario eliminado",
            };
        }

    }
}