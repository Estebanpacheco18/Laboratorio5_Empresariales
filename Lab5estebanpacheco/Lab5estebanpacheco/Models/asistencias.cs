using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab5estebanpacheco.Models;

public partial class asistencias
{
    [Key]
    public ulong id_asistencia { get; set; }

    public int? id_estudiante { get; set; }

    public int? id_curso { get; set; }

    public DateOnly? fecha { get; set; }

    public string? estado { get; set; }
}
