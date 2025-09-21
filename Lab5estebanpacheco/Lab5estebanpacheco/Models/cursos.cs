using System;
using System.Collections.Generic;

namespace Lab5estebanpacheco.Models;

public partial class cursos
{
    public ulong id_curso { get; set; }

    public string nombre { get; set; } = null!;

    public string? descripcion { get; set; }

    public int creditos { get; set; }
    
    public ulong? id_profesor { get; set; }
    
    public virtual profesores? profesor { get; set; }
}
