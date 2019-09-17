using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain.Services
{
    public class AreaService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create (Area entity)
        {
            _db.Area.Add(entity);
            _db.SaveChanges();
            return new OperationResult(true, "Área guardada satisfactoriamente.");
        }

        public List<Area> GetAll()
        {
            var area = _db.Area.ToList();
            return area;
        }

        public IQueryable<Area> GetPage()
        {
            return _db.Area;
        }

        public OperationResult<Area> GetById (int id)
        {
            var area = _db.Area.FirstOrDefault(x => x.Id == id);
            if(area == null)
            {
                return new OperationResult<Area>(false, "Área no encontrada");
            }
            else
            {
                return new OperationResult<Area>(true, area);
            }
        }

        public  OperationResult<Area> Update (Area area)
        {
            var getArea = GetById(area.Id);
            var entity = getArea.Entity;
            if (!getArea.Succeeded)
            {
                return getArea;
            }

            entity.Nombre = area.Nombre;
            entity.Activo = area.Activo;
            entity.UsuarioActualizoId = area.UsuarioActualizoId;

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<Area>(true, "Registro actualizado");
        }

        public OperationResult Delete (int id)
        {
            var getArea = GetById(id);
            if (getArea.Succeeded)
            {
                _db.Area.Remove(getArea.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, getArea.Message);
            }
            return new OperationResult(true, "Eliminado existosamente");
        }
    }
}
