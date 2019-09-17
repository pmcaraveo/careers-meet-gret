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
using Microsoft.AspNet.Identity;
using MVC5.Helpers;
using Mvc.JQuery.DataTables;
using Careers.Helpers;

namespace Careers.Controllers
{
    public class EmpresaController : BaseController
    {
        private readonly EmpresaService _service = new EmpresaService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: Empresa
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        // GET: Empresa/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<Empresa> getEmpresa = _service.GetById(id);
            if (!getEmpresa.Succeeded)
            {
                MessageDanger(getEmpresa.Message);
                return View();
            }
            return View(getEmpresa.Entity);
        }

        // GET: Empresa/Create
        public ActionResult Create()
        {         
            return View();
        }

        // POST: Empresa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpresaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var empresa = new Empresa()
                {
                    Nombre = model.Nombre,
                    UsuarioCreoId = User.Identity.GetUserId<int>(),
                    Activo = model.Activo
                };
                var create = _service.Create(empresa);
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

        // GET: Empresa/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<Empresa> getEmpresa = _service.GetById(id);
            if (!getEmpresa.Succeeded)
            {
                MessageDanger(getEmpresa.Message);
                return View();
            }
            var viewModel = new EmpresaViewModel()
            {
                Id = getEmpresa.Entity.Id,
                Nombre = getEmpresa.Entity.Nombre,
                Activo = getEmpresa.Entity.Activo
            };
            return View(viewModel);
        }

        // POST: Empresa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmpresaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var empresa = new Empresa()
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo,
                    UsuarioActualizoId = User.Identity.GetUserId<int>()
                };

                var update = _service.Update(empresa);

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

        // GET: Empresa/Delete/5
        public ActionResult Delete(int id)
        {
            var getEmpresa = _service.GetById(id);
            if (!getEmpresa.Succeeded)
            {
                MessageDanger(getEmpresa.Message);
                return View();
            }
            return View(getEmpresa.Entity);
        }

        // POST: Empresa/Delete/5
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

        public DataTablesResult<EmpresaDataTable> GetPagedEmpresa(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new EmpresaDataTable()
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