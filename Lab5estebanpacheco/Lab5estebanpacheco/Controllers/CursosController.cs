using Lab5estebanpacheco.DTOs;
using Lab5estebanpacheco.Models;
using Lab5estebanpacheco.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab5estebanpacheco.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CursosController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cursos = await _unitOfWork.Repository<cursos>().GetAllAsync();
        var cursosDto = cursos.Select(c => new CursoDto
        {
            id_curso = c.id_curso,
            nombre = c.nombre,
            descripcion = c.descripcion,
            creditos = c.creditos
        });

        return Ok(cursosDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(ulong id)
    {
        var curso = await _unitOfWork.Repository<cursos>().GetByIdAsync(id);
        if (curso == null) return NotFound();

        var cursoDto = new CursoDto
        {
            id_curso = curso.id_curso,
            nombre = curso.nombre,
            descripcion = curso.descripcion,
            creditos = curso.creditos
        };

        return Ok(cursoDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CursoDto cursoDto)
    {
        var curso = new cursos
        {
            nombre = cursoDto.nombre,
            descripcion = cursoDto.descripcion,
            creditos = cursoDto.creditos
        };

        await _unitOfWork.Repository<cursos>().AddAsync(curso);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = curso.id_curso }, cursoDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(ulong id, CursoDto cursoDto)
    {
        var curso = await _unitOfWork.Repository<cursos>().GetByIdAsync(id);
        if (curso == null) return NotFound();

        curso.nombre = cursoDto.nombre;
        curso.descripcion = cursoDto.descripcion;
        curso.creditos = cursoDto.creditos;

        _unitOfWork.Repository<cursos>().Update(curso);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(ulong id)
    {
        var curso = await _unitOfWork.Repository<cursos>().GetByIdAsync(id);
        if (curso == null) return NotFound();

        _unitOfWork.Repository<cursos>().Delete(curso);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
