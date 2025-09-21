using System;
using System.Collections.Generic;

namespace Lab5estebanpacheco.Models;

public partial class estudiante
{
    public ulong id_estudiante { get; set; }

    public string nombre { get; set; } = null!;

    public int edad { get; set; }

    public string? direccion { get; set; }

    public string? telefono { get; set; }

    public string? correo { get; set; }
}
