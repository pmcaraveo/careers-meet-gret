using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyAlfaLive.Domain;
using MyAlfaLive.Areas.Admin.Models;
using Mvc.JQuery.DataTables;
using MyAlfaLive.Helpers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using MyAlfaLive.Models;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace MyAlfaLive.Areas.Admin.Controllers
{
    public class UsuariosController : BaseController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly UsuariosService _service = new UsuariosService();
        private readonly CatalogoService _catService = new CatalogoService();

        #region Constructors

        public UsuariosController()
        {
        }

        public UsuariosController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        #endregion

        #region Index

        // GET: Admin/Usuarios
        public ActionResult Index()
        {
            return View();
        }

        public DataTablesResult<UsuariosDataTable> GetPagedUsuarios(DataTablesParam dtParams)
        {
            var data = _service.GetPage().Select(x => new UsuariosDataTable()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Email = x.Email,
                Empresa = x.Empresa.Nombre
            });

            return DataTablesResult.Create(data, dtParams, x => new
            {
                Tools = DataTablesButtons.GetEditButton(Url.Action("Edit", new { id = x.Id })) +
                        DataTablesButtons.GetDeleteButton(Url.Action("Delete", new { id = x.Id })) +
                        DataTablesButtons.GetDetailsButton(Url.Action("Details", new { id = x.Id }))
            });
        }

        #endregion

        #region Create

        // GET: Admin/Usuarios/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            var model = new UsuariosViewModel();
            model.Empresas = new MySelectList(_catService.GetEmpresasForSelectList());

            // TODO:FILTRAR LA LISTA SEGÚN EL USUARIO LOGEADO
            model.Roles = new MySelectList(_catService.GetRolesForSelectList());
            return View(model);
        }

        // POST: Admin/Usuarios/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsuariosViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Crear un nuevo usuario
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Nombre = model.Nombre, Apellido = model.Apellido, EmpresaId = model.EmpresaId };
                var result = await UserManager.CreateAsync(user);

                //Si se creó el usuario
                if (result.Succeeded)
                {
                    //Agregar usuario a roles seleccionados.
                    foreach (var role in model.RolesId)
                    {
                        //Verificar que existe el rol
                        var rolexiste = await RoleManager.FindByIdAsync(role);

                        //Si el rol existe, agregarlo al usuario
                        if (rolexiste != null)
                        {
                            var roleresult = await UserManager.AddToRoleAsync(user.Id, rolexiste.Name);

                            //Agregar al usuario rol de colaborador
                            var colaborador = UserManager.AddToRole(user.Id, "Colaborador");
                        }
                    }

                    //Enviar correo para confirmar cuenta
                    await SendConfirmationEmailAsync(user);

                    MessageSuccess("Usuario creado correctamente, se a enviado un email al usuario.");

                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            model.Empresas = new MySelectList(_catService.GetEmpresasForSelectList());
            model.Roles = new MySelectList(_catService.GetRolesForSelectList());
            return View(model);
        }

        #endregion

        #region Details

        // GET: Admin/Usuarios/Details/5
        public ActionResult Details(int id)
        {
            var model = new UsuarioDetailViewModel();
            OperationResult<AspNetUsers> getUsuario = _service.GetById(id);

            if (getUsuario.Succeeded)
            {
                model.Usuario = getUsuario.Entity;

                var roles = _service.GetRoles(id);
                model.Roles = roles;

                return View(model);
            }
            else
            {
                MessageDanger(getUsuario.Message);
            }

            return View();
        }

        #endregion

        #region ConfirmEmail

        // GET: Admin/Usuarios/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == default(int) || code == null)
            {
                return View("Error");
            }

            //Confirmando el email
            var result = await UserManager.ConfirmEmailAsync(userId, code);

            //Si se pudo confirmar
            if (result.Succeeded)
            {
                //Obtener el usuario para enviar email
                var user = UserManager.FindById(userId);

                //Enviar correo para crear password
                await SendSetPasswordAsync(user);
                MessageSuccess("Email confirmado.");
                return View("SetPasswordEmail");

            }
            return View("Error");
        }

        #endregion

        #region SetPassword

        // GET: Admin/Usuarios/SetPassword
        [AllowAnonymous]
        public ActionResult SetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: Admin/Usuarios/SetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Obtener usuario
                var user = await UserManager.FindByNameAsync(model.Email);

                //Crear password
                var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    //Redireccionar a SetPasswordConfirmation
                    return View("SetPasswordConfirmation");
                }
                return View ("Error");
            }
            //Regresar vista
            return View(model);
        }

        #endregion

        //// GET: Admin/Usuarios/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    OperationResult<AspNetUsers> getUsuario = _service.GetById(id);
        //    if (!getUsuario.Succeeded)
        //    {
        //        MessageDanger(getUsuario.Message);
        //        return View();
        //    }

        //    var viewModel = new UsuariosViewModel();
        //    var model = viewModel.ToViewModel(getUsuario.Entity);
        //    model = AddSelectListsToUsuarioViewModel(model);
        //    return View(model);
        //}

        //// POST: Admin/Usuarios/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(UsuariosViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var usuario = model.ToUsuario();
        //        var update = _service.Update(usuario);

        //        if (update.Succeeded)
        //        {
        //            MessageSuccess(update.Message);
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            MessageDanger(update.Message);
        //        }
        //    }
        //    model = AddSelectListsToUsuarioViewModel(model);
        //    return View(model);
        //}

        // GET: Admin/Usuarios/Delete/5
        //public ActionResult Delete(int id)
        //{
            //var getUsuario = _service.GetById(id);
            //if (!getUsuario.Succeeded)
            //{
            //    MessageDanger(getUsuario.Message);
            //    return View();
            //}
            //return View(getUsuario.Entity);
        //}

        // POST: Admin/Usuarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirm(int id)
        //{
            //var eliminar = _service.Delete(id);
            //if (eliminar.Succeeded)
            //{
            //    MessageSuccess(eliminar.Message);
            //    return RedirectToAction("Index");
            //}

            //MessageDanger(eliminar.Message);
            //return View();
        //}

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private UsuariosViewModel AddSelectListsToUsuarioViewModel(UsuariosViewModel usuario = null)
        {
            if (usuario == null)
            {
                usuario = new UsuariosViewModel();
            }

            usuario.Empresas = new MySelectList(_catService.GetEmpresasForSelectList());
            return usuario;
        }


        /// <summary>
        /// Enviar correo de confirmación de cuenta
        /// </summary>
        /// <param name = "user" > Usuario de la aplicación</param>
        /// <returns></returns>
        private async Task SendConfirmationEmailAsync(ApplicationUser user)
        {
            //Obtener Url de confirmación.
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Usuarios",
                new { userId = user.Id, Code = code },
                protocol: Request.Url.Scheme);

            //Crear el cuerpo del correo de confirmación
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Content/htmltemplates/EmailConfirmation.html"));
            body = body.Replace("#CallBack#", callbackUrl);

            //Crear IdentityMessage
            IdentityMessage identityMessage = new IdentityMessage()
            {
                Destination = user.Email,
                Subject = "Completa tu registro para acceder a MyAlfaLive",
                Body = body
            };

            //Enviar el correo de confirmación
            EmailService emailService = new EmailService();
            await emailService.SendAsync(identityMessage);
        }

        /// <summary>
        /// Enviar correo de recuperación de contraseña.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task SendSetPasswordAsync(ApplicationUser user)
        {
            //Obtener Url de confirmación.
            var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var callbackUrl = Url.Action("SetPassword", "Usuarios",
                new { userId = user.Id, Code = code },
                protocol: Request.Url.Scheme);

            //Crear el cuerpo del correo de confirmación
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Content/htmltemplates/SetPassword.html"));
            body = body.Replace("#Password#", callbackUrl);

            //Crear IdentityMessage
            IdentityMessage identityMessage = new IdentityMessage()
            {
                Destination = user.Email,
                Subject = "Establecer contraseña",
                Body = body
            };

            //Enviar el correo de confirmación
            EmailService emailService = new EmailService();
            await emailService.SendAsync(identityMessage);
        }
        #endregion
    }
}
