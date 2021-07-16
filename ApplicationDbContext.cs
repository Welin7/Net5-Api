using System;
using Microsoft.EntityFrameworkCore;
using Net5_Api.Controllers.Model;

namespace Net5_Api
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Diretor> Diretores { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}