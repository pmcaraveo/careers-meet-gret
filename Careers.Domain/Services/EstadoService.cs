using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain
{
    public class EstadoService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create(Estado entity)
        {
            _db.Estado.Add(entity);
            _db.SaveChanges();
            return new OperationResult(true, "Estado guardado satisfactoriamente.");
        }
        public List<Estado> GetAll()
        {
            var estado = _db.Estado
                .Include(v => v.Pais)
                .ToList();
            return estado;
        }

        public IQueryable<Estado> GetPage()
        {
            return _db.Estado;
        }

        public OperationResult<Estado> GetById(int id)
        {
            var estado = _db.Estado.FirstOrDefault(x => x.Id == id);
            if (estado == null)
            {
                return new OperationResult<Estado>(false, "País no encontrado.");
            }
            else
            {
                return new OperationResult<Estado>(true, estado);
            }
        }

        public OperationResult<Estado> Update(Estado estado)
        {
            var getEstado = GetById(estado.Id);
            var entity = getEstado.Entity;

            if (!getEstado.Succeeded)
            {
                return getEstado;
            }

            entity.Nombre = estado.Nombre;
            entity.Activo = estado.Activo;
            entity.UsuarioActualizoId = estado.UsuarioActualizoId;

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<Estado>(true, "Registro actualizado");
        }

        public OperationResult Delete(int id)
        {
            var getEstado = GetById(id);
            if (getEstado.Succeeded)
            {
                _db.Estado.Remove(getEstado.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, getEstado.Message);
            }
            return new OperationResult(true, "Eliminado existosamente");
        }
    }
}