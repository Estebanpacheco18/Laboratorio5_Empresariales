namespace Lab5estebanpacheco.DTOs;

public class CursoDto
{
    public ulong id_curso { get; set; }
    public string nombre { get; set; } = null!;
    public string? descripcion { get; set; }
    public int creditos { get; set; }
}
