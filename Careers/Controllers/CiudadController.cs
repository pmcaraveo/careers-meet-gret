using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Careers.Domain;
using Careers.Helpers;
using Careers.Models;
using Careers.Models.Display;
using Microsoft.AspNet.Identity;
using Mvc.JQuery.DataTables;
using MVC5.Helpers;

namespace Careers.Controllers
{
    public class CiudadController : BaseController
    {
        private readonly CiudadService _service = new CiudadService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: Ciudad
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        // GET: Ciudad/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<Ciudad> getCiudad = _service.GetById(id);
            if (!getCiudad.Succeeded)
            {
                MessageDanger(getCiudad.Message);
                return View();
            }
            return View(getCiudad.Entity);
        }

        // GET: Ciudad/Create
        public ActionResult Create()
        {
            return View(AddSelectListsToCiudadViewModel());
        }

        // POST: Ciudad/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CiudadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UsuarioCreo = new Ciudad()
                {
                    UsuarioCreoId = User.Identity.GetUserId<int>()
                };

                var ciudad = model.ToCiudad();
                var create = _service.Create(ciudad);
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
            model = AddSelectListsToCiudadViewModel(model);
            return View(model);
        }

        // GET: Ciudad/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<Ciudad> getCiudad = _service.GetById(id);
            if (!getCiudad.Succeeded)
            {
                MessageDanger(getCiudad.Message);
                return View();
            }
            var viewModel = new CiudadViewModel();
            var model = viewModel.ToViewModel(getCiudad.Entity);
            model = AddSelectListsToCiudadViewModel(model);
            return View(model);
        }

        // POST: Ciudad/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CiudadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ciudad = new Ciudad()
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo,
                    UsuarioActualizoId = User.Identity.GetUserId<int>()
                };
                var update = _service.Update(ciudad);
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
            model = AddSelectListsToCiudadViewModel(model);
            return View(model);
        }

        // GET: Ciudad/Delete/5
        public ActionResult Delete(int id)
        {
            var getCiudad = _service.GetById(id);
            if (!getCiudad.Succeeded)
            {
                MessageDanger(getCiudad.Message);
                return View();
            }
            return View(getCiudad.Entity);
        }

        // POST: Ciudad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var eliminar = _service.Delete(id);
            if (eliminar.Succeeded)
            {
                MessageSuccess(eliminar.Message);
                return RedirectToAction("Index");
            };
            MessageDanger(eliminar.Message);
            return View();
        }

        #region Helpers

        private CiudadViewModel AddSelectListsToCiudadViewModel(CiudadViewModel ciudad = null)
        {
            if (ciudad == null)
            {
                ciudad = new CiudadViewModel();
            }
            ciudad.Estado = new MySelectList(_catService.GetEstadosForSelectList());
            return ciudad;
        }

        public DataTablesResult<CiudadDataTable> GetPagedCiudad(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new CiudadDataTable()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Estado = x.Estado.Nombre,
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
        #endregion
    }
}
