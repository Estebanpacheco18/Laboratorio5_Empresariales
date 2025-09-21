using System;
using System.Collections.Generic;

namespace Lab5estebanpacheco.Models;

public partial class materias
{
    public ulong id_materia { get; set; }

    public int? id_curso { get; set; }

    public string nombre { get; set; } = null!;

    public string? descripcion { get; set; }
}
