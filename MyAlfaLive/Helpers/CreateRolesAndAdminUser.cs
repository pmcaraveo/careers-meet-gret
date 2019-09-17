using Microsoft.AspNet.Identity;
using MyAlfaLive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using enu = MyAlfaLive.Domain.Enums;

namespace MyAlfaLive.Helpers
{
    public static class CreateRolesAndAdminUser
    {
        public static void Seed()
        {

            var context = new ApplicationDbContext();
            // Initialize default identity roles
            var rolStore = new CustomRoleStore(context);
            var rolManager = new RoleManager<CustomRole, int>(rolStore);
            // RoleTypes is a class containing constant string values for different roles
            var identityRoles = new List<CustomRole>();
            identityRoles.Add(new CustomRole() { Name = enu.Roles.SuperAdministrador.ToString(), Descripcion = enu.Roles.SuperAdministrador.GetDisplayName(), Activo = true });
            identityRoles.Add(new CustomRole() { Name = enu.Roles.AdministradorCareers.ToString(), Descripcion = enu.Roles.AdministradorCareers.GetDisplayName(), Activo = true });
            identityRoles.Add(new CustomRole() { Name = enu.Roles.AdministradorMeetG.ToString(), Descripcion = enu.Roles.AdministradorMeetG.GetDisplayName(), Activo = true });
            identityRoles.Add(new CustomRole() { Name = enu.Roles.AdministradorAskUs.ToString(), Descripcion = enu.Roles.AdministradorAskUs.GetDisplayName(), Activo = true });
            identityRoles.Add(new CustomRole() { Name = enu.Roles.AgenteCareers.ToString(), Descripcion = enu.Roles.AgenteCareers.GetDisplayName(), Activo = true });
            identityRoles.Add(new CustomRole() { Name = enu.Roles.AgenteMeetG.ToString(), Descripcion = enu.Roles.AgenteMeetG.GetDisplayName(), Activo = true });
            identityRoles.Add(new CustomRole() { Name = enu.Roles.AgenteAskUs.ToString(), Descripcion = enu.Roles.AgenteAskUs.GetDisplayName(), Activo = true });
            //identityRoles.Add(new IdentityRole() { Name = RoleTypes.User });

            foreach (var role in identityRoles)
            {
                rolManager.Create(role);
            }

            // Initialize empresa
            var empresa = new EmpresaIdentity()
            {
                Id = 1,
                Nombre = "Alfa",
                FechaCreacion = DateTime.Now,
                Activo = true
            };

            // Initialize default user
            var userStore = new CustomUserStore(context);
            var userManager = new UserManager<ApplicationUser, int>(userStore);
            ApplicationUser admin = new ApplicationUser();
            admin.Email = "sysadmin@email.com";
            admin.UserName = "sysadmin@email.com";
            admin.Nombre = "System";
            admin.Apellido = "Administrator";
            admin.Empresa = empresa;
            admin.EmailConfirmed = true;

            userManager.Create(admin, "Admin-10");
            userManager.AddToRole(admin.Id, enu.Roles.SuperAdministrador.ToString());
        }
    }
}