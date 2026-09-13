

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 5. Endpoint GET principal — "/"
app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        mensagem = "Laboratório HTTP e Postman",
        estado = "Aplicação em funcionamento"
    });
});

// 6. Endpoint GET com query string — "/saudacao"
app.MapGet("/saudacao", (string? nome) =>
{
    if (string.IsNullOrWhiteSpace(nome))
    {
        return Results.BadRequest(new
        {
            erro = "O parâmetro nome é obrigatório."
        });
    }

    return Results.Ok(new
    {
        mensagem = $"Olá, {nome}!"
    });
});

// 8. Endpoint POST — "/eco"
app.MapPost("/eco", (Mensagem entrada) =>
{
    if (string.IsNullOrWhiteSpace(entrada.Texto))
    {
        return Results.BadRequest(new
        {
            erro = "O campo texto é obrigatório."
        });
    }

    return Results.Created("/eco", new
    {
        recebido = entrada.Texto,
        tamanho = entrada.Texto.Length,
        maiusculas = entrada.Texto.ToUpper() 
    });
});

// 9/10. Endpoint GET — "/calcular"


app.MapGet("/calcular", (double a, double b, string? op) =>
{
    if (string.IsNullOrWhiteSpace(op))
    {
        return Results.BadRequest(new
        {
            erro = "O parâmetro op é obrigatório."
        });
    }

    var operacao = op.Trim().ToLower();

    if (operacao == "somar")
    {
        var resultado = a + b;
        return Results.Ok(new
        {
            a,
            b,
            operacao,
            resultado,
            descricao = $"{a} + {b} = {resultado}" // Exercício 14
        });
    }

    if (operacao == "subtrair")
    {
        var resultado = a - b;
        return Results.Ok(new
        {
            a,
            b,
            operacao,
            resultado,
            descricao = $"{a} - {b} = {resultado}"
        });
    }

    if (operacao == "multiplicar")
    {
        var resultado = a * b;
        return Results.Ok(new
        {
            a,
            b,
            operacao,
            resultado,
            descricao = $"{a} * {b} = {resultado}"
        });
    }

    if (operacao == "dividir")
    {
        if (b == 0)
        {
            return Results.BadRequest(new
            {
                erro = "Não é possível dividir por zero."
            });
        }

        var resultado = a / b;
        return Results.Ok(new
        {
            a,
            b,
            operacao,
            resultado,
            descricao = $"{a} / {b} = {resultado}"
        });
    }

    // Exercício 13 / Desafio 23 — potência
    if (operacao == "potencia")
    {
        var resultado = Math.Pow(a, b);
        return Results.Ok(new
        {
            a,
            b,
            operacao,
            resultado,
            descricao = $"{a} ^ {b} = {resultado}"
        });
    }

    // Exercício 12 — resto da divisão
    if (operacao == "resto")
    {
        if (b == 0)
        {
            return Results.BadRequest(new
            {
                erro = "Não é possível calcular o resto com divisor zero."
            });
        }

        var resultado = a % b;
        return Results.Ok(new
        {
            a,
            b,
            operacao,
            resultado,
            descricao = $"{a} % {b} = {resultado}"
        });
    }

    // Nenhuma operação reconhecida.
    return Results.BadRequest(new
    {
        erro = "Operação inválida.",
        operacoesPermitidas = new[]
        {
            "somar", "subtrair", "multiplicar", "dividir", "potencia", "resto"
        }
    });
});

// Inicia a aplicação.
app.Run();


public record Mensagem(string Texto);