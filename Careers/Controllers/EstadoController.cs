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
    public class EstadoController : BaseController
    {
        private readonly EstadoService _service = new EstadoService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: Estado
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        // GET: Estado/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<Estado> getEstado = _service.GetById(id);
            if (!getEstado.Succeeded)
            {
                MessageDanger(getEstado.Message);
                return View();
            }
            return View(getEstado.Entity);
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            return View(AddSelectListsToEstadoViewModel());
        }

        // POST: Estado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UsuarioCreo = new Estado()
                {
                    UsuarioCreoId = User.Identity.GetUserId<int>()
                };

                var estado = model.ToEstado();
                var create = _service.Create(estado);
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
            model = AddSelectListsToEstadoViewModel(model);
            return View(model);
        }

        // GET: Estado/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<Estado> getEstado = _service.GetById(id);
            if (!getEstado.Succeeded)
            {
                MessageDanger(getEstado.Message);
                return View();
            }
            var viewModel = new EstadoViewModel();
            var model = viewModel.ToViewModel(getEstado.Entity);
            model = AddSelectListsToEstadoViewModel(model);
            return View(model);
        }

        // POST: Estado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EstadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var estado = new Estado()
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo,
                    UsuarioActualizoId = User.Identity.GetUserId<int>()
                };

                //var estado = model.ToEstado();
                var update = _service.Update(estado);
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
            model = AddSelectListsToEstadoViewModel(model);
            return View(model);
        }

        // GET: Estado/Delete/5
        public ActionResult Delete(int id)
        {
            var getEstado = _service.GetById(id);
            if (!getEstado.Succeeded)
            {
                MessageDanger(getEstado.Message);
                return View();
            }
            return View(getEstado.Entity);
        }

        // POST: Estado/Delete/5
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

        private EstadoViewModel AddSelectListsToEstadoViewModel(EstadoViewModel estado = null)
        {
            if (estado == null)
            {
                estado = new EstadoViewModel();
            }
            estado.Pais = new MySelectList(_catService.GetPaisesForSelectList());
            return estado;
        }

        public DataTablesResult<EstadoDataTable> GetPagedEstado(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new EstadoDataTable()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Pais = x.Pais.Nombre,
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