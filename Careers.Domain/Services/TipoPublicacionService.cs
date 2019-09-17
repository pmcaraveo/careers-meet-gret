using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain.Services
{
    public class TipoPublicacionService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create(TipoPublicacion entity)
        {
            _db.TipoPublicacion.Add(entity);
            _db.SaveChanges();
            return new OperationResult(true, "Nivel Organizacional guardada satisfactoriamente.");
        }

        public List<TipoPublicacion> GetAll()
        {
            var tipoPub = _db.TipoPublicacion.ToList();
            return tipoPub;
        }

        public IQueryable<TipoPublicacion> GetPage()
        {
            return _db.TipoPublicacion;
        }

        public OperationResult<TipoPublicacion> GetById(int id)
        {
            var tipoPub = _db.TipoPublicacion.FirstOrDefault(x => x.Id == id);
            if (tipoPub == null)
            {
                return new OperationResult<TipoPublicacion>(false, "Tipo de Publicación no encontrado");
            }
            else
            {
                return new OperationResult<TipoPublicacion>(true, tipoPub);
            }
        }

        public OperationResult<TipoPublicacion> Update(TipoPublicacion tipoPub)
        {
            var getTipoPub = GetById(tipoPub.Id);
            var entity = getTipoPub.Entity;
            if (!getTipoPub.Succeeded)
            {
                return getTipoPub;
            }

            entity.Nombre = tipoPub.Nombre;
            entity.Activo = tipoPub.Activo;
            entity.UsuarioActualizoId = tipoPub.UsuarioActualizoId;

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<TipoPublicacion>(true, "Registro actualizado");
        }
        public OperationResult Delete(int id)
        {
            var getTipoPub = GetById(id);
            if (getTipoPub.Succeeded)
            {
                _db.TipoPublicacion.Remove(getTipoPub.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, getTipoPub.Message);
            }
            return new OperationResult(true, "Eliminado existosamente");
        }
    }
}
