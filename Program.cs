using sistemaEstudiante.Models;
using sistemaEstudiante.Services;
using Spectre.Console;

class Program
{
    public static bool running = true;

    public static string[] choices =
    [
        "1. Mostrar estudiantes",
        "2. Eliminar un estudiante",
        "3. Agregar un estudiante",
        "4. Salir"
    ];

    public static EstudianteService estudianteService = new();

    public static void Main()
    {
        while (running)
        {
            var selections = new SelectionPrompt<string>()
                .Title("[green]Indica la acción a realizar:[/]")
                .HighlightStyle("DarkGoldenrod")
                .AddChoices(choices);

            var option = AnsiConsole.Prompt(selections);

            List<Estudiante> estudiantes = estudianteService.FindAll();

            switch (option)
            {
                case "1. Mostrar estudiantes":
                    AnsiConsole.Clear();

                    var table = new Table();
                    table.AddColumn("Id");
                    table.AddColumn("Nombre");
                    table.AddColumn("Matrícula");
                    table.AddColumn("Edad");

                    foreach (Estudiante estudiante in estudiantes)
                    {
                        table.AddRow(
                            estudiante.Id.ToString(),
                            estudiante.Nombre,
                            estudiante.Matricula,
                            estudiante.Edad.ToString()
                        );
                    }

                    AnsiConsole.Write(table);
                    break;

                case "2. Eliminar un estudiante":
                    AnsiConsole.Clear();

                    int id = AnsiConsole.Ask<int>(
                        "Indica el ID del estudiante que quieres eliminar: "
                    );

                    var confirmed = AnsiConsole.Confirm("¿Estás segura?");

                    if (confirmed)
                    {
                        bool deleted = estudianteService.Delete(id);

                        if (deleted)
                        {
                            AnsiConsole.MarkupLine(
                                "\n[green]¡Estudiante eliminado![/]\n"
                            );
                        }
                        else
                        {
                            AnsiConsole.MarkupLine(
                                "\n[red]¡Estudiante no encontrado![/]\n"
                            );
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine(
                            "\n[yellow]¡Operación cancelada![/]\n"
                        );
                    }

                    break;

                case "3. Agregar un estudiante":
                    AnsiConsole.Clear();

                    string nombre = AnsiConsole.Ask<string>(
                        "[yellow]Indica el nombre del estudiante: [/]"
                    );

                    string matricula = AnsiConsole.Ask<string>(
                        "[yellow]Indica la matrícula: [/]"
                    );

                    int edad = AnsiConsole.Ask<int>(
                        "[yellow]Indica la edad: [/]"
                    );

                    int nuevoId = estudiantes.Count > 0
                        ? estudiantes.Max(e => e.Id) + 1
                        : 1;

                    Estudiante nuevoEstudiante = new()
                    {
                        Id = nuevoId,
                        Nombre = nombre,
                        Matricula = matricula,
                        Edad = edad
                    };

                    estudianteService.Create(nuevoEstudiante);

                    AnsiConsole.MarkupLine(
                        "\n[green]¡Estudiante agregado![/]\n"
                    );

                    break;

                case "4. Salir":
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine(
                        "[green]¡Fin del programa![/]"
                    );

                    running = false;
                    break;
            }
        }
    }
}
