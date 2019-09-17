using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain.Services
{
    public class CanalService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create(Canal entity)
        {
            _db.Canal.Add(entity);
            _db.SaveChanges();
            return new OperationResult(true, "Registro guardado satisfactoriamente.");
        }

        public List<Canal> GetAll()
        {
            var canal = _db.Canal.ToList();
            return canal;
        }

        public IQueryable<Canal> GetPage()
        {
            return _db.Canal;
        }

        public OperationResult<Canal> GetById (int id)
        {
            var canal = _db.Canal.FirstOrDefault(x => x.Id == id);
            if(canal == null)
            {
                return new OperationResult<Canal>(false, "Canal no encontrada");
            }
            else
            {
                return new OperationResult<Canal>(true, canal);
            }
        }

        public  OperationResult<Canal> Update (Canal canal)
        {
            var getCanal = GetById(canal.Id);
            var entity = getCanal.Entity;
            if (!getCanal.Succeeded)
            {
                return getCanal;
            }

            entity.Nombre = canal.Nombre;
            entity.Activo = canal.Activo;
            entity.FechaActualizacion = canal.FechaActualizacion;
            entity.UsuarioActualizoId = canal.UsuarioActualizoId;

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<Canal>(true, "Registro actualizado");
        }

        public OperationResult Delete (int id)
        {
            var getCanal = GetById(id);
            if (getCanal.Succeeded)
            {
                _db.Canal.Remove(getCanal.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, getCanal.Message);
            }
            return new OperationResult(true, "Eliminado existosamente");
        }
    }
}
