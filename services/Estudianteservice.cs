using sistemaEstudiante.Models;

namespace sistemaEstudiante.Services;

public class EstudianteService
{
    private readonly List<Estudiante> _estudiantes = new()
    {
        new Estudiante
        {
            Id = 1,
            Nombre = "wirmel damian",
            Matricula = "2026-001",
            Edad = 8
        },
        new Estudiante
        {
            Id = 2,
            Nombre = "wirmel sadiel",
            Matricula = "2026-002",
            Edad = 5
        }
    };

    public List<Estudiante> FindAll()
    {
        return _estudiantes;
    }

    public bool Delete(int id)
    {
        var estudiante = _estudiantes.FirstOrDefault(e => e.Id == id);

        if (estudiante is null)
        {
            return false;
        }

        _estudiantes.Remove(estudiante);
        return true;
    }

    public bool Create(Estudiante estudiante)
    {
        _estudiantes.Add(estudiante);
        return true;
    }
}