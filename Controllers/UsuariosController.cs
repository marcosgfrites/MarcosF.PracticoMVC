using AccesoDatos;
using Entidades;
using PracticoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticoMVC.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Account
        public ActionResult IngresoUsuario()
        {
            return View();
        }

        public ActionResult ConsultaUsuarios()
        {
            UsuariosQuerys uq = new UsuariosQuerys();
            List<Usuarios> usuarios = new List<Usuarios>();
            RolesQuerys rq = new RolesQuerys();
            List<Roles> roles = new List<Roles>();
            roles = rq.GetRoles();
            usuarios = uq.GetUsuarios();

            ViewBag.ListaUsuarios = usuarios;
            ViewBag.ListaRoles = roles;

            return View();
        }

        // preparo el formulario para el registro
        [HttpGet]
        public ActionResult RegistroUsuario()
        {
            RolesQuerys rq = new RolesQuerys();
            List<Roles> roles = new List<Roles>();
            roles = rq.GetRoles();

            ViewBag.ListaRoles = roles;

            return View();
        }

        // valido el formulario para proceder al registro
        [HttpPost]
        public ActionResult RegistroUsuario(UsuarioModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            RolesQuerys rq = new RolesQuerys();

            if (ModelState.IsValid)
            {
                try
                {
                    UsuariosQuerys uq = new UsuariosQuerys();
                    Usuarios entidad = new Usuarios();
                    entidad.IdRol = modelo.IdRol;
                    entidad.Usuario = modelo.Usuario;
                    entidad.Nombre = modelo.Nombre;
                    entidad.Apellido = modelo.Apellido;
                    entidad.Password = modelo.Password;
                    entidad.Activo = modelo.Activo;
                    var existe = uq.ExisteUsuario(entidad.Usuario); //busco el usuario por nombre y apellido
                    if (existe == true) //si el usuario existe
                    {
                        exito = 0;
                        ViewBag.Class = "alert alert-warning";
                        ViewBag.Message = "El usuario que intenta registrar ya existe.";
                    }
                    else //si no existe, continuo con la inserción
                    {
                        var obj = uq.InsertUsuario(entidad); //inserto el producto
                        if (obj == true) //si se pudo insertar
                        {
                            exito = 1;
                            ViewBag.Class = "alert alert-success";
                            ViewBag.Message = "Usuario registrado correctamente!";
                        }
                        else //si no se pudo insertar, el error está en el método o la conexión a la DB
                        {
                            exito = 0;
                            ViewBag.Class = "alert alert-danger";
                            ViewBag.Message = "Oops! Algo ha ocurrido!";
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            //maneja la vista, según la variable éxito
            if (exito == 1)
            {
                ModelState.Clear();

                List<Roles> roles = new List<Roles>();
                roles = rq.GetRoles();

                ViewBag.ListaRoles = roles;

                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();

                    List<Roles> roles = new List<Roles>();
                    roles = rq.GetRoles();

                    ViewBag.ListaRoles = roles;

                    return View();
                }
                else
                {
                    List<Roles> roles = new List<Roles>();
                    roles = rq.GetRoles();

                    ViewBag.ListaRoles = roles;

                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }

        // preparo el formulario para eliminar
        [HttpGet]
        public ActionResult EliminaUsuario(int codigo = 0)
        {
            UsuarioModeloEliminar model = new UsuarioModeloEliminar();
            Usuarios entidad = new Usuarios();
            List<Usuarios> datosUsuario = new List<Usuarios>();
            entidad.Id = codigo;
            UsuariosQuerys uq = new UsuariosQuerys();
            datosUsuario = uq.UsuarioPorCodigo(entidad.Id);

            RolesQuerys rq = new RolesQuerys();
            List<Roles> roles = new List<Roles>();
            roles = rq.GetRoles();
            ViewBag.ListaRoles = roles;

            foreach (var datos in datosUsuario)
            {
                model.Id = datos.Id;
                model.IdRol = datos.IdRol;
                model.Usuario = datos.Usuario;
                model.Nombre = datos.Nombre;
                model.Apellido = datos.Apellido;
                model.Activo = datos.Activo;
            }

            return View(model);
        }

        // valido el formulario y procedo a la eliminacion
        [HttpPost]
        public ActionResult EliminaUsuario(UsuarioModeloEliminar modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            RolesQuerys rq = new RolesQuerys();

            if (ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    UsuariosQuerys uq = new UsuariosQuerys();
                    Usuarios entidad = new Usuarios();
                    entidad.Id = modelo.Id;
                    var obj = uq.DeleteUsuario(entidad.Id);
                    if (obj == true)
                    {
                        exito = 1;
                        ViewBag.Class = "alert alert-success";
                        ViewBag.Message = "Usuario eliminado correctamente!";
                        ViewBag.Exito = 1;

                    }
                    else //si no se pudo eliminar, el error está en el método o la conexión a la DB
                    {
                        exito = 0;
                        ViewBag.Class = "alert alert-danger";
                        ViewBag.Message = "Oops! Algo ha ocurrido!";
                        ViewBag.Exito = 0;

                    }

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            //manejo la vista, según el valor de la variable exito
            if (exito == 1)
            {
                ModelState.Clear();

                List<Roles> roles = new List<Roles>();
                roles = rq.GetRoles();
                ViewBag.ListaRoles = roles;

                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();

                    List<Roles> roles = new List<Roles>();
                    roles = rq.GetRoles();
                    ViewBag.ListaRoles = roles;

                    return View();
                }
                else
                {
                    List<Roles> roles = new List<Roles>();
                    roles = rq.GetRoles();
                    ViewBag.ListaRoles = roles;

                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }

        // preparo el formulario para editar
        [HttpGet]
        public ActionResult EditaUsuario(int codigo = 0)
        {
            UsuarioModeloEditar model = new UsuarioModeloEditar();
            Usuarios entidad = new Usuarios();
            List<Usuarios> datosUsuario = new List<Usuarios>();
            entidad.Id = codigo;
            UsuariosQuerys uq = new UsuariosQuerys();
            datosUsuario = uq.UsuarioPorCodigo(entidad.Id);

            RolesQuerys rq = new RolesQuerys();
            List<Roles> roles = new List<Roles>();
            roles = rq.GetRoles();
            ViewBag.ListaRoles = roles;

            foreach (var datos in datosUsuario)
            {
                model.Id = datos.Id;
                model.IdRol = datos.IdRol;
                model.Usuario = datos.Usuario;
                model.Nombre = datos.Nombre;
                model.Apellido = datos.Apellido;
                model.Activo = datos.Activo;
            }

            return View(model);
        }

        //valido el formulario para proceder con la modificación
        [HttpPost]
        public ActionResult EditaUsuario(UsuarioModeloEditar modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            RolesQuerys rq = new RolesQuerys();

            if (ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    Usuarios entidad = new Usuarios();
                    UsuariosQuerys uq = new UsuariosQuerys();
                    entidad.Id = modelo.Id;
                    entidad.IdRol = modelo.IdRol;
                    entidad.Usuario = modelo.Usuario;
                    entidad.Nombre = modelo.Nombre;
                    entidad.Apellido = modelo.Apellido;
                    entidad.Activo = modelo.Activo;
                    var obj = uq.UpdateUsuario(entidad.Id, entidad.IdRol, entidad.Usuario, entidad.Nombre, entidad.Apellido, entidad.Activo);
                    if (obj == true)
                    {
                        exito = 1;
                        ViewBag.Class = "alert alert-success";
                        ViewBag.Message = "Usuario actualizado correctamente!";
                        ViewBag.Exito = 1;
                    }
                    else //si no se pudo modificar, el error está en el método o la conexión a la DB
                    {
                        exito = 0;
                        ViewBag.Class = "alert alert-danger";
                        ViewBag.Message = "Oops! Algo ha ocurrido!";
                        ViewBag.Exito = 0;
                    }

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            //manejo la vista, según el valor de la variable exito
            if (exito == 1)
            {
                ModelState.Clear();

                List<Roles> roles = new List<Roles>();
                roles = rq.GetRoles();
                ViewBag.ListaRoles = roles;

                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();

                    List<Roles> roles = new List<Roles>();
                    roles = rq.GetRoles();
                    ViewBag.ListaRoles = roles;

                    return View();
                }
                else
                {
                    List<Roles> roles = new List<Roles>();
                    roles = rq.GetRoles();
                    ViewBag.ListaRoles = roles;

                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }
    }
}