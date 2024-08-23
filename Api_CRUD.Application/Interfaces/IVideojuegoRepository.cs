using Api_CRUD.Domain.Entities;

namespace Api_CRUD.Application.Interfaces;

public interface IVideojuegoRepository
{
    Task<IEnumerable<Videojuego>> GetAllAsync();
    Task<IEnumerable<Videojuego>> GetPagedAsync(int pageNumber, int pageSize, string nombre, string compañia, int? añoLanzamiento);
    Task<Videojuego> GetByIdAsync(int id);
    Task<int> CreateAsync(Videojuego videojuego);
    Task<int> UpdateAsync(Videojuego videojuego);
    Task<int> DeleteAsync(int id);
}
