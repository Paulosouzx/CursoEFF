
using ApiCrud.Data;
using ApiCrud.Estudantes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ApiCrud
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<AppDbContext>();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    "Server=PAULOSOUZX;Database=EstudantesDB;Integrated Security=True;TrustServerCertificate=True;"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //configurando as rotas
            app.AddRotasEstudantes();

            app.Run();
        }
    }
}
