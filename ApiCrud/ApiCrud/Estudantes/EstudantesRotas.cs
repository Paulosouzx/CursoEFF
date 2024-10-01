using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Estudantes
{
    public static class EstudantesRotas
    {

        //para ser um metodo de extensao igual os outros de program, usamos o this antes de webapplication
        public static void AddRotasEstudantes(this WebApplication app)
        {
            var rotasEstudantes = app.MapGroup("estudantes");

            rotasEstudantes.MapPost("", 
                async (AddEstudanteRequest request, AppDbContext context) =>
                {
                    bool jaExiste = await context.Estudantes
                        .AnyAsync(estudante => estudante.Nome == request.Nome);
                    
                    if(jaExiste) return Results.Conflict("Já Existe um estudante");
                
                Estudante novoEstudante = new Estudante(request.Nome);
                await context.Estudantes.AddAsync(novoEstudante);
                await context.SaveChangesAsync();
                
                return Results.Ok(novoEstudante);
            });
        }
    }
}
