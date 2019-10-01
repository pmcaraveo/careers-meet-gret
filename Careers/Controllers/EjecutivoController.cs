using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Careers.Domain;
using Careers.Models;
using Mvc.JQuery.DataTables;
using MVC5.Helpers;
using Careers.Helpers;

namespace Careers.Controllers
{
    public class EjecutivoController : BaseController
    {
        private readonly EjecutivoService _service = new EjecutivoService();
        private readonly CatalogoService _catService = new CatalogoService();
               
        public ActionResult Index()
        {
            ViewBag.Dato = _service.ListaPerfil();
            return View();
        }

        public ActionResult Historial()
        {
            ViewBag.Datos = _service.ListaHistorial();
            return View();
        }

        public ActionResult Dash()
        {
            ViewBag.Datos = _service.ListaHistorial();
            return View();
        }


        public ActionResult Home()
        {
            ViewBag.Dato = _service.ListaHomePerfil();
            return View();
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            return View(carga());
        }


        public ActionResult Confirmacion(int id)
        {
            OperationResult<Ejecutivos> getEjecutivo = _service.GetById(id);
            if (!getEjecutivo.Succeeded)
            {
                MessageDanger(getEjecutivo.Message);
                return View();
            }

            return View(getEjecutivo.Entity);
        }


        public ActionResult getApoyo(int ApoyoId)
        {
            var data = _service.getApoyoId(ApoyoId).Select(x => new ColViewModel()
            {
               Id = x.Id,
               Nombre = x.Nombre,
               Email = x.Email,
               Telefono = x.Telefono
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // POST: Estado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EjecutivoViewModel model)
        {

            if (ModelState.IsValid)
            {
          
                var ejecutivo = model.ToEjecutivo();
                var create = _service.Create(ejecutivo);
                if (create.Succeeded)
                {
                    MessageSuccess(create.Message);
                    return RedirectToAction("Index");
                }
                else
                {
                    MessageDanger(create.Message);
                }
            }
            model = carga(model);
            return View(model);
        }


        // GET: Estado/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<Ejecutivos> getEjecutivo = _service.GetById(id);
            if (!getEjecutivo.Succeeded)
            {
                MessageDanger(getEjecutivo.Message);
                return View();
            }

            return View(getEjecutivo.Entity);
        }

        public ActionResult Edit(int id)
        {
            OperationResult<Ejecutivos> getEjecutivo = _service.GetById(id);
            if (!getEjecutivo.Succeeded)
            {
                MessageDanger(getEjecutivo.Message);
                return View();
            }
            var viewModel = new EjecutivoViewModel();
            var model = viewModel.ToViewModel(getEjecutivo.Entity);
            model = carga(model);
            return View(model);
        }



        //// POST: Estado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EjecutivoViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var eje = new Ejecutivos()
                //{
                //    Id = model.Id,
                //    Nombre = model.Nombre,
                  
                //  //  UsuarioActualizoId = User.Identity.GetUserId<int>()
                //};

                var eje = model.ToEjecutivo();
                var update = _service.Update(eje);
                if (update.Succeeded)
                {
                    MessageSuccess(update.Message);
                    return RedirectToAction("Index");
                }
                else
                {
                    MessageDanger(update.Message);
                }
            }
            model = carga(model);
            return View(model);
        }

        //// GET: Estado/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var getEstado = _service.GetById(id);
        //    if (!getEstado.Succeeded)
        //    {
        //        MessageDanger(getEstado.Message);
        //        return View();
        //    }
        //    return View(getEstado.Entity);
        //}

        //// POST: Estado/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var eliminar = _service.Delete(id);
        //    if (eliminar.Succeeded)
        //    {
        //        MessageSuccess(eliminar.Message);
        //        return RedirectToAction("Index");
        //    };
        //    MessageDanger(eliminar.Message);
        //    return View();
        //}

        #region Helpers

        private EjecutivoViewModel carga(EjecutivoViewModel ejecutivo = null)
        {
            if (ejecutivo == null)
            {
                ejecutivo = new EjecutivoViewModel();
            }
            ejecutivo.Empresa = new MySelectList(_catService.GetEmpresasForSelectList());
            ejecutivo.NivelOrganizacional = new MySelectList(_catService.GetNivelOrgForSelectList());
            ejecutivo.Apoyo = new MySelectList(_catService.GetColaboradorForSelectList());
            ejecutivo.Canal = new MySelectList(_catService.GetTipoCanalSelectList());
            
            return ejecutivo;
        }
        private EjecutivoViewModel getApoyo(EjecutivoViewModel ejecutivo = null)
        {
            if (ejecutivo == null)
            {
                ejecutivo = new EjecutivoViewModel();
            }
     
            ejecutivo.Apoyo = new MySelectList(_catService.GetColaboradorForSelectList());
            return ejecutivo;
        }

        //private EjecutivoViewModel cargaNivel(EjecutivoViewModel ejecutivo = null)
        //{
        //    if (ejecutivo == null)
        //    {
        //        ejecutivo = new EjecutivoViewModel();
        //    }
        //    ejecutivo.NivelOrganizacional = new MySelectList(_catService.GetNivelOrgForSelectList());
        //    return ejecutivo;
        //}

        //public DataTablesResult<EjecutivoDataTable> GetPagedEjecutivo(DataTablesParam dtParams)
        //{
        //    var data = _service.GetPage().Select(x => new EjecutivoDataTable()
        //    {
        //        Id = x.Id,
        //        Nombre = x.Nombre,
        //        Empresa = x.Empresa.Nombre,
        //        NivelOrganizacional = x.NivelOrganizacional.Nombre,
        //        Descripcion = x.Descripcion
        //    });

        //    return DataTablesResult.Create(data, dtParams, x => new
        //    {
        //        Tools = DataTablesButtons.GetEditButton(Url.Action("Edit", new { id = x.Id })) +
        //                DataTablesButtons.GetDeleteButton(Url.Action("Delete", new { id = x.Id })) +
        //                DataTablesButtons.GetDetailsButton(Url.Action("Details", new { id = x.Id })),
        //        //Activo = x.Activo ? "SI" : "NO"
        //    });
        //}
        #endregion
    }
}