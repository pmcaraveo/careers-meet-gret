using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyAlfaLive.Domain.Services
{
    public class EmpresaService
    {
        private readonly MyAlfaLiveEntities _db = new MyAlfaLiveEntities();

        public OperationResult Create(Empresa entity)
        {
            _db.Empresa.Add(entity);
            _db.SaveChanges();

            return new OperationResult(true, "Empresa guardada satisfactoriamente.");
        }

        public List<Empresa> GetAll()
        {
            var empresa = _db.Empresa
                .Include(v => v.AspNetUsers)
                .ToList();
            return empresa;
        }

        public IQueryable<Empresa> GetPage()
        {
            return _db.Empresa;
        }

        public OperationResult<Empresa> GetById(int id)
        {
            var empresa = _db.Empresa.FirstOrDefault(x => x.Id == id);
            if (empresa == null)
            {
                return new OperationResult<Empresa>(false, "Empresa no encontrada.");
            }
            else
            {
                return new OperationResult<Empresa>(true, empresa);
            }
        }

        public OperationResult<Empresa> Update(Empresa empresa)
        {
            var getEmpresa = GetById(empresa.Id);
            var entity = getEmpresa.Entity;

            if (!getEmpresa.Succeeded)
            {
                return getEmpresa;
            }

            entity.Nombre = empresa.Nombre;
            entity.Activo = empresa.Activo;
            entity.UsuarioActualizoId = empresa.UsuarioActualizoId;

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<Empresa>(true, "Registro actualizado");
        }

        public OperationResult Delete(int id)
        {
            var getEmpresa = GetById(id);
            if (getEmpresa.Succeeded)
            {
                _db.Empresa.Remove(getEmpresa.Entity);
                _db.SaveChanges();
            }
            else
            {
                return new OperationResult(false, getEmpresa.Message);
            }
            return new OperationResult(true, "Eliminado existosamente");
        }
    }
}
