using System;
using System.Collections.Generic;

namespace Lab5estebanpacheco.Models;

public partial class matriculas
{
    public ulong id_matricula { get; set; }

    public int? id_estudiante { get; set; }

    public int? id_curso { get; set; }

    public string? semestre { get; set; }
}
