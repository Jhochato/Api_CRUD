using Api_CRUD.Application.Interfaces;
using Api_CRUD.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api_CRUD.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VideojuegosController : ControllerBase
    {
        private readonly IVideojuegoRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly IValidator<Videojuego> _validator;

        public VideojuegosController(IVideojuegoRepository repository, IMemoryCache cache, IValidator<Videojuego> validator)
        {
            _repository = repository;
            _cache = cache;
            _validator = validator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            if (!_cache.TryGetValue("AllVideojuegos", out IEnumerable<Videojuego> videojuegos))
            {
                videojuegos = await _repository.GetAllAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set("AllVideojuegos", videojuegos, cacheEntryOptions);
            }

            return Ok(videojuegos);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(int pageNumber = 1, int pageSize = 5, string nombre = "", string compañia = "", int? añoLanzamiento = null)
        {
            var videojuegos = await _repository.GetPagedAsync(pageNumber, pageSize, nombre, compañia, añoLanzamiento);
            return Ok(videojuegos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var videojuego = await _repository.GetByIdAsync(id);
            if (videojuego == null) return NotFound();
            return Ok(videojuego);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Videojuego videojuego)
        {
            var validationResult = await _validator.ValidateAsync(videojuego);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var result = await _repository.CreateAsync(videojuego);
            if (result > 0) return Ok(result);
            return StatusCode(500, "Error al crear el videojuego");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Videojuego videojuego)
        {
            if (id != videojuego.Id) return BadRequest("ID no coincide");

            var validationResult = await _validator.ValidateAsync(videojuego);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var result = await _repository.UpdateAsync(videojuego);
            if (result > 0) return Ok(result);
            return StatusCode(500, "Error al actualizar el videojuego");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result > 0) return Ok(result);
            return NotFound("Videojuego no encontrado");
        }
    }
}
