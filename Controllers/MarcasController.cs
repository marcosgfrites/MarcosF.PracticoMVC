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
    public class MarcasController : Controller
    {
        // obtengo el listado de todas las marcas
        public ActionResult ConsultaMarcas()
        {
            MarcasQuerys mq = new MarcasQuerys();
            List<Marcas> marcas = new List<Marcas>();
            marcas = mq.GetMarcas();

            ViewBag.ListaMarcas = marcas;

            return View();
        }

        // preparo el formulario para registrar
        [HttpGet]
        public ActionResult RegistroMarca()
        {
            return View();
        }

        // valido el formulario para proceder al registro
        [HttpPost]
        public ActionResult RegistroMarca(MarcaModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido
            if(ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    MarcasQuerys mq = new MarcasQuerys();
                    Marcas entidad = new Marcas();
                    entidad.Nombre = modelo.Nombre;
                    var existe = mq.ExisteNombre(entidad.Nombre); //busco la marca por nombre
                    if (existe == true) //si la marca ya existe
                    {
                        exito = 0;
                        ViewBag.Class = "alert alert-warning";
                        ViewBag.Message = "La marca que intenta registrar ya existe.";
                    }
                    else //si no existe, continuo con la inserción
                    {
                        var obj = mq.InsertMarca(entidad); //inserto la marca
                        if (obj == true) //si se pudo insertar
                        {
                            exito = 1;
                            ViewBag.Class = "alert alert-success";
                            ViewBag.Message = "Marca registrada correctamente!";
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
        public ActionResult EliminaMarca(int codigo = 0)
        {
            MarcaModelo model = new MarcaModelo();
            Marcas entidad = new Marcas();
            List<Marcas> datosMarca = new List<Marcas>();
            entidad.Id = codigo;
            MarcasQuerys mq = new MarcasQuerys();
            datosMarca = mq.MarcaPorCodigo(entidad.Id);

            foreach (var datos in datosMarca)
            {
                model.Id = datos.Id;
                model.Nombre = datos.Nombre;
            }

            return View(model);
        }

        // valido el formulario para proceder con la eliminación
        [HttpPost]
        public ActionResult EliminaMarca(MarcaModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            MarcasQuerys mq = new MarcasQuerys();

            if (ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    Marcas entidad = new Marcas();
                    entidad.Id = modelo.Id;
                    var obj = mq.DeleteMarca(entidad.Id);
                    if (obj == true)
                    {
                        exito = 1;
                        ViewBag.Class = "alert alert-success";
                        ViewBag.Message = "Marca eliminada correctamente!";
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

        //preparo el formulario para la modificación
        [HttpGet]
        public ActionResult EditarMarca(int codigo = 0)
        {
            MarcaModelo model = new MarcaModelo();
            Marcas entidad = new Marcas();
            List<Marcas> datosMarca = new List<Marcas>();
            entidad.Id = codigo;
            MarcasQuerys mq = new MarcasQuerys();
            datosMarca = mq.MarcaPorCodigo(entidad.Id);

            foreach(var datos in datosMarca)
            {
                model.Id = datos.Id;
                model.Nombre = datos.Nombre;
            }

            return View(model);
        }

        //valido el formulario y procedo con la modificación
        [HttpPost]
        public ActionResult EditarMarca(MarcaModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            MarcasQuerys mq = new MarcasQuerys();

            if (ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    Marcas entidad = new Marcas();
                    entidad.Id = modelo.Id;
                    entidad.Nombre = modelo.Nombre;
                    var obj = mq.UpdateMarca(entidad.Id,entidad.Nombre);
                    if (obj == true)
                    {
                        exito = 1;
                        ViewBag.Class = "alert alert-success";
                        ViewBag.Message = "Marca actualizada correctamente!";
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