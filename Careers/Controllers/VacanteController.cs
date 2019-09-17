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
    public class VacanteController : BaseController
    {
        private readonly VacanteService _service = new VacanteService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: Vacante
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        // GET: Vacante/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<Vacante> getVacante = _service.GetById(id);
            if (!getVacante.Succeeded)
            {
                MessageDanger(getVacante.Message);
                return View();
            }

            return View(getVacante.Entity);
        }

        // GET: Vacante/Create
        public ActionResult Create()
        {
            return View(AddSelectListsToVacanteViewModel());
        }

        // POST: Vacante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VacanteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vacante = model.ToVacante();

                var create = _service.Create(vacante);
                if (create.Succeeded)
                {
                    MessageSuccess(create.Message);
                    return JavaScript("location.reload(true)");
                }
                else
                {
                    MessageDanger(create.Message);
                }
            }
            model = AddSelectListsToVacanteViewModel(model);
            return View(model);
        }

        // GET: Vacante/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<Vacante> getVacante = _service.GetById(id);
            if (!getVacante.Succeeded)
            {
                MessageDanger(getVacante.Message);
                return View();
            }

            var viewModel = new VacanteViewModel();
            var model = viewModel.ToViewModel(getVacante.Entity);
            model = AddSelectListsToVacanteViewModel(model);
            return View(model);
        }

        // POST: Vacante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VacanteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vacante = model.ToVacante();
                var update = _service.Update(vacante);

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
            model = AddSelectListsToVacanteViewModel(model);
            return View(model);
        }
        
        // GET: Vacante/Delete/5
        public ActionResult Delete(int id)
        {
            var getVacante = _service.GetById(id);
            if (!getVacante.Succeeded)
            {
                MessageDanger(getVacante.Message);
                return View();
            }
            return View(getVacante.Entity);
        }

        // POST: Vacante/Delete/5
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

        #region Helpers

        private VacanteViewModel AddSelectListsToVacanteViewModel(VacanteViewModel vacante = null)
        {
            if (vacante == null)
            {
                vacante = new VacanteViewModel();
            }

            vacante.Empresas = new MySelectList(_catService.GetEmpresasForSelectList());
            vacante.Areas = new MySelectList(_catService.GetAreasForSelectList());
            vacante.Paises = new MySelectList(_catService.GetPaisesForSelectList());
            vacante.NivelOrganizacional = new MySelectList(_catService.GetNivelOrgForSelectList());
            vacante.TipoPublicacion = new MySelectList(_catService.GetAreasForSelectList());
            return vacante;
        }


        public JsonResult GetEstadoByPaisId(int id)
        {           
            var estados = _catService.GetEstadosByPaisIdForSelectList(id);
            return Json(estados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCiudadByEstadoId(int id)
        {
            var ciudades = _catService.GetCiudadesByEstadoIdForSelectList(id);
            return Json(ciudades, JsonRequestBehavior.AllowGet);
        }

        public DataTablesResult<VacanteDataTable> GetPagedVacante(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new VacanteDataTable()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Empresa = x.Empresa.Nombre,
                Pais= x.Ciudad.Estado.Pais.Nombre,
                Estado = x.Ciudad.Estado.Nombre,
                Ciudad = x.Ciudad.Nombre,
                Area = x.Area.Nombre,
            });

            return DataTablesResult.Create(data, dtParams, x => new
            {
                Tools = DataTablesButtons.GetEditButton(Url.Action("Edit", new { id = x.Id })) +
                        DataTablesButtons.GetDeleteButton(Url.Action("Delete", new { id = x.Id })) +
                        DataTablesButtons.GetDetailsButton(Url.Action("Details", new { id = x.Id })) 
            });
        }

        #endregion
    }
}
