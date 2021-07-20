using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net5_Api.Controllers.Model;

namespace Net5_Api.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly ApplicationDbContext _context;
        public FilmeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Filme>> GetAll()
        {
            var filmes = await _context.Filmes.ToListAsync();

            if (!filmes.Any())
            {
                throw new System.Exception("Não existem filmes cadastrados!");
            }

            return filmes;
        }

        public async Task<Filme> GetById(long id)
        {
            var filme = await _context.Filmes.Include(filme => filme.Diretor).FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                throw new System.Exception("Filme não encontrado!");
            }

            return filme;
        }

        public async Task<Diretor> GetDiretorId(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

            if (diretor == null)
            {
                throw new System.Exception("Diretor informado não encontrado!");
            }

            return diretor;
        }

        public async Task<Filme> Add(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task<Filme> Update(Filme filme, long idDiretor, int idFilme)
        {
            if (idDiretor == 0)
            {
                throw new System.Exception("Id informado não existe para atualização!");
            }

            filme.Id = idFilme;
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task<Filme> Delete(long id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
            _context.Remove(filme);
            await _context.SaveChangesAsync();
            return filme;
        }
    }
}