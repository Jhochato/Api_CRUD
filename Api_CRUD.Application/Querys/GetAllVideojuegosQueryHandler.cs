using Api_CRUD.Application.Interfaces;
using Api_CRUD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_CRUD.Application.Querys
{
    public class GetAllVideojuegosQueryHandler
    {
        private readonly IVideojuegoRepository _repository;

        public GetAllVideojuegosQueryHandler(IVideojuegoRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<Videojuego>> Handle()
        {
            return await _repository.GetAllAsync();
        }
    }
}
