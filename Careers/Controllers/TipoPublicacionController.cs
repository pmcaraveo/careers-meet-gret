using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Careers.Domain;
using Careers.Domain.Services;
using Careers.Helpers;
using Careers.Models;
using Careers.Models.Display;
using Microsoft.AspNet.Identity;
using Mvc.JQuery.DataTables;
using MVC5.Helpers;

namespace Careers.Controllers
{
    public class TipoPublicacionController : BaseController
    {
        private readonly TipoPublicacionService _service = new TipoPublicacionService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: TipoPublicacion
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        // GET: TipoPublicacion/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<TipoPublicacion> getTipoPub = _service.GetById(id);
            if (!getTipoPub.Succeeded)
            {
                MessageDanger(getTipoPub.Message);
                return View();
            }
            return View(getTipoPub.Entity);
        }

        // GET: TipoPublicacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPublicacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoPublicacion model)
        {
            if (ModelState.IsValid)
            {
                var tipoPub = new TipoPublicacion()
                {
                    Nombre = model.Nombre,
                    UsuarioCreoId = User.Identity.GetUserId<int>(),
                    Activo = model.Activo
                };
                var create = _service.Create(tipoPub);
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
            return View(model);
        }

        // GET: TipoPublicacion/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<TipoPublicacion> getTipoPub = _service.GetById(id);
            if (!getTipoPub.Succeeded)
            {
                MessageDanger(getTipoPub.Message);
                return View();
            }
            var viewModel = new TipoPublicacionViewModel()
            {
                Id = getTipoPub.Entity.Id,
                Nombre = getTipoPub.Entity.Nombre,
                Activo = getTipoPub.Entity.Activo
            };
            return View(viewModel);
        }

        // POST: TipoPublicacion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoPublicacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tipoPub = new TipoPublicacion()
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo,
                    UsuarioActualizoId = User.Identity.GetUserId<int>()
                };

                var update = _service.Update(tipoPub);

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
            return View(model);
        }

        // GET: TipoPublicacion/Delete/5
        public ActionResult Delete(int id)
        {
            var getTipoPub = _service.GetById(id);
            if (!getTipoPub.Succeeded)
            {
                MessageDanger(getTipoPub.Message);
                return View();
            }
            return View(getTipoPub.Entity);
        }

        // POST: TipoPublicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var eliminar = _service.Delete(id);
            if (eliminar.Succeeded)
            {
                MessageSuccess(eliminar.Message);
                return RedirectToAction("Index");
            }

            MessageDanger(eliminar.Message);
            return View();
        }

        public DataTablesResult<TipoPublicacionDataTable> GetPagedTipoPublicacion(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new TipoPublicacionDataTable()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Activo = x.Activo
            });

            return DataTablesResult.Create(data, dtParams, x => new
            {
                Tools = DataTablesButtons.GetEditButton(Url.Action("Edit", new { id = x.Id })) +
                        DataTablesButtons.GetDeleteButton(Url.Action("Delete", new { id = x.Id })) +
                        DataTablesButtons.GetDetailsButton(Url.Action("Details", new { id = x.Id })),
                Activo = x.Activo ? "SI" : "NO"
            });
        }
    }
}
