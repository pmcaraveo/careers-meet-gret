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
    public class NivelOrganizacionalController : BaseController
    {
        private readonly NivelOrganizacionalService _service = new NivelOrganizacionalService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: NivelOrganizacional
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        // GET: NivelOrganizacional/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<NivelOrganizacional> getNivelOrg = _service.GetById(id);
            if (!getNivelOrg.Succeeded)
            {
                MessageDanger(getNivelOrg.Message);
                return View();
            }
            return View(getNivelOrg.Entity);
        }

        // GET: NivelOrganizacional/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NivelOrganizacional/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NivelOrganizacionalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nivelOrg = new NivelOrganizacional()
                {
                    Nombre = model.Nombre,
                    UsuarioCreoId = User.Identity.GetUserId<int>(),
                    Activo = model.Activo
                };
                var create = _service.Create(nivelOrg);
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

        // GET: NivelOrganizacional/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<NivelOrganizacional> getNivelOrg= _service.GetById(id);
            if (!getNivelOrg.Succeeded)
            {
                MessageDanger(getNivelOrg.Message);
                return View();
            }
            var viewModel = new NivelOrganizacionalViewModel()
            {
                Id = getNivelOrg.Entity.Id,
                Nombre = getNivelOrg.Entity.Nombre,
                Activo = getNivelOrg.Entity.Activo
            };
            return View(viewModel);
        }

        // POST: NivelOrganizacional/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NivelOrganizacionalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nivelOrg = new NivelOrganizacional()
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo,
                    UsuarioActualizoId = User.Identity.GetUserId<int>()
                };

                var update = _service.Update(nivelOrg);

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

        // GET: NivelOrganizacional/Delete/5
        public ActionResult Delete(int id)
        {
            var getNivelOrg = _service.GetById(id);
            if (!getNivelOrg.Succeeded)
            {
                MessageDanger(getNivelOrg.Message);
                return View();
            }
            return View(getNivelOrg.Entity);
        }

        // POST: NivelOrganizacional/Delete/5
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

        public DataTablesResult<NivelOrganizacionalDataTable> GetPagedNivelOrganizacional(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new NivelOrganizacionalDataTable()
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
