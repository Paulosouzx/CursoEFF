using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Estudantes
{
    public static class EstudantesRotas
    {

        //para ser um metodo de extensao igual os outros de program, usamos o this antes de webapplication
        public static void AddRotasEstudantes(this WebApplication app)
        {
            RouteGroupBuilder rotasEstudantes = app.MapGroup("estudantes");

            rotasEstudantes.MapPost("", 
                async (AddEstudanteRequest request, AppDbContext context, CancellationToken ct) =>
                {
                    bool jaExiste = await context.Estudantes
                        .AnyAsync(estudante => estudante.Nome == request.Nome, ct);
                    
                    if(jaExiste) return Results.Conflict("Já Existe um estudante");
                
                Estudante novoEstudante = new Estudante(request.Nome);
                await context.Estudantes.AddAsync(novoEstudante, ct);
                await context.SaveChangesAsync(ct);
                
                var estudanteRetorno = new EstudanteDto(novoEstudante.Id, novoEstudante.Nome);
                
                return Results.Ok(estudanteRetorno);
            });
            
            //Retornar todos os estudantes
            rotasEstudantes.MapGet("", async (AppDbContext context, CancellationToken ct) =>
            {
                List<EstudanteDto> estudantes = await context.Estudantes
                    .Where(x => x.Ativo)
                    .Select(estudante => new EstudanteDto(estudante.Id, estudante.Nome))
                    .ToListAsync(ct);
                
                return estudantes;
            });
            
            //Modificar nome estudantes
            rotasEstudantes.MapPut("{id:guid}", async (Guid id, UpdateStudentRequest request, AppDbContext context,  CancellationToken ct) =>
            {
                Estudante? estudante = await context.Estudantes
                    .SingleOrDefaultAsync(estudante => estudante.Id == id, ct);
                
                if(estudante == null)
                    return Results.NotFound();
                
                estudante.AtualizarNome(request.Nome);
                await context.SaveChangesAsync(ct);
                return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome)); 
            });
            
            //Deletar Estudante
            rotasEstudantes.MapDelete("{id}",
                async (Guid id, AppDbContext context, CancellationToken ct) =>
            {
                var estudante = await context.Estudantes
                    .SingleOrDefaultAsync(estudante => estudante.Id == id, ct);
                    
                if(estudante == null) return Results.NotFound();
                
                estudante.Desativar();
                
                await context.SaveChangesAsync(ct);

                return Results.Ok();
            });
        }
    }
}
