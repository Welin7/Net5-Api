using System.Threading;
using System.Threading.Tasks;
using Net5_Api.Controllers.Model;
using Net5_Api.DTOs.Filme;

namespace Net5_Api.Services
{
    public interface IFilmeService
    {
        Task<Filme> Add(Filme filme);
        Task<Filme> Delete(long id);
        Task<FilmeListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
        Task<Filme> GetById(long id);
        Task<Diretor> GetDiretorId(long id);
        Task<Filme> Update(Filme filme, long id);
    }
}