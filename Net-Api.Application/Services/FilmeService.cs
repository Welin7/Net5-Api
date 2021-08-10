using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net5_Api.Controllers.Model;
using Net5_Api.DTOs.Filme;
using Net5_Api.Extensions;


namespace Net5_Api.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly ApplicationDbContext _context;
        public FilmeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FilmeListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken)
        {
            var pagedModel = await _context.Filmes
                    .AsNoTracking()
                    .OrderBy(p => p.Id)
                    .PaginateAsync(page, limit, cancellationToken);

            if (!pagedModel.Items.Any())
            {
                throw new Exception("Não existem filmes cadastrados!");
            }

            var CurrentPage = pagedModel.CurrentPage;
            var TotalPages = pagedModel.TotalPages;
            var TotalItems = pagedModel.TotalItems;
            var Items = pagedModel.Items.Select(filme => new FilmeOutputGetAllDTO(filme.Id, filme.Titulo, filme.Ano)).ToList();

            FilmeListOutputGetAllDTO listOutputGetAllDTO = new FilmeListOutputGetAllDTO(CurrentPage, TotalPages, TotalItems, Items);

            return listOutputGetAllDTO;
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