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
    public class PaisController : BaseController
    {
        private readonly PaisService _service = new PaisService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: Pais
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        // GET: Pais/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<Pais> getPais = _service.GetById(id);
            if (!getPais.Succeeded)
            {
                MessageDanger(getPais.Message);
                return View();
            }
            return View(getPais.Entity);
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pais = new Pais()
                {
                    Nombre = model.Nombre,
                    Activo = model.Activo,
                    UsuarioCreoId = User.Identity.GetUserId<int>()

                };

                var create = _service.Create(pais);
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

        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<Pais> getPais = _service.GetById(id);
            if (!getPais.Succeeded)
            {
                MessageDanger(getPais.Message);
                return View();
            }
            var viewModel = new PaisViewModel()
            {
                Id = getPais.Entity.Id,
                Nombre = getPais.Entity.Nombre,
                Activo = getPais.Entity.Activo
            };
            return View(viewModel);
        }

        // POST: Pais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pais = new Pais()
                {

                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo,
                    UsuarioActualizoId = User.Identity.GetUserId<int>()
                };

                var update = _service.Update(pais);
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

        // GET: Pais/Delete/5
        public ActionResult Delete(int id)
        {
            var getPais = _service.GetById(id);
            if (!getPais.Succeeded)
            {
                MessageDanger(getPais.Message);
                return View();
            }
            return View(getPais.Entity);
        }

        // POST: Pais/Delete/5
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
        public DataTablesResult<PaisDataTable> GetPagedPais(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new PaisDataTable()
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
