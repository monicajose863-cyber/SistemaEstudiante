namespace sistemaEstudiante.Models;

public class Estudiante {
  public required int Id { get; set; }
  public required string Nombre { get; set; }
  public required string Matricula { get; set; }
  public required int Edad { get; set; }
}