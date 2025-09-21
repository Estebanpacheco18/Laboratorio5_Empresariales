namespace Lab5estebanpacheco.DTOs;

public class MatriculaDto
{
    public ulong id_matricula { get; set; }
    public int? id_estudiante { get; set; }
    public int? id_curso { get; set; }
    public DateTime fecha_matricula { get; set; }
    public string? semestre { get; set; }
}