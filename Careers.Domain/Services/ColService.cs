using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Careers.Domain
{
    public class ColService
    {
        private readonly CareersEntities _db = new CareersEntities();

        public OperationResult Create(Colaborador entity)
        {
            _db.Colaborador.Add(entity);
            _db.SaveChanges();
            return new OperationResult(true, "Registro guardado.");
        }

        public List<Colaborador> GetAll()
        {
            var col = _db.Colaborador
                .Include(v => v.Area)
                .Include(v => v.Empresa)
                .ToList();
            return col;
        }

        public IQueryable<Colaborador> GetPage()
        {
            return _db.Colaborador;
        }

        public OperationResult<Colaborador> GetById(int id)
        {
            var colab = _db.Colaborador.FirstOrDefault(x => x.Id == id);
            if (colab == null)
            {
                return new OperationResult<Colaborador>(false, "Colaborador no encontrada.");
            }
            else
            {
                return new OperationResult<Colaborador>(true, colab);
            }
        }


        public OperationResult<Colaborador> Update(Colaborador col)
        {
            _db.Entry(col).State = EntityState.Modified;
            _db.SaveChanges();
            return new OperationResult<Colaborador>(true, "Registro actualizado");
        }

        //public OperationResult Delete(int id)
        //{
        //    var GetVacante = GetById(id);
        //    if (GetVacante.Succeeded)
        //    {
        //        _db.Vacante.Remove(GetVacante.Entity);
        //        _db.SaveChanges();
        //    }
        //    else
        //    {
        //        return new OperationResult(false, "Registro no encontrado");
        //    }
        //    return new OperationResult(true, "Eleminado existosamente");
        //}
    }
}
