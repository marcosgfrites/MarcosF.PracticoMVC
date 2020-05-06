using Dapper;
using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace AccesoDatos
{
    public class UsuariosQuerys
    {
        public List<Usuarios> GetUsuarios()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Usuarios> listado = new List<Usuarios>();
            listado = con.Query<Usuarios>("SELECT Id,IdRol,Usuario,Nombre,Apellido,Password,PasswordSalt,FechaCreacion,Activo FROM Usuarios WHERE Activo=1 ORDER BY FechaCreacion DESC").ToList();

            return listado;
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var cripto = new HMACSHA512())
            {
                passwordHash = cripto.Key;
                passwordSalt = cripto.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool InsertUsuario(Usuarios usuario)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);

            byte[] passwordHash, passwordSalt;
            CrearPasswordHash(usuario.Password, out passwordHash, out passwordSalt);
            string pass_hash = Convert.ToBase64String(passwordHash);
            string pass_salt = Convert.ToBase64String(passwordSalt);


            int nuevo = con.Execute("INSERT INTO Usuarios(IdRol,Usuario,Nombre,Apellido,Password,PasswordSalt,FechaCreacion,Activo) VALUES(@IdRol,@Usuario,@Nombre,@Apellido,@Password,@PasswordSalt,@FechaCreacion,@Activo)",
                new { IdRol = usuario.IdRol, Usuario = usuario.Usuario, Nombre = usuario.Nombre, Apellido = usuario.Apellido, Password = usuario.Password, PasswordSalt = passwordSalt, FechaCreacion = DateTime.Now, Activo = usuario.Activo });
            if (nuevo > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}