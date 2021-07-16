using System.Collections.Generic;
using System.Threading.Tasks;
using Net5_Api.Controllers.Model;

namespace Net5_Api.Services
{
    public interface IDiretorService
    {
        Task<Diretor> Add(Diretor diretor);
        Task<Diretor> Delete(long id);
        Task<List<Diretor>> GetAll();
        Task<Diretor> GetById(long id);
        Task <Diretor> Update(Diretor diretor);
    }
}