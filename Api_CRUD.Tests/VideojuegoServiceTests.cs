using Api_CRUD.API.Controllers;
using Api_CRUD.Application.Interfaces;
using Api_CRUD.Application.Querys;
using Api_CRUD.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class VideojuegoServiceTests
{
    private readonly Mock<IVideojuegoRepository> _mockRepo;
    private readonly VideojuegosController _controller;
    private readonly Mock<IValidator<Videojuego>> _mockValidator;
 

    public VideojuegoServiceTests()
    {
        _mockRepo = new Mock<IVideojuegoRepository>();
        _mockValidator = new Mock<IValidator<Videojuego>>();
        _controller = new VideojuegosController(_mockRepo.Object, null, _mockValidator.Object);
    }

    [Fact]
    public async Task Create_ValidVideojuego_ReturnsOkResult()
    {
        var videojuego = new Videojuego { Nombre = "Test", Compañía = "Company", AñoLanzamiento = 2020 };
        _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Videojuego>())).ReturnsAsync(1);
        _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<Videojuego>(), default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var result = await _controller.Create(videojuego);

        Assert.IsType<OkObjectResult>(result);
    }

   

}
