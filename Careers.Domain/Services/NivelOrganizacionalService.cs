using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain.Services
{
    public class NivelOrganizacionalService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create (NivelOrganizacional entity)
        {
            _db.NivelOrganizacional.Add(entity);
            _db.SaveChanges();
            return new OperationResult(true, "Nivel Organizacional guardada satisfactoriamente.");
        }

        public List<NivelOrganizacional> GetAll()
        {
            var nivelOrg = _db.NivelOrganizacional.ToList();
            return nivelOrg;
        }

        public IQueryable<NivelOrganizacional> GetPage()
        {
            return _db.NivelOrganizacional;
        }


        public OperationResult<NivelOrganizacional> GetById(int id)
        {
            var nivelOrg = _db.NivelOrganizacional.FirstOrDefault(x => x.Id == id);
            if (nivelOrg == null)
            {
                return new OperationResult<NivelOrganizacional>(false, "Nivel Organizacional no encontrado");
            }
            else
            {
                return new OperationResult<NivelOrganizacional>(true, nivelOrg);
            }
        }
        public OperationResult<NivelOrganizacional> Update(NivelOrganizacional nivelOrg)
        {
            var getNivelOrg = GetById(nivelOrg.Id);
            var entity = getNivelOrg.Entity;
            if (!getNivelOrg.Succeeded)
            {
                return getNivelOrg;
            }

            entity.Nombre = nivelOrg.Nombre;
            entity.Activo = nivelOrg.Activo;
            entity.UsuarioActualizoId = nivelOrg.UsuarioActualizoId;

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<NivelOrganizacional>(true, "Registro actualizado");
        }
        public OperationResult Delete(int id)
        {
            var getNivelOrg = GetById(id);
            if (getNivelOrg.Succeeded)
            {
                _db.NivelOrganizacional.Remove(getNivelOrg.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, getNivelOrg.Message);
            }
            return new OperationResult(true, "Eliminado existosamente");
        }
    }
}
