using Api_CRUD.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_CRUD.Application.Validators
{
    public class VideojuegoValidator : AbstractValidator<Videojuego>
    {
        public VideojuegoValidator()
        {
            RuleFor(v => v.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(v => v.Compañía).NotEmpty().WithMessage("La compañía es obligatoria.");
            RuleFor(v => v.AñoLanzamiento).InclusiveBetween(1950, DateTime.Now.Year).WithMessage("El año de lanzamiento no es válido.");
        }
    }
}
