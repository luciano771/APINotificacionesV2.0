using APINotificacionesV2.Models.Entities;
using APINotificacionesV2.Models.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APINotificacionesV2.Models.Datos;

namespace APINotificacionesV2.Models.Repository.Implementations
{
    public class UsuariosRepository : IRepository<Usuarios>
    {
        private readonly DBContext _dbContext;
        public UsuariosRepository(DBContext dbContext) 
        {
            _dbContext = dbContext;
        }


        public async Task<Usuarios> Create(Usuarios usuarios)
        {
            try
            {
                _dbContext.Usuarios.Add(usuarios);
                await _dbContext.SaveChangesAsync();
                return usuarios; // Asegúrate de que el modelo devuelto tiene el ID actualizado.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var usuarios = await _dbContext.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return false;
            }

            _dbContext.Usuarios.Remove(usuarios);
            int affectedRows = await _dbContext.SaveChangesAsync();

            return affectedRows > 0;
        }

        public Task<IQueryable<Usuarios>> GetAll()
        { 
            var usuariosQuery = _dbContext.Usuarios.AsQueryable();
            return Task.FromResult(usuariosQuery);
        }

        public Task<IQueryable<Usuarios>> GetById(int id)
        {
            var usuariosById = _dbContext.Usuarios
                                              .Where(c => c.UsuarioId == id).AsQueryable();
            return Task.FromResult(usuariosById);
        }





        public Task<Usuarios> Update(int id, Usuarios entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete (int idusuario, int idnota)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Usuarios>> GetById(int idusuario, int idnota)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuarios> UsuarioExists(Usuarios usuario)
        {
            var usuarioRegistrado = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == usuario.NombreUsuario && u.CorreoElectronico == usuario.CorreoElectronico && u.Contraseña == usuario.Contraseña);

            return usuarioRegistrado;
        }


    }
}
