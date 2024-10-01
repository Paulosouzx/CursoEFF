using ApiCrud.Estudantes;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Vai falar que é uma tabela pro meu sistema
        public DbSet<Estudante> Estudantes { get; set; }
    }
}
