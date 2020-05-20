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
            listado = con.Query<Usuarios>("SELECT Id,IdRol,Usuario,Nombre,Apellido,Password,PasswordSalt,FechaCreacion,Activo FROM Usuarios WHERE Activo=1 ORDER BY Apellido ASC, Nombre ASC").ToList();

            return listado;
        }

        /**INICIO DEL MÉTODO ORIGINAL DE INSERCIÓN - ESTA SECCIÓN SE COMENTA PUESTO QUE AL UTILIZARLO EN LA DB NOS INSERTA SIMBOLOS CHINOS EN EL SALT O AL MENOS ES LO QUE SE VE**/
        //private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var cripto = new HMACSHA512())
        //    {
        //        passwordHash = cripto.Key;
        //        passwordSalt = cripto.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //public bool InsertUsuario(Usuarios usuario)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);

        //    byte[] passwordHash, passwordSalt;
        //    CrearPasswordHash(usuario.Password, out passwordHash, out passwordSalt);
        //    string pass_hash = Convert.ToBase64String(passwordHash);
        //    string pass_salt = Convert.ToBase64String(passwordSalt);

        //    int nuevo = con.Execute("INSERT INTO Usuarios(IdRol,Usuario,Nombre,Apellido,Password,PasswordSalt,FechaCreacion,Activo) VALUES(@IdRol,@Usuario,@Nombre,@Apellido,@Password,@PasswordSalt,@FechaCreacion,@Activo)",
        //        new { IdRol = usuario.IdRol, Usuario = usuario.Usuario, Nombre = usuario.Nombre, Apellido = usuario.Apellido, Password = usuario.Password, PasswordSalt = passwordSalt, FechaCreacion = DateTime.Now, Activo = usuario.Activo });
        //    if (nuevo > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        /**FIN DEL MÉTODO ORIGINAL DE INSERCIÓN**/

        /**PRUEBA 2 DE INSERCION DE USUARIOS - GENERACION DE HASH**/
        public static string HashConSalt(string password)
        {
            byte[] salt;
            byte[] buffer;
            if(password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 16, 1000, HashAlgorithmName.SHA512))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(32);
            }
            byte[] dst = new byte[49];
            Buffer.BlockCopy(salt, 0, dst, 1, 16);
            Buffer.BlockCopy(buffer, 0, dst, 17, 32);
            return Convert.ToBase64String(dst);
        }

        public bool InsertUsuario(Usuarios usuario)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);

            string passwordSalt = HashConSalt(usuario.Password);

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
        /**FIN DE LA PRUEBA 2 DE INSERCIÓN**/

        public bool ExisteUsuario(string usuario) //comprueba si existe el usuario devolviendo la cantidad de veces que existe
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            var existe = con.Query<int>("SELECT COUNT(Usuario) FROM Usuarios WHERE Usuario='" + usuario + "'");
            if (existe.First() > 0) //si la cantidad es mayor a 0, significa que si existe
            {
                return true; //si existe, devuelve true
            }
            else
            {
                return false; //si no existe, devuelve false
            }
        }

        public List<Usuarios> UsuarioPorCodigo(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Usuarios> datosUsuario = new List<Usuarios>();
            datosUsuario = con.Query<Usuarios>("SELECT Id,IdRol,Usuario,Nombre,Apellido,Password,FechaCreacion,Activo FROM Usuarios WHERE Id=@Id",
                new { Id = id }).ToList();
            return datosUsuario;
        }

        public bool DeleteUsuario(int id) //elimina un usuario de acuerdo al numero de Id
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int baja = con.Execute("DELETE FROM Usuarios WHERE Id=@Id",
                new { Id = id });
            if (baja > 0) //si la cantidad es mayor a 0, significa que se eliminó
            {
                return true; //si se eliminó, devuelve true
            }
            else
            {
                return false; //si no se pudo eliminar, devuelve false
            }
        }

        public bool UpdateUsuario(int id, string idRol, string usuario, string nombre, string apellido, int activo)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int edita = con.Execute("UPDATE Usuarios SET IdRol=@IdRol,Usuario=@Usuario,Nombre=@Nombre,Apellido=@Apellido,Activo=@Activo WHERE Id=@Id",
                new { Id = id, IdRol = idRol, Usuario = usuario , Nombre = nombre, Apellido = apellido, Activo = activo });
            if (edita > 0) //si la cantidad es mayor a 0, significa que se modificó
            {
                return true; //si se modificó, devuelve true
            }
            else
            {
                return false; //si no se pudo modificar, devuelve false
            }
        }
    }
}