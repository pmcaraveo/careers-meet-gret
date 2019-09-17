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
    public class UsuarioController : BaseController
    {
        private readonly UsersService _service = new UsersService();
        private readonly CatalogoService _catService = new CatalogoService();

        // GET: Usuario
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }
    
        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            OperationResult<AspNetUsers> getUsuario = _service.GetById(id);
            if (!getUsuario.Succeeded)
            {
                MessageDanger(getUsuario.Message);
                return View();
            }

            return View(getUsuario.Entity);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View(AddSelectListsToUsuarioViewModel());
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = model.ToUsuario();
                var create = _service.Create(usuario);
                if (create.Succeeded)
                {
                    MessageSuccess(create.Message);
                    return JavaScript("Index");
                }
                else
                {
                    MessageDanger(create.Message);
                }
            }
            model = AddSelectListsToUsuarioViewModel(model);
            return View(model);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            OperationResult<AspNetUsers> getUsuario = _service.GetById(id);
            if (!getUsuario.Succeeded)
            {
                MessageDanger(getUsuario.Message);
                return View();
            }

            var viewModel = new UsuarioViewModel();
            var model = viewModel.ToViewModel(getUsuario.Entity);
            model = AddSelectListsToUsuarioViewModel(model);
            return View(model);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = model.ToUsuario();
                var update = _service.Update(usuario);

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
            model = AddSelectListsToUsuarioViewModel(model);
            return View(model);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Helpers
        private UsuarioViewModel AddSelectListsToUsuarioViewModel(UsuarioViewModel usuario = null)
        {
            if (usuario == null)
            {
                usuario = new UsuarioViewModel();
            }

            usuario.Empresas = new MySelectList(_catService.GetEmpresasForSelectList());
            return usuario;
        }
        #endregion
    }
}