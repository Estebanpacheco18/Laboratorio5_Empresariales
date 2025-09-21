using System;
using System.Collections.Generic;

namespace Lab5estebanpacheco.Models;

public partial class profesores
{
    public ulong id_profesor { get; set; }

    public string nombre { get; set; } = null!;

    public string? especialidad { get; set; }

    public string? correo { get; set; }

    public virtual ICollection<cursos> cursos { get; set; } = new List<cursos>();
}
