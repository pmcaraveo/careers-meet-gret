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
    public class CanalController : BaseController
    {
        private readonly CanalService _service = new CanalService();
        //private readonly CatalogoService _catService = new CatalogoService();

        // GET: canal
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: canal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CanalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var canal = model.ToCanal(User.Identity.GetUserId<int>());
                var create = _service.Create(canal);
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

        // GET: canal/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<Canal> getCanal = _service.GetById(id);
            if (!getCanal.Succeeded)
            {
                MessageDanger(getCanal.Message);
                return View();
            }
            var viewModel = new CanalViewModel()
            {
                Id = getCanal.Entity.Id,
                Nombre = getCanal.Entity.Nombre,
                //FechaCreacion = getCanal.Entity.FechaCreacion.DateTime.Now,
                Activo = getCanal.Entity.Activo
            };
            return View(viewModel);
        }

        // POST: Area/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CanalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var canal = new Canal()
                {
                    Id = model.Id,
                    Nombre = model.Nombre.ToUpper(),
                    Activo = model.Activo,
                    FechaActualizacion = DateTime.Now,
                    UsuarioActualizoId = User.Identity.GetUserId<int>()
                };

                var update = _service.Update(canal);

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

        public ActionResult Details(int id)
        {
            OperationResult<Canal> getCanal = _service.GetById(id);
            if (!getCanal.Succeeded)
            {
                MessageDanger(getCanal.Message);
                return View();
            }
            return View(getCanal.Entity);
        }

        public ActionResult Delete(int id)
        {
            var getCanal = _service.GetById(id);
            if (!getCanal.Succeeded)
            {
                MessageDanger(getCanal.Message);
                return View();
            }
            return View(getCanal.Entity);
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


        public DataTablesResult<CanalDataTable> GetPagedCanal(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new CanalDataTable()
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