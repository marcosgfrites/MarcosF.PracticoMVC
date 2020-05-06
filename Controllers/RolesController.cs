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
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult ConsultaRoles()
        {
            RolesQuerys rq = new RolesQuerys();
            List<Roles> roles = new List<Roles>();
            roles = rq.GetRoles();

            ViewBag.ListaRoles = roles;

            return View();
        }

        // preparo el formulario para el registro de roles
        [HttpGet]
        public ActionResult RegistroRol()
        {
            return View();
        }

        // preparo el formulario para el registro de roles
        [HttpPost]
        public ActionResult RegistroRol(RolModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido
            if (ModelState.IsValid)
            {
                try
                {
                    RolesQuerys rq = new RolesQuerys();
                    Roles entidad = new Roles();
                    entidad.Id = modelo.Id;
                    entidad.Descripcion = modelo.Descripcion;
                    var existe = rq.ExisteNombre(entidad.Id,entidad.Descripcion); //busco el rol por descripcion
                    if (existe == true) //si la marca ya existe
                    {
                        exito = 0;
                        ViewBag.Class = "alert alert-warning";
                        ViewBag.Message = "El rol que intenta registrar ya existe.";
                    }
                    else //si no existe, continuo con la inserción
                    {
                        var obj = rq.InsertRol(entidad); //inserto la marca
                        if (obj == true) //si se pudo insertar
                        {
                            exito = 1;
                            ViewBag.Class = "alert alert-success";
                            ViewBag.Message = "Rol registrado correctamente!";
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
                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }

        // preparo el formulario para eliminar
        [HttpGet]
        public ActionResult EliminaRol(string codigo = "")
        {
            RolModelo model = new RolModelo();
            Roles entidad = new Roles();
            List<Roles> datosRol = new List<Roles>();
            entidad.Id = codigo;
            RolesQuerys rq = new RolesQuerys();
            datosRol = rq.RolPorCodigo(entidad.Id);

            foreach (var datos in datosRol)
            {
                model.Id = datos.Id;
                model.Descripcion = datos.Descripcion;
            }

            return View(model);
        }

        // valido el formulario para proceder con la eliminación
        [HttpPost]
        public ActionResult EliminaRol(RolModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            RolesQuerys rq = new RolesQuerys();

            if (ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    Roles entidad = new Roles();
                    entidad.Id = modelo.Id;
                    var obj = rq.DeleteRol(entidad.Id);
                    if (obj == true)
                    {
                        exito = 1;
                        ViewBag.Class = "alert alert-success";
                        ViewBag.Message = "Rol eliminado correctamente!";
                    }
                    else //si no se pudo eliminar, el error está en el método o la conexión a la DB
                    {
                        exito = 0;
                        ViewBag.Class = "alert alert-danger";
                        ViewBag.Message = "Oops! Algo ha ocurrido!";
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
                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }

        [HttpGet]
        public ActionResult EditaRol(string codigo = "")
        {
            RolModelo model = new RolModelo();
            Roles entidad = new Roles();
            List<Roles> datosRol = new List<Roles>();
            entidad.Id = codigo;
            RolesQuerys rq = new RolesQuerys();
            datosRol = rq.RolPorCodigo(entidad.Id);

            foreach (var datos in datosRol)
            {
                model.Id = datos.Id;
                model.Descripcion = datos.Descripcion;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditaRol(RolModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            RolesQuerys rq = new RolesQuerys();

            if (ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    Roles entidad = new Roles();
                    entidad.Id = modelo.Id;
                    entidad.Descripcion = modelo.Descripcion;
                    var obj = rq.UpdateRol(entidad.Id, entidad.Descripcion);
                    if (obj == true)
                    {
                        exito = 1;
                        ViewBag.Class = "alert alert-success";
                        ViewBag.Message = "Rol actualizado correctamente!";
                    }
                    else //si no se pudo eliminar, el error está en el método o la conexión a la DB
                    {
                        exito = 0;
                        ViewBag.Class = "alert alert-danger";
                        ViewBag.Message = "Oops! Algo ha ocurrido!";
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
                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }
    }
}