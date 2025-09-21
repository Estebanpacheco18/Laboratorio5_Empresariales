namespace Lab5estebanpacheco.DTOs;

public class EstudianteDto
{
    public ulong id_estudiante { get; set; }
    public string nombre { get; set; } = null!;
    public int edad { get; set; }
    public string? direccion { get; set; }
    public string? telefono { get; set; }
    public string? correo { get; set; }
}