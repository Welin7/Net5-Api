using Net5_Api.Controllers.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Net5_Api.DTOs.Diretor;
using System;
using Net5_Api.Extensions;


namespace Net5_Api.Services
{
    public class DiretorService : IDiretorService
    {
        private readonly ApplicationDbContext _context;
        public DiretorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DiretorListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken)
        {
            var pagedModel = await _context.Diretores
                    .AsNoTracking()
                    .OrderBy(p => p.Id)
                    .PaginateAsync(page, limit, cancellationToken);

            if (!pagedModel.Items.Any())
            {
                throw new Exception("Não existem diretores cadastrados!");
            }

            var CurrentPage = pagedModel.CurrentPage;
            var TotalPages = pagedModel.TotalPages;
            var TotalItems = pagedModel.TotalItems;
            var Items = pagedModel.Items.Select(diretor => new DiretorOutputGetAllDTO(diretor.Id, diretor.Nome)).ToList();

            DiretorListOutputGetAllDTO listOutputGetAllDTO = new DiretorListOutputGetAllDTO(CurrentPage, TotalPages, TotalItems, Items);

            return listOutputGetAllDTO;
        }
        public async Task<Diretor> GetById(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

            if (diretor == null)
            {
                throw new System.Exception("Não existem diretores cadastrados!");
            }

            return diretor;
        }

        public async Task<Diretor> Add(Diretor diretor)
        {
            _context.Diretores.Add(diretor);
            await _context.SaveChangesAsync();
            return diretor;
        }

        public async Task<Diretor> Update(Diretor diretor, long id)
        {
            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();
            return diretor;
        }

        public async Task<Diretor> Delete(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
            _context.Remove(diretor);
            await _context.SaveChangesAsync();
            return diretor;
        }
    }
}