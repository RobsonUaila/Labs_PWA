var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();


var estudantes = new Dictionary<int, Estudante>
{
    [1] = new Estudante(1, "Ana sitoe", "Engenharia Informática"),
    [2] = new Estudante(2, "Paulo José", "Engenharia de Telecomunicações"),
    [3] = new Estudante(3, "Marta Carlos", "Informática")
};

var cursos = new Dictionary<int, Curso>
{
    [1] = new Curso(1, "Engenharia Informática", 4),
    [2] = new Curso(2, "Engenharia de Telecomunicações", 4)
};

var disciplinas = new Dictionary<int, Disciplina>
{
    [1] = new Disciplina(1, "Programação Web Avançada", "ASP.NET Core"),
    [2] = new Disciplina(2, "Segurança de Redes", "Fundamentos de segurança")
};


string Create_page(string titulo, string conteudo)
{
    return $"""
    <!DOCTYPE html>
    <html lang="pt">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>{titulo}</title>
        <link rel="stylesheet" href="/css/site.css">
    </head>
    <body>
        <header>
            <div class="marca container">
                <img src="/images/ustm.png" alt="Logótipo USTM">
                <div>
                    <h1>{titulo}</h1>
                    <p>Laboratório de Routing</p>
                </div>
            </div>
        </header>
        <main class="container">
            {conteudo}
            <p><a href="/">Voltar à página inicial</a></p>
        </main>
        <footer>Aula 8 — Routing no ASP.NET Core</footer>
    </body>
    </html>
    """;
}


app.MapGet("/estudantes/{id:int}", (int id) =>
{
    if (!estudantes.TryGetValue(id, out var estudante))
    {
        var erro = $"""
        <section class="card erro">
            <h2>Estudante não encontrado</h2>
            <p>Não existe estudante com o identificador <strong>{id}</strong>.</p>
        </section>
        """;

        return Results.Content(
            Create_page("Estudante não encontrado", erro),
            "text/html; charset=utf-8",
            statusCode: StatusCodes.Status404NotFound);
    }

    var conteudo = $"""
    <section class="card">
        <h2>Dados do estudante</h2>
        <p><strong>Id:</strong> {estudante.Id}</p>
        <p><strong>Nome:</strong> {estudante.Nome}</p>
        <p><strong>Curso:</strong> {estudante.Curso}</p>
    </section>
    """;

    return Results.Content(
        Create_page($"Estudante {estudante.Id}", conteudo),
        "text/html; charset=utf-8");
});


app.MapGet("/cursos/{id:int}", (int id) =>
{
    if (!cursos.TryGetValue(id, out var curso))
    {
        var erro = $"""
        <section class="card erro">
            <h2>Curso não encontrado</h2>
            <p>Não existe curso com o identificador <strong>{id}</strong>.</p>
        </section>
        """;

        return Results.Content(
            Create_page("Curso não encontrado", erro),
            "text/html; charset=utf-8",
            statusCode: StatusCodes.Status404NotFound);
    }

    var conteudo = $"""
    <section class="card">
        <h2>Dados do curso</h2>
        <p><strong>Id:</strong> {curso.Id}</p>
        <p><strong>Nome:</strong> {curso.Nome}</p>
        <p><strong>Duração:</strong> {curso.DuracaoAnos} anos</p>
    </section>
    """;

    return Results.Content(
        Create_page($"Curso {curso.Id}", conteudo),
        "text/html; charset=utf-8");
});


app.MapGet("/disciplinas/{id:int}", (int id) =>
{
    if (!disciplinas.TryGetValue(id, out var disciplina))
    {
        var erro = $"""
        <section class="card erro">
            <h2>Disciplina não encontrada</h2>
            <p>Não existe disciplina com o identificador <strong>{id}</strong>.</p>
        </section>
        """;

        return Results.Content(
            Create_page("Disciplina não encontrada", erro),
            "text/html; charset=utf-8",
            statusCode: StatusCodes.Status404NotFound);
    }

    var conteudo = $"""
    <section class="card">
        <h2>Dados da disciplina</h2>
        <p><strong>Id:</strong> {disciplina.Id}</p>
        <p><strong>Nome:</strong> {disciplina.Nome}</p>
        <p><strong>Descrição:</strong> {disciplina.Descricao}</p>
    </section>
    """;

    return Results.Content(
        Create_page($"Disciplina {disciplina.Id}", conteudo),
        "text/html; charset=utf-8");
});

app.Run();


public record Estudante(int Id, string Nome, string Curso);
public record Curso(int Id, string Nome, int DuracaoAnos);
public record Disciplina(int Id, string Nome, string Descricao);