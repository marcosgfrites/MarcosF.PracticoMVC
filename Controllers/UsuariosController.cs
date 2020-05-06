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

        [HttpGet]
        public ActionResult RegistroUsuario()
        {
            RolesQuerys rq = new RolesQuerys();
            List<Roles> roles = new List<Roles>();
            roles = rq.GetRoles();

            ViewBag.ListaRoles = roles;

            return View();
        }

        [HttpPost]
        public ActionResult RegistroUsuario(UsuarioModelo nuevo)
        {
            return View();
        }
    }
}