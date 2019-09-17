using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyAlfaLive.Domain
{
    public class UsuariosService
    {
        private readonly MyAlfaLiveEntities _db = new MyAlfaLiveEntities();

        public OperationResult Create (AspNetUsers entity)
        {
            _db.AspNetUsers.Add(entity);
            _db.SaveChanges();

            return new OperationResult(true, "Usuario guardado");
        }

        public List<AspNetUsers> GetAll()
        {
            var usuarios = _db.AspNetUsers
                .Include(v => v.Empresa)
                .Include(v => v.AspNetRoles)
                .ToList();
            return usuarios;
        }

        public IQueryable<AspNetUsers> GetPage()
        {
            return _db.AspNetUsers;
        }

        public OperationResult<AspNetUsers> GetById(int id)
        {
            var usuario = _db.AspNetUsers.FirstOrDefault(x => x.Id == id);
            if (usuario == null)
            {
                return new OperationResult<AspNetUsers>(false, "Usuario no encontrado.");
            }
            else
            {
                return new OperationResult<AspNetUsers>(true, usuario);
            }
        }

        public List<AspNetRoles> GetRoles(int userId)
        {
            var result = _db.AspNetUsers.First(x => x.Id == userId).AspNetRoles.ToList();

            return result;
        }

        public OperationResult<AspNetUsers> Update(AspNetUsers usuarios)
        {
            _db.Entry(usuarios).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<AspNetUsers>(true, "Registro actualizado");
        }

        public OperationResult<AspNetUsers> Delete(AspNetUsers usuarios)
        {
            var getUsuario = GetById(usuarios.Id);
            var entity = getUsuario.Entity;

            if (!getUsuario.Succeeded)
            {
                return getUsuario;
            }

            entity.Activo = usuarios.Activo;    

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<AspNetUsers>(true, "Usuario eliminado");

            //if (getUsuario.Succeeded)
            //{
            //    _db.AspNetUsers.Remove(getUsuario.Entity);
            //    _db.SaveChanges();
            //}
            //else
            //{
            //    return new OperationResult(false, "Registro no encontrado");
            //}
            //return new OperationResult(true, "Eliminado exitosamente");
        }
    }
}
