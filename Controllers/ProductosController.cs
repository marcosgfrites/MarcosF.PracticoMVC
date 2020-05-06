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
    public class ProductosController : Controller
    {
        // obtengo el listado de productos activos (stock)
        public ActionResult ConsultaProducto()
        {
            ProductosQuerys pq = new ProductosQuerys();
            List<Productos> prod = new List<Productos>();
            MarcasQuerys mq = new MarcasQuerys();
            List<Marcas> mar = new List<Marcas>();
            mar = mq.GetMarcas();
            prod = pq.GetProductos();
            ViewBag.Lista = prod;
            ViewBag.ListaMarcas = mar;

            return View();
        }

        // preparo el formulario para registrar con los selects necesarios
        [HttpGet]
        public ActionResult RegistroProducto()
        {
            MarcasQuerys mq = new MarcasQuerys();
            List<Marcas> marcas = new List<Marcas>();
            marcas = mq.GetMarcas();
            ViewBag.Lista = marcas;

            return View();
        }

        // valido el formulario para proceder al registro
        [HttpPost]
        public ActionResult RegistroProducto(ProductoModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido
            if (ModelState.IsValid)
            {
                try
                {
                    ProductosQuerys pq = new ProductosQuerys();
                    Productos entidad = new Productos();
                    entidad.Nombre = modelo.Nombre;
                    entidad.Descripcion = modelo.Descripcion;
                    entidad.IdMarca = modelo.IdMarca;
                    entidad.Activo = modelo.Activo;
                    entidad.PrecioUnitario = modelo.PrecioUnitario;
                    entidad.UrlImange = modelo.UrlImange;
                    var existe = pq.ExisteNombre(entidad.Nombre); //busco el producto por nombre
                    if(existe == true) //si el producto existe
                    {
                        exito = 0;
                        ViewBag.Class = "alert alert-warning";
                        ViewBag.Message = "El producto que intenta registrar ya existe.";
                    }
                    else //si no existe, continuo con la inserción
                    {
                        var obj = pq.InsertProducto(entidad); //inserto el producto
                        if (obj == true) //si se pudo insertar
                        {
                            exito = 1;
                            ViewBag.Class = "alert alert-success";
                            ViewBag.Message = "Producto registrado correctamente!";
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

                MarcasQuerys mq = new MarcasQuerys();
                List<Marcas> marcas = new List<Marcas>();
                marcas = mq.GetMarcas();
                ViewBag.Lista = marcas;

                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();

                    MarcasQuerys mq = new MarcasQuerys();
                    List<Marcas> marcas = new List<Marcas>();
                    marcas = mq.GetMarcas();
                    ViewBag.Lista = marcas;

                    return View();
                }
                else
                {
                    MarcasQuerys mq = new MarcasQuerys();
                    List<Marcas> marcas = new List<Marcas>();
                    marcas = mq.GetMarcas();
                    ViewBag.Lista = marcas;
                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }

        // preparo el formulario para eliminar
        [HttpGet]
        public ActionResult EliminaProducto(int codigo = 0)
        {
            ProductoModelo model = new ProductoModelo();
            Productos entidad = new Productos();
            List<Productos> datosProducto = new List<Productos>();
            entidad.Codigo = codigo;
            ProductosQuerys pq = new ProductosQuerys();
            datosProducto = pq.ProductoPorCodigo(entidad.Codigo);

            MarcasQuerys mq = new MarcasQuerys();
            List<Marcas> marcas = new List<Marcas>();
            marcas = mq.GetMarcas();
            ViewBag.Lista = marcas;

            foreach (var datos in datosProducto)
            {
                model.Codigo = datos.Codigo;
                model.Nombre = datos.Nombre;
                model.Descripcion = datos.Descripcion;
                model.IdMarca = datos.IdMarca;

                ViewBag.MarcaSeleccionada = model.IdMarca;
                
                model.PrecioUnitario = datos.PrecioUnitario;
                model.Activo = datos.Activo;
                if (string.IsNullOrEmpty(datos.UrlImange))
                {
                    model.UrlImange = "Este producto no tiene URL, de imagen, asociada.";
                }
                else
                {
                    model.UrlImange = datos.UrlImange;
                }
            }

            return View(model);
        }

        // valido el formulario para proceder con la eliminación
        [HttpPost]
        public ActionResult EliminaProducto(ProductoModelo modelo)
        {
            int exito = 2; // es el valor cuando el modelo no es valido

            MarcasQuerys mq = new MarcasQuerys();

            if (ModelState.IsValid) //si se cumplen todas las validaciones
            {
                try
                {
                    ProductosQuerys pq = new ProductosQuerys();
                    Productos entidad = new Productos();
                    entidad.Codigo = modelo.Codigo;
                    var obj = pq.DeleteProducto(entidad.Codigo);
                    if (obj == true)
                    {
                        exito = 1;
                        ViewBag.Class = "alert alert-success";
                        ViewBag.Message = "Producto eliminado correctamente!";
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

                List<Marcas> marcas = new List<Marcas>();
                marcas = mq.GetMarcas();
                ViewBag.Lista = marcas;

                return View();
            }
            else
            {
                if (exito == 0)
                {
                    ModelState.Clear();

                    List<Marcas> marcas = new List<Marcas>();
                    marcas = mq.GetMarcas();
                    ViewBag.Lista = marcas;

                    return View();
                }
                else
                {
                    List<Marcas> marcas = new List<Marcas>();
                    marcas = mq.GetMarcas();
                    ViewBag.Lista = marcas;

                    ViewBag.Class = "alert alert-warning";
                    ViewBag.Message = "Faltan datos por ingresar! Controle todos los campos que son obligatorios.";

                    return View(modelo);
                }
            }
        }

    }
}