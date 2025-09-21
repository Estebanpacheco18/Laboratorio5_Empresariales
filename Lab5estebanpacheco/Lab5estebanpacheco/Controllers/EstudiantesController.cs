using Lab5estebanpacheco.DTOs;
using Lab5estebanpacheco.Models;
using Lab5estebanpacheco.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab5estebanpacheco.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EstudiantesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var estudiantes = await _unitOfWork.Repository<estudiante>().GetAllAsync();
        var estudiantesDto = estudiantes.Select(e => new EstudianteDto
        {
            id_estudiante = e.id_estudiante,
            nombre = e.nombre,
            edad = e.edad,
            direccion = e.direccion,
            telefono = e.telefono,
            correo = e.correo
        });

        return Ok(estudiantesDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(ulong id)
    {
        var estudiante = await _unitOfWork.Repository<estudiante>().GetByIdAsync(id);
        if (estudiante == null) return NotFound();

        var estudianteDto = new EstudianteDto
        {
            id_estudiante = estudiante.id_estudiante,
            nombre = estudiante.nombre,
            edad = estudiante.edad,
            direccion = estudiante.direccion,
            telefono = estudiante.telefono,
            correo = estudiante.correo
        };

        return Ok(estudianteDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EstudianteDto estudianteDto)
    {
        var estudiante = new estudiante
        {
            nombre = estudianteDto.nombre,
            edad = estudianteDto.edad,
            direccion = estudianteDto.direccion,
            telefono = estudianteDto.telefono,
            correo = estudianteDto.correo
        };

        await _unitOfWork.Repository<estudiante>().AddAsync(estudiante);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = estudiante.id_estudiante }, estudianteDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(ulong id, EstudianteDto estudianteDto)
    {
        var estudiante = await _unitOfWork.Repository<estudiante>().GetByIdAsync(id);
        if (estudiante == null) return NotFound();

        estudiante.nombre = estudianteDto.nombre;
        estudiante.edad = estudianteDto.edad;
        estudiante.direccion = estudianteDto.direccion;
        estudiante.telefono = estudianteDto.telefono;
        estudiante.correo = estudianteDto.correo;

        _unitOfWork.Repository<estudiante>().Update(estudiante);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(ulong id)
    {
        var estudiante = await _unitOfWork.Repository<estudiante>().GetByIdAsync(id);
        if (estudiante == null) return NotFound();

        _unitOfWork.Repository<estudiante>().Delete(estudiante);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
