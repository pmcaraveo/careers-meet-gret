using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain
{
    public class PaisService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create(Pais entity)
        {
            _db.Pais.Add(entity);
            _db.SaveChanges();
            return new OperationResult(true, "Pais guardado satisfactoriamente.");
        }
        public List<Pais> GetAll()
        {
            var pais = _db.Pais.ToList();
            return pais;
        }

        public IQueryable<Pais> GetPage()
        {
            return _db.Pais;
        }

        public OperationResult<Pais> GetById(int id)
        {
            var pais = _db.Pais.FirstOrDefault(x => x.Id == id);
            if (pais == null)
            {
                return new OperationResult<Pais>(false, "País no encontrado.");
            }
            else
            {
                return new OperationResult<Pais>(true, pais);
            }
        }

        public OperationResult<Pais> Update(Pais pais)
        {
            var getPais = GetById(pais.Id);
            var entity = getPais.Entity;

            if (!getPais.Succeeded)
            {
                return getPais;
            }

            entity.Nombre = pais.Nombre;
            entity.Activo = pais.Activo;
            entity.UsuarioActualizoId = pais.UsuarioActualizoId;

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<Pais>(true, "Registro actualizado");
        }

        public OperationResult Delete(int id)
        {
            var getPais = GetById(id);
            if (getPais.Succeeded)
            {
                _db.Pais.Remove(getPais.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, getPais.Message);
            }
            return new OperationResult(true, "Eliminado existosamente");
        }
    }
}