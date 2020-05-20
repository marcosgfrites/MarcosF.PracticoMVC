using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticoMVC.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectCliente()
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
    }
}