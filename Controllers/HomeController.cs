using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticoMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProductosQuerys pq = new ProductosQuerys();
            MarcasQuerys mq = new MarcasQuerys();

            List<Productos> prodNuevos = new List<Productos>();
            prodNuevos = pq.GetCincoNovedades();

            List<Productos> prodPrecioMax = new List<Productos>();
            prodPrecioMax = pq.GetProductoMayorPrecio();

            List<Marcas> marcas = new List<Marcas>();
            marcas = mq.GetMarcas();

            ViewBag.Novedades = prodNuevos;
            ViewBag.MayorPrecio = prodPrecioMax;
            ViewBag.ListaMarcas = marcas;

            return View();
        }
    }
}