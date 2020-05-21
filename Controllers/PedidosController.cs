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
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
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

        [HttpPost]
        public ActionResult SelectCliente(PedidoBuscaClienteModelo modelo)
        {
            ClientesQuerys cq = new ClientesQuerys();
            List<Clientes> clientes = new List<Clientes>();
            Clientes entidad = new Clientes();
            entidad.RazonSocial = modelo.Cliente;
            clientes = cq.ClientePorNombre(entidad.RazonSocial);

            UsuariosQuerys uq = new UsuariosQuerys();
            List<Usuarios> usuarios = new List<Usuarios>();
            usuarios = uq.GetUsuarios();

            ViewBag.ListaClientes = clientes;
            ViewBag.ListaUsuarios = usuarios;

            return View();
        }

        [HttpGet]
        public ActionResult RegistroPedido(int codigo = 0)
        {
            PedidoModelo model = new PedidoModelo();
            Clientes entidad_c = new Clientes();
            List<Clientes> datosCliente = new List<Clientes>();
            entidad_c.Codigo = codigo;
            ClientesQuerys cq = new ClientesQuerys();
            datosCliente = cq.ClientePorCodigo(entidad_c.Codigo);

            foreach (var datos in datosCliente)
            {
                model.CodigoCliente = datos.Codigo;
                model.RazonSocialCliente = datos.RazonSocial;
            }

            PedidosQuerys pq = new PedidosQuerys();
            List<Pedidos> datosPedidos = new List<Pedidos>();
            datosPedidos = pq.UltimoPedido();
            int nuevoPedido = 0;
            foreach (var pedido in datosPedidos)
            {
                nuevoPedido = pedido.NumeroPedido + 1;
            }

            model.NumeroPedido = nuevoPedido;

            return View(model);
        }
    }
}