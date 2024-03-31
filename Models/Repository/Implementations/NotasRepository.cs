using APINotificacionesV2.Models.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using APINotificacionesV2.Models.Entities;
using APINotificacionesV2.Models.Datos;

namespace APINotificacionesV2.Models.Repository.Implementations
{
    public class NotasRepository : IRepository<Notas>
    {
        private readonly DBContext _dbContext;
        public NotasRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Notas> Create(Notas notas)
        {
            try
            { 
                _dbContext.Notas.Add(notas);
                await _dbContext.SaveChangesAsync();
                return notas; // Asegúrate de que el modelo devuelto tiene el ID actualizado.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
 
         
        public async Task<bool> Delete(int idusuario, int idnota)
        {
            var nota = await _dbContext.Notas
                .FirstOrDefaultAsync(n => n.UsuarioId == idusuario && n.NotaId == idnota);

            if (nota == null)
            {
                return false;
            }

            _dbContext.Notas.Remove(nota);
            int affectedRows = await _dbContext.SaveChangesAsync();

            return affectedRows > 0;
        }


        public Task<IQueryable<Notas>> GetAll()
        {
            var notasQuery = _dbContext.Notas.AsQueryable();
            return Task.FromResult(notasQuery);
        }

        public Task<IQueryable<Notas>> GetById(int id)
        {
            throw new NotImplementedException();

        }

        //trae la nota del usuario  
        public Task<IQueryable<Notas>> GetById(int idusuario, int idnota)
        {
            var notas = _dbContext.Notas
                .Include(n => n.Usuarios)
                .Where(n => n.UsuarioId == idusuario && n.NotaId == idnota);

            return Task.FromResult(notas);
        }


        public async Task<Notas> Update(int id, Notas notas)
        {
            try
            {
                var _Notas = await _dbContext.Notas.FindAsync(id);
                if (_Notas != null)
                {
                    _Notas.UsuarioId = notas.UsuarioId;
                    _Notas.Titulo = notas.Titulo;
                    _Notas.Contenido = notas.Contenido;
                    _Notas.FechaCreacion = notas.FechaCreacion;
                    _Notas.Etiquetas = notas.Etiquetas;

                    await _dbContext.SaveChangesAsync();
                    return _Notas;
                }
                return _Notas;

            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
         
        public Task<Notas> UsuarioExists(Notas notas)
        {
           throw new NotImplementedException();
        }


    }
}

