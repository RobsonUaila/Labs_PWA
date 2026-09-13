var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Ola, Sou o Robson Uaila, Estudante de Desenvolvimento de Software. Esta e a minha primeira aplicao em ASP.NET CORE" );
app.MapGet("/disciplina", ()=> " Programacao WEB avancada");
app.MapGet("/universidade", () => "Universidade Sao Tomas de Mocambique!");
app.Run();
