using MVC5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Careers.Domain
{
    public class EjecutivoService
    {
        private readonly CareersEntities _db = new CareersEntities();
        //Ejecutivos entity
        public OperationResult Create(Ejecutivos entity)
        {
            /*_db.Ejecutivos.Add(entity);
            _db.SaveChanges();
            return new OperationResult(true, "Operacion Realizada exitosamente.");*/
            using (DbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    
                    Horarios fechas = new Horarios();
                 
                    fechas.EjecutivoId = entity.Id;
                    fechas.Fecha = "2019-09-07 16:00:00.000";
                    
                    _db.Ejecutivos.Add(entity);
                    _db.Horarios.Add(fechas);

                    _db.SaveChanges();
                    dbTran.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    dbTran.Rollback();
                    throw;
                }
            }

            return new OperationResult(true, "Operacion Realizada exitosamente.");
        }

       

        public OperationResult saveStore(Ejecutivos entity)
        {
                using (DbContextTransaction dbTran = _db.Database.BeginTransaction())
                {
                    try
                    {
                    _db.Ejecutivos.Add(entity);
                    _db.SaveChanges();

                        dbTran.Commit();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbTran.Rollback();
                        throw;
                    }
                }

            return new OperationResult(true, "Operacion Realizada exitosamente.");
        }


        public List<Ejecutivos> GetAll()
        {
            var ejecutivo = _db.Ejecutivos
                .Include(v => v.Empresa)
                .Include(v => v.NivelOrganizacional)
                .Include(v => v.Colaborador)
                .Include(v => v.Canal)
                .ToList();
            return ejecutivo;
        }
        
        public OperationResult<Ejecutivos> GetAllEje()
        {
            var ejecutivo = _db.Ejecutivos.FirstOrDefault();
            if (ejecutivo == null)
            {
                return new OperationResult<Ejecutivos>(false, "No hay Ejecutivos.");
            }
            else
            {
                return new OperationResult<Ejecutivos>(true, ejecutivo);
            }
        }

        public IQueryable<Ejecutivos> ListaDatos(){

            return _db.Ejecutivos;
        }

        public IQueryable<v_perfil_ejecutivos> ListaPerfil()
        {

            return _db.v_perfil_ejecutivos.OrderBy(a => a.Id);
        }

        public IQueryable<v_historico> ListaHistorial()
        {
            return _db.v_historico.OrderBy(a => a.Ejecutivo);
        }



        public OperationResult<Ejecutivos> GetById(int id)
        {
            var ejecutivo = _db.Ejecutivos.FirstOrDefault(x => x.Id == id);
            if (ejecutivo == null)
            {
                return new OperationResult<Ejecutivos>(false, "Ejecutivo no encontrado.");
            }
            else
            {
                return new OperationResult<Ejecutivos>(true, ejecutivo);
            }
        }

        public OperationResult<Colaborador> GetId(int id)
        {
            var col = _db.Colaborador.FirstOrDefault(x => x.Id == id);
            if (col == null)
            {
                return new OperationResult<Colaborador>(false, "Ejecutivo no encontrado.");
            }
            else
            {
                return new OperationResult<Colaborador>(true, col);
            }
        }
        public List<Colaborador> getApoyoId(int ApoyoId)
        {
            return _db.Colaborador.Where(x => x.Id == ApoyoId).ToList();
        }


        //public OperationResult<Estado> Update(Estado estado)
        //{
        //    var getEstado = GetById(estado.Id);
        //    var entity = getEstado.Entity;

        //    if (!getEstado.Succeeded)
        //    {
        //        return getEstado;
        //    }

        //    entity.Nombre = estado.Nombre;
        //    entity.Activo = estado.Activo;
        //    entity.UsuarioActualizoId = estado.UsuarioActualizoId;

        //    _db.Entry(entity).State = EntityState.Modified;
        //    _db.SaveChanges();
        //    return new OperationResult<Estado>(true, "Registro actualizado");
        //}

        //public OperationResult Delete(int id)
        //{
        //    var getEstado = GetById(id);
        //    if (getEstado.Succeeded)
        //    {
        //        _db.Estado.Remove(getEstado.Entity);
        //        _db.SaveChanges();
        //    }
        //    else
        //    {
        //        return new OperationResult(false, getEstado.Message);
        //    }
        //    return new OperationResult(true, "Eliminado existosamente");
        //}
    }
}