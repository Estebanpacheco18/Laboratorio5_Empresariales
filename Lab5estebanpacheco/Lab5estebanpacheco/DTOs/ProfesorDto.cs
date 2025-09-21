namespace Lab5estebanpacheco.DTOs;

public class ProfesorDto
{
    public ulong id_profesor { get; set; }
    public string nombre { get; set; } = null!;
    public string? especialidad { get; set; }
    public string? correo { get; set; }
}