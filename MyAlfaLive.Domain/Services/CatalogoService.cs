using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAlfaLive.Domain;

namespace MyAlfaLive.Domain
{
    public class CatalogoService
    {
        private readonly MyAlfaLiveEntities _db = new MyAlfaLiveEntities();

        #region GetForSelectLists

        public List<SelectListModel> GetUsuariosForSelectList()
        {
            return _db.AspNetUsers
                .Where(x => x.Activo == true)
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
        }

        public List<SelectListModel> GetEmpresasForSelectList()
        {
            return _db.Empresa
                .Where(x => x.Activo == true)
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
        }

        public List<SelectListModel> GetRolesForSelectList()
        {
            return _db.AspNetRoles
                .Where(x => x.Activo == true)
                .OrderBy(o => o.Name)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Name
                }).ToList();
        }

        /*public List<SelectListModel> GetAreasForSelectList()
        {
            return _db.Area
                .Where(x => x.Activo == true)
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text= x.Nombre                
                }).ToList();
        }

        public List<SelectListModel> GetPaisesForSelectList()
        {
            return _db.Pais
                .Where(x => x.Activo == true)
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
        }

        public List<SelectListModel> GetEstadosByPaisIdForSelectList(int id)
        {
            return _db.Estado
                .Where(x => x.Activo == true && x.PaisId == id)
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
        }

        public List<SelectListModel> GetCiudadesByEstadoIdForSelectList(int id)
        {
            return _db.Ciudad
                .Where(x => x.Activo == true && x.EstadoId == id)
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
        }

        public List<SelectListModel> GetEstadosForSelectList()
        {
            return _db.Estado
                .Where(x => x.Activo == true )
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
        }

        public List<SelectListModel> GetCiudadesForSelectList()
        {
            return _db.Ciudad
                .Where(x => x.Activo == true)
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
        }

        public List<SelectListModel> GetNivelOrgForSelectList()
        {
            return _db.NivelOrganizacional
                .Where(x => x.Activo == true)
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
        }

        public List<SelectListModel> GetTipoPubForSelectList()
        {
            return _db.TipoPublicacion
                .Where(x => x.Activo == true)
                .OrderBy(o => o.Nombre)
                .Select(x => new SelectListModel()
                {
                    Id = x.Id.ToString(),
                    Text = x.Nombre
                }).ToList();
        }*/
        #endregion
    }
}
