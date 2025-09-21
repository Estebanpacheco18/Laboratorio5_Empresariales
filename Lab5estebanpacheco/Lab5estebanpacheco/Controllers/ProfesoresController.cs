using Lab5estebanpacheco.DTOs;
using Lab5estebanpacheco.Models;
using Lab5estebanpacheco.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab5estebanpacheco.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfesoresController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var profesores = await _unitOfWork.Repository<profesores>().GetAllAsync();
        var profesoresDto = profesores.Select(p => new ProfesorDto
        {
            id_profesor = p.id_profesor,
            nombre = p.nombre,
            especialidad = p.especialidad,
            correo = p.correo
        });

        return Ok(profesoresDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(ulong id)
    {
        var profesor = await _unitOfWork.Repository<profesores>().GetByIdAsync(id);
        if (profesor == null) return NotFound();

        var profesorDto = new ProfesorDto
        {
            id_profesor = profesor.id_profesor,
            nombre = profesor.nombre,
            especialidad = profesor.especialidad,
            correo = profesor.correo
        };

        return Ok(profesorDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProfesorDto profesorDto)
    {
        var profesor = new profesores
        {
            nombre = profesorDto.nombre,
            especialidad = profesorDto.especialidad,
            correo = profesorDto.correo
        };

        await _unitOfWork.Repository<profesores>().AddAsync(profesor);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = profesor.id_profesor }, profesorDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(ulong id, ProfesorDto profesorDto)
    {
        var profesor = await _unitOfWork.Repository<profesores>().GetByIdAsync(id);
        if (profesor == null) return NotFound();

        profesor.nombre = profesorDto.nombre;
        profesor.especialidad = profesorDto.especialidad;
        profesor.correo = profesorDto.correo;

        _unitOfWork.Repository<profesores>().Update(profesor);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(ulong id)
    {
        var profesor = await _unitOfWork.Repository<profesores>().GetByIdAsync(id);
        if (profesor == null) return NotFound();

        _unitOfWork.Repository<profesores>().Delete(profesor);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
