using System.Threading;
using System.Threading.Tasks;
using Net5_Api.Controllers.Model;
using Net5_Api.DTOs.Diretor;

namespace Net5_Api.Services
{
    public interface IDiretorService
    {
        Task<DiretorListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
        Task<Diretor> Add(Diretor diretor);
        Task<Diretor> Delete(long id);
        Task<Diretor> GetById(long id);
        Task <Diretor> Update(Diretor diretor, long id);
    }
}