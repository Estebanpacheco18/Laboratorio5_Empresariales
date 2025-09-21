using Lab5estebanpacheco.DTOs;
using Lab5estebanpacheco.Models;
using Lab5estebanpacheco.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lab5estebanpacheco.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MatriculasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var matriculas = await _unitOfWork.Repository<matriculas>().GetAllAsync();
        var matriculasDto = matriculas.Select(m => new MatriculaDto
        {
            id_matricula = m.id_matricula,
            id_estudiante = m.id_estudiante,
            id_curso = m.id_curso,
            semestre = m.semestre
        });

        return Ok(matriculasDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(ulong id)
    {
        var matricula = await _unitOfWork.Repository<matriculas>().GetByIdAsync(id);
        if (matricula == null) return NotFound();

        var matriculaDto = new MatriculaDto
        {
            id_matricula = matricula.id_matricula,
            id_estudiante = matricula.id_estudiante,
            id_curso = matricula.id_curso,
            semestre = matricula.semestre
        };

        return Ok(matriculaDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MatriculaDto matriculaDto)
    {
        var matricula = new matriculas
        {
            id_estudiante = matriculaDto.id_estudiante,
            id_curso = matriculaDto.id_curso,
            semestre = matriculaDto.semestre
        };

        await _unitOfWork.Repository<matriculas>().AddAsync(matricula);
        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = matricula.id_matricula }, matriculaDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(ulong id, MatriculaDto matriculaDto)
    {
        var matricula = await _unitOfWork.Repository<matriculas>().GetByIdAsync(id);
        if (matricula == null) return NotFound();

        matricula.id_estudiante = matriculaDto.id_estudiante;
        matricula.id_curso = matriculaDto.id_curso;
        matricula.semestre = matriculaDto.semestre;

        _unitOfWork.Repository<matriculas>().Update(matricula);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(ulong id)
    {
        var matricula = await _unitOfWork.Repository<matriculas>().GetByIdAsync(id);
        if (matricula == null) return NotFound();

        _unitOfWork.Repository<matriculas>().Delete(matricula);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

    [HttpPost("AsignarEstudiante")]
    public async Task<IActionResult> AsignarEstudiante(ulong idCurso, ulong idEstudiante)
    {
        var curso = await _unitOfWork.Repository<cursos>().GetByIdAsync(idCurso);
        if (curso == null) return NotFound("Curso no encontrado");

        var estudiante = await _unitOfWork.Repository<estudiante>().GetByIdAsync(idEstudiante);
        if (estudiante == null) return NotFound("Estudiante no encontrado");

        var matricula = new matriculas
        {
            id_curso = (int?)idCurso,
            id_estudiante = (int?)idEstudiante,
            semestre = "2023-II"
        };

        await _unitOfWork.Repository<matriculas>().AddAsync(matricula);
        await _unitOfWork.SaveAsync();

        return Ok("Estudiante asignado al curso");
    }

    [HttpPost("AsignarProfesor")]
    public async Task<IActionResult> AsignarProfesor(ulong idCurso, ulong idProfesor)
    {
        var curso = await _unitOfWork.Repository<cursos>().GetByIdAsync(idCurso);
        if (curso == null) return NotFound("Curso no encontrado");

        var profesor = await _unitOfWork.Repository<profesores>().GetByIdAsync(idProfesor);
        if (profesor == null) return NotFound("Profesor no encontrado");
        
        return Ok("Profesor asignado al curso");
    }

    [HttpGet("ListarParticipantes/{idCurso}")]
    public async Task<IActionResult> ListarParticipantes(ulong idCurso)
    {
        var curso = await _unitOfWork.Repository<cursos>().GetByIdAsync(idCurso);
        if (curso == null) return NotFound("Curso no encontrado");

        var matriculas = await _unitOfWork.Repository<matriculas>().GetAllAsync();
        var estudiantes = matriculas
            .Where(m => m.id_curso == (int?)idCurso)
            .Select(m => m.id_estudiante)
            .Distinct();

        var estudiantesInfo = await Task.WhenAll(estudiantes.Select(async id =>
        {
            var estudiante = await _unitOfWork.Repository<estudiante>().GetByIdAsync((ulong)id);
            return estudiante?.nombre;
        }));

        // Ajusta la lógica para obtener el profesor relacionado con el curso
        var profesor = await _unitOfWork.Repository<profesores>().GetByIdAsync(curso.id_profesor.Value);

        return Ok(new
        {
            Curso = curso.nombre,
            Profesor = profesor?.nombre,
            Estudiantes = estudiantesInfo
        });
    }
}
