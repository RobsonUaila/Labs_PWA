using System.Diagnostics;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// MIDDLEWARE 1 - REGISTO DE PEDIDOS

app.Use(async (context, next) =>
{
    var metodo = context.Request.Method;
    var caminho = context.Request.Path;
    Console.WriteLine($"[ENTRADA] {metodo} {caminho}");
    await next();
    var status = context.Response.StatusCode;
    Console.WriteLine($"[SAÍDA] {metodo} {caminho} -> {status}");
});

// MIDDLEWARE 2 - MEDIÇÃO DO TEMPO

app.Use(async (context, next) =>
{
    var cronometro = Stopwatch.StartNew();
    context.Response.OnStarting(() =>
    {
        cronometro.Stop();
        context.Response.Headers["X-Response-Time-ms"] =
        cronometro.ElapsedMilliseconds.ToString();
        return Task.CompletedTask;
    });
    await next();
});


// MIDDLEWARE 3 - HEADER OBRIGATÓRIO

app.Use(async (context, next) =>
{
if (!context.Request.Headers.TryGetValue("X-Lab-Key", out var chave))
{
context.Response.StatusCode = StatusCodes.Status400BadRequest;
await context.Response.WriteAsJsonAsync(new
{
    erro = "O cabeçalho X-Lab-Key é obrigatório."
});
return;
}
if (chave.ToString() != "USTM2026")
{
    context.Response.StatusCode = StatusCodes.Status403Forbidden;
    await context.Response.WriteAsJsonAsync(new
    {
        erro = "Valor de X-Lab-Key inválido."
    });
    return;
}
await next();
});

// ENDPOINTS

app.MapGet("/", () => Results.Ok(new
{
    mensagem = "Laboratório de Middleware",
    estado = "Aplicação activa"
}));
app.MapGet("/lento", async () =>
{
    await Task.Delay(500);
    return Results.Ok(new
    {
        mensagem = "Operação concluída",
        atrasoSimuladoMs = 500
    });
});
app.Run();