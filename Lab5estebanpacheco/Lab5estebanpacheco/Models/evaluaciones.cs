using System;
using System.Collections.Generic;

namespace Lab5estebanpacheco.Models;

public partial class evaluaciones
{
    public ulong id_evaluacion { get; set; }

    public int? id_estudiante { get; set; }

    public int? id_curso { get; set; }

    public decimal? calificacion { get; set; }

    public DateOnly? fecha { get; set; }
}
