using Microsoft.AspNetCore.Mvc;

namespace APINotificacionesV2.Models.Repository.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAll();
        Task<IQueryable<TEntity>> GetById(int id);
        Task<IQueryable<TEntity>> GetById(int idusuario, int idnota);
        Task<TEntity> UsuarioExists(TEntity entity);
        Task<TEntity> Update(int id, TEntity entity);
        Task<bool> Delete(int id);
        Task<bool> Delete(int idusuario,int idnota);
        Task<TEntity> Create(TEntity entity);

    }
}
