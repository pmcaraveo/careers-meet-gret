using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain
{
    public class CiudadService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create (Ciudad entity)
        {
            _db.Ciudad.Add(entity);
            _db.SaveChanges();
            return new OperationResult(true, "Ciudad guardado satisfactoriamente.");
        }

        public List<Ciudad> GetAll()
        {
            var ciudad = _db.Ciudad
                .Include(v => v.Estado)
                .ToList();
            return ciudad;
        }

        public IQueryable<Ciudad> GetPage()
        {
            return _db.Ciudad;
        }

        public OperationResult<Ciudad> GetById (int id)
        {
            var ciudad = _db.Ciudad.FirstOrDefault(X => X.Id == id);
            if (ciudad == null)
            {
                return new OperationResult<Ciudad>(false, "Ciudad no encontrada.");
            }
            else
            {
                return new OperationResult<Ciudad>(true, ciudad);
            }
        }

        public OperationResult<Ciudad> Update (Ciudad ciudad)
        {
            var getCiudad = GetById(ciudad.Id);
            var entity = getCiudad.Entity;

            if (!getCiudad.Succeeded)
            {
                return getCiudad;
            }

            entity.Nombre = ciudad.Nombre;
            entity.Activo = ciudad.Activo;
            entity.UsuarioActualizoId = ciudad.UsuarioActualizoId;

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<Ciudad>(true, "Registro actualizado");
        }

        public OperationResult Delete (int id)
        {
            var getCiudad = GetById(id);
            if (getCiudad.Succeeded)
            {
                _db.Ciudad.Remove(getCiudad.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, getCiudad.Message);
            }
            return new OperationResult(true, "Eliminado exitosamente");
        }
    }
}
