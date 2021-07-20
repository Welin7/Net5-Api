using System.Collections.Generic;
using Net5_Api.Controllers.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Net5_Api.Services
{
    public class DiretorService : IDiretorService
    {
        private readonly ApplicationDbContext _context;
        public DiretorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Diretor>> GetAll()
        {
            var diretores = await _context.Diretores.ToListAsync();

            if (!diretores.Any())
            {
                throw new System.Exception("Não existem diretores cadastrados!");
            }

            return diretores;

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