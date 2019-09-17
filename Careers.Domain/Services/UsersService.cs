using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain
{
    public class UsersService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create (AspNetUsers entity)
        {
            _db.AspNetUsers.Add(entity);
            _db.SaveChanges();

            return new OperationResult(true, "Usuario guardado.");
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
            var usuarios = _db.AspNetUsers.FirstOrDefault(x => x.Id == id);
            if (usuarios == null)
            {
                return new OperationResult<AspNetUsers>(false, "Usuario no encontrado.");
            }
            else
            {
                return new OperationResult<AspNetUsers>(true, usuarios);
            }
        }

        public OperationResult<AspNetUsers> Update(AspNetUsers usuarios)
        {
            _db.Entry(usuarios).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<AspNetUsers>(true, "Registro actualizado");
        }

        public OperationResult Delete (int id)
        {
            var GetUsuario = GetById(id);
            if (GetUsuario.Succeeded)
            {
                _db.AspNetUsers.Remove(GetUsuario.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, "Registro no encontrado");
            }
            return new OperationResult(true, "Eliminado exitosamente");
        }
    }
}
