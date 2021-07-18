using System.Collections.Generic;
using System.Threading.Tasks;
using Net5_Api.Controllers.Model;

namespace Net5_Api.Services
{
    public interface IFilmeService
    {
        Task<Filme> Add(Filme filme);
        Task<Filme> Delete(long id);
        Task<List<Filme>> GetAll();
        Task<Filme> GetById(long id);
        Task<Diretor> GetDiretorId(long id);
        Task<Filme> Update(Filme filme);
    }
}