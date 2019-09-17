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
    public class ColController : BaseController
    {
        private readonly ColService _service = new ColService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: 
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        public ActionResult Create()
        {
            return View(AddSelectListsToColViewModel());
        }

        // POST: /Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ColViewModel model)
        {
             if (ModelState.IsValid)
                {
                    var colaborador = model.ToCol();
                    var create = _service.Create(colaborador);
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
        model = AddSelectListsToColViewModel(model);
        return View(model);         
        }

        //GET: 
        public ActionResult Edit(int id)
        {
            OperationResult<Colaborador> getCol = _service.GetById(id);
            if (!getCol.Succeeded)
            {
                MessageDanger(getCol.Message);
                return View();
            }
            var viewModel = new ColViewModel();
            var model = viewModel.ToViewModel(getCol.Entity);
            model = AddSelectListsToColViewModel(model);
            return View(model);
        }

        // POST: /Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ColViewModel model)
        {
            if (ModelState.IsValid)
            {
                var col = model.ToCol();
                var update = _service.Update(col);
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
            model = AddSelectListsToColViewModel(model);
            return View(model);
        }


        private ColViewModel AddSelectListsToColViewModel(ColViewModel col = null)
        {
            if (col == null)
            {
                col = new ColViewModel();
            }

            col.Empresa = new MySelectList(_catService.GetEmpresasForSelectList());
            col.Area = new MySelectList(_catService.GetAreasForSelectList());
            return col;
        }

        public DataTablesResult<ColDataTable> GetPagedCol(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new ColDataTable()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Email =x.Email,
                Telefono = x.Telefono,
                Empresa = x.Empresa.Nombre,
                Area = x.Area.Nombre
            });

            return DataTablesResult.Create(data, dtParams, x => new
            {
                Tools = DataTablesButtons.GetEditButton(Url.Action("Edit", new { id = x.Id })) +
                        DataTablesButtons.GetDeleteButton(Url.Action("Delete", new { id = x.Id })) 
                        //DataTablesButtons.GetDetailsButton(Url.Action("Details", new { id = x.Id }))
            });
        }
    }
}
