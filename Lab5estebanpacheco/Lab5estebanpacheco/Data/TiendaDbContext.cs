using Lab5estebanpacheco.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab5estebanpacheco.Data;

public partial class TiendaDbContext : DbContext
{
    public TiendaDbContext()
    {
    }

    public TiendaDbContext(DbContextOptions<TiendaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<asistencias> asistencias { get; set; }
    public virtual DbSet<cursos> cursos { get; set; }
    public virtual DbSet<estudiante> estudiantes { get; set; }
    public virtual DbSet<evaluaciones> evaluaciones { get; set; }
    public virtual DbSet<materias> materias { get; set; }
    public virtual DbSet<matriculas> matriculas { get; set; }
    public virtual DbSet<profesores> profesores { get; set; }
    public virtual DbSet<user> users { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=db;User=root;Password=1234;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.43-mysql"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<cursos>().HasKey(c => c.id_curso);
        modelBuilder.Entity<estudiante>().HasKey(e => e.id_estudiante);
        modelBuilder.Entity<evaluaciones>().HasKey(e => e.id_evaluacion);
        modelBuilder.Entity<materias>().HasKey(m => m.id_materia);
        modelBuilder.Entity<matriculas>().HasKey(m => m.id_matricula);
        modelBuilder.Entity<profesores>().HasKey(p => p.id_profesor);
        modelBuilder.Entity<asistencias>().HasKey(a => a.id_asistencia);

        // Configuración de la relación entre cursos y profesores
        modelBuilder.Entity<cursos>()
            .HasOne(c => c.profesor)
            .WithMany(p => p.cursos)
            .HasForeignKey(c => c.id_profesor)
            .HasConstraintName("FK_Cursos_Profesores");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}