using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;
using CRUD.Models.ViewModels;

namespace CRUD.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            List<ListClienteViewModel> lst;
            using (facturaEntities db = new facturaEntities())
            {
                lst = (from d in db.cliente
                       select new ListClienteViewModel
                       {
                           id_cli = d.id_cli,
                           nombre_cli = d.nombre_cli,
                           correo_cli = d.correo_cli,
                       }).ToList();
            }
                return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Nuevo(ClienteViewModel clientemodel)
        {
            try
            {
                //Validar los data Annotations
                if(ModelState.IsValid)
                {
                    //Si todo es valido vamos a guardar los datos en la base de datos 
                    using(facturaEntities db = new facturaEntities())
                    {
                        var oCliente = new cliente();
                        oCliente.id_cli = clientemodel.id_cli;
                        oCliente.nombre_cli = clientemodel.nombre_cli;
                        oCliente.cedula_cli = clientemodel.cedula_cli;
                        oCliente.correo_cli = clientemodel.correo_cli;
                        oCliente.fechaNacimiento_cli = clientemodel.fechaNacimiento_cli;

                        db.cliente.Add(oCliente);
                        db.SaveChanges();
                    }
                    return Redirect("~/Cliente/Index");
                }
                return View(clientemodel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

    }


}