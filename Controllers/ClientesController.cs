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
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsultaClientes()
        {
            ClientesQuerys cq = new ClientesQuerys();
            List<Clientes> clientes = new List<Clientes>();
            clientes = cq.GetClientes();
            UsuariosQuerys uq = new UsuariosQuerys();
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = uq.GetUsuarios();

            ViewBag.ListaClientes = clientes;
            ViewBag.ListaUsuarios = usuarios;

            return View();
        }

        // preparo el formulario para el registro
        [HttpGet]
        public ActionResult RegistroCliente()
        {
            UsuariosQuerys uq = new UsuariosQuerys();
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = uq.GetUsuarios();

            ViewBag.ListaUsuarios = usuarios;

            return View();
        }

        // valido el formulario para proceder al registro
        [HttpPost]
        public ActionResult RegistroCliente(ClienteModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            UsuariosQuerys uq = new UsuariosQuerys();

            if (ModelState.IsValid)
            {
                try
                {
                    ClientesQuerys cq = new ClientesQuerys();
                    Clientes entidad = new Clientes();
                    entidad.RazonSocial = modelo.RazonSocial;
                    entidad.IdUsuario = modelo.IdUsuario;
                    var existe = cq.ExisteCliente(entidad.RazonSocial); //busco el cliente por razón social
                    if (existe == true) //si el cliente existe
                    {
                        exito = 0;
                        ViewBag.Class = "alert alert-warning";
                        ViewBag.Message = "El cliente que intenta registrar ya existe.";
                    }
                    else //si no existe, continuo con la inserción
                    {
                        var obj = cq.InsertCliente(entidad); //inserto el producto
                        if (obj == true) //si se pudo insertar
                        {
                            exito = 1;
                            ViewBag.Class = "alert alert-success";
                            ViewBag.Message = "Cliente registrado correctamente!";
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

                List<Usuarios> usuarios = new List<Usuarios>();
                usuarios = uq.GetUsuarios();

                ViewBag.ListaUsuarios = usuarios;

                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();

                    List<Usuarios> usuarios = new List<Usuarios>();
                    usuarios = uq.GetUsuarios();

                    ViewBag.ListaUsuarios = usuarios;

                    return View();
                }
                else
                {
                    List<Usuarios> usuarios = new List<Usuarios>();
                    usuarios = uq.GetUsuarios();

                    ViewBag.ListaUsuarios = usuarios;

                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }

        // preparo el formulario para eliminar
        [HttpGet]
        public ActionResult EliminaCliente(int codigo = 0)
        {
            ClienteModelo model = new ClienteModelo();
            Clientes entidad = new Clientes();
            List<Clientes> datosCliente = new List<Clientes>();
            entidad.Codigo = codigo;
            ClientesQuerys cq = new ClientesQuerys();
            datosCliente = cq.ClientePorCodigo(entidad.Codigo);

            UsuariosQuerys uq = new UsuariosQuerys();
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = uq.GetUsuarios();
            ViewBag.ListaUsuarios = usuarios;

            foreach (var datos in datosCliente)
            {
                model.Codigo = datos.Codigo;
                model.RazonSocial = datos.RazonSocial;
                model.IdUsuario = datos.IdUsuario;
            }

            return View(model);
        }

        // valido el formulario y procedo a la eliminacion
        [HttpPost]
        public ActionResult EliminaCliente(ClienteModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            UsuariosQuerys uq = new UsuariosQuerys();

            if (ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    ClientesQuerys cq = new ClientesQuerys();
                    Clientes entidad = new Clientes();
                    entidad.Codigo = modelo.Codigo;
                    var obj = cq.DeleteCliente(entidad.Codigo);
                    if (obj == true)
                    {
                        exito = 1;
                        ViewBag.Class = "alert alert-success";
                        ViewBag.Message = "Cliente eliminado correctamente!";
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

                List<Usuarios> usuarios = new List<Usuarios>();
                usuarios = uq.GetUsuarios();
                ViewBag.ListaUsuarios = usuarios;

                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();

                    List<Usuarios> usuarios = new List<Usuarios>();
                    usuarios = uq.GetUsuarios();
                    ViewBag.ListaUsuarios = usuarios;

                    return View();
                }
                else
                {
                    List<Usuarios> usuarios = new List<Usuarios>();
                    usuarios = uq.GetUsuarios();
                    ViewBag.ListaUsuarios = usuarios;

                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }

        // preparo el formulario para editar
        [HttpGet]
        public ActionResult EditaCliente(int codigo = 0)
        {
            ClienteModelo model = new ClienteModelo();
            Clientes entidad = new Clientes();
            List<Clientes> datosCliente = new List<Clientes>();
            entidad.Codigo = codigo;
            ClientesQuerys cq = new ClientesQuerys();
            datosCliente = cq.ClientePorCodigo(entidad.Codigo);

            UsuariosQuerys uq = new UsuariosQuerys();
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = uq.GetUsuarios();
            ViewBag.ListaUsuarios = usuarios;

            foreach (var datos in datosCliente)
            {
                model.Codigo = datos.Codigo;
                model.RazonSocial = datos.RazonSocial;
                model.IdUsuario = datos.IdUsuario;
            }

            return View(model);
        }

        //valido el formulario para proceder con la modificación
        [HttpPost]
        public ActionResult EditaCliente(ClienteModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            UsuariosQuerys uq = new UsuariosQuerys();

            if (ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    Clientes entidad = new Clientes();
                    ClientesQuerys cq = new ClientesQuerys();
                    entidad.Codigo = modelo.Codigo;
                    entidad.RazonSocial = modelo.RazonSocial;
                    entidad.IdUsuario = modelo.IdUsuario;
                    var obj = cq.UpdateCliente(entidad.Codigo, entidad.RazonSocial, entidad.IdUsuario);
                    if (obj == true)
                    {
                        exito = 1;
                        ViewBag.Class = "alert alert-success";
                        ViewBag.Message = "Cliente actualizado correctamente!";
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

                List<Usuarios> usuarios = new List<Usuarios>();
                usuarios = uq.GetUsuarios();
                ViewBag.ListaUsuarios = usuarios;

                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();

                    List<Usuarios> usuarios = new List<Usuarios>();
                    usuarios = uq.GetUsuarios();
                    ViewBag.ListaUsuarios = usuarios;

                    return View();
                }
                else
                {
                    List<Usuarios> usuarios = new List<Usuarios>();
                    usuarios = uq.GetUsuarios();
                    ViewBag.ListaUsuarios = usuarios;

                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }
    }
}