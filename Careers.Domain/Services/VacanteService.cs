using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain
{
    public class VacanteService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create(Vacante entity)
        {
            _db.Vacante.Add(entity);
            _db.SaveChanges();

            return new OperationResult(true, "Vacante guardada satisfactoriamente.");
        }

        public List<Vacante> GetAll()//
        {
            var vacante = _db.Vacante
                .Include(v => v.Area)
                .Include(v => v.Ciudad)
                .Include(v => v.Empresa)
                .Include(v => v.NivelOrganizacional)
                .Include(v => v.TipoPublicacion)
                .ToList();
            return vacante;
        }

        public IQueryable<Vacante> GetPage()
        {
            return _db.Vacante;
        }

        public OperationResult<Vacante> GetById(int id)
        {
            var vacante = _db.Vacante.FirstOrDefault(x => x.Id == id);
            if (vacante == null)
            {
                return new OperationResult<Vacante>(false, "Vacante no encontrada.");
            }
            else
            {
                return new OperationResult<Vacante>(true, vacante);
            }
        }

        public OperationResult<Vacante> Update(Vacante vacante)
        {
            _db.Entry(vacante).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<Vacante>(true, "Registro actualizado");
        }

        public OperationResult Delete(int id)
        {
            var GetVacante = GetById(id);
            if (GetVacante.Succeeded)
            {
                _db.Vacante.Remove(GetVacante.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, "Registro no encontrado");
            }
            return new OperationResult(true, "Eleminado existosamente");
        }
    }
}
