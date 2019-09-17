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
    public class AreaController : BaseController
    {
        private readonly AreaService _service = new AreaService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: Area
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        // GET: Area/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<Area> getArea = _service.GetById(id);
            if (!getArea.Succeeded)
            {
                MessageDanger(getArea.Message);
                return View();
            }
            return View(getArea.Entity);
        }

        // GET: Area/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Area/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AreaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var area = new Area()
                {
                    Nombre = model.Nombre,
                    UsuarioCreoId = User.Identity.GetUserId<int>(),
                    Activo = model.Activo
                };
                var create = _service.Create(area);
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

        // GET: Area/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<Area> getArea = _service.GetById(id);
            if (!getArea.Succeeded)
            {
                MessageDanger(getArea.Message);
                return View();
            }
            var viewModel = new AreaViewModel()
            {
                Id = getArea.Entity.Id,
                Nombre = getArea.Entity.Nombre,
                Activo = getArea.Entity.Activo
            };
            return View(viewModel);
        }

        // POST: Area/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AreaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var area = new Area()
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo,
                    UsuarioActualizoId = User.Identity.GetUserId<int>()
                };

                var update = _service.Update(area);

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

        // GET: Area/Delete/5
        public ActionResult Delete(int id)
        {
            var getArea = _service.GetById(id);
            if (!getArea.Succeeded)
            {
                MessageDanger(getArea.Message);
                return View();
            }
            return View(getArea.Entity);
        }

        // POST: Area/Delete/5
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

        public DataTablesResult<AreaDataTable> GetPagedArea(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new AreaDataTable()
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
