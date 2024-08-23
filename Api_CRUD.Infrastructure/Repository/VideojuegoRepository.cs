using Api_CRUD.Application.Interfaces;
using Api_CRUD.Domain.Entities;
using Api_CRUD.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Api_CRUD.Infrastructure.Repository
{
    public class VideojuegoRepository : IVideojuegoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideojuegoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Videojuego>> GetAllAsync()
        {
            return await _context.Videojuegos.ToListAsync();
        }

        public async Task<IEnumerable<Videojuego>> GetPagedAsync(int pageNumber, int pageSize, string nombre, string compañia, int? añoLanzamiento)
        {
            var query = _context.Videojuegos.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(v => v.Nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(compañia))
                query = query.Where(v => v.Compañía.Contains(compañia));

            if (añoLanzamiento.HasValue)
                query = query.Where(v => v.AñoLanzamiento == añoLanzamiento.Value);

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<Videojuego> GetByIdAsync(int id)
        {
            return await _context.Videojuegos.FindAsync(id);
        }

        public async Task<int> CreateAsync(Videojuego videojuego)
        {
            _context.Videojuegos.Add(videojuego);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Videojuego videojuego)
        {
            _context.Videojuegos.Update(videojuego);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var videojuego = await _context.Videojuegos.FindAsync(id);
            if (videojuego != null)
            {
                _context.Videojuegos.Remove(videojuego);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
