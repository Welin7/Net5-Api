using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Net5_Api.Controllers.Model;
using Net5_Api.DTOs.Filme;
using Net5_Api.Services;

namespace Net5_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeService _filmeService;

        public FilmeController(IFilmeService FilmeService)
        {
            _filmeService = FilmeService;
        }

        /// <summary>
        /// O método Get retorna uma lista de todos os filmes do banco.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/filme
        ///     {
        ///        "id": 2,
        ///        "titulo": "Titanic",
        ///        "ano": null
        ///     },
        ///     {
        ///        "id": 3,
        ///        "titulo": "Et O Extraterrestre",
        ///        "ano": null
        ///     } 
        ///       
        /// </remarks>
        /// <returns>Todos os filmes já cadastrados no banco</returns>
        /// <response code="200">Filmes listados com sucesso</response>

        // GET api/filmes
        [HttpGet]
        public async Task<ActionResult<FilmeListOutputGetAllDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1)
        {
            return await _filmeService.GetByPageAsync(limit, page, cancellationToken);
        }

        /// <summary>
        /// O método Get retorna um registro do filme de acordo com o parâmetro id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/filme/id
        ///     {
        ///        "id": 2,
        ///        "titulo": "Et O Extraterrestre",
        ///        "nomeDoDiretor": "Steven Spielberg"
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do filme</param>
        /// <returns>Registro do filme informado como parâmetro</returns>
        /// <response code="200">Filme localizado sucesso</response>

        // GET api/filmes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id)
        {
            var filme = await _filmeService.GetById(id);

            var outputDTO = new FilmeOutputGetByIdDTO(filme.Id, filme.Titulo, filme.Diretor.Nome);
            return Ok(outputDTO);
        }

        /// <summary>
        /// O método Post registra um filme no banco de acordo com o nome informado e id do diretor.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/filme
        ///     {
        ///        "titulo": "Et O Extraterrestre",
        ///        "diretorId": 3
        ///     } 
        ///       
        /// </remarks>
        /// <param name="inputDTO">Titulo do filme e id do diretor</param>
        /// <returns>O filme cadastrado no banco</returns>
        /// <response code="200">Filme criado com sucesso</response>
        /// <response code="500">Erro interno inesperado</response>

        // POST api/filmes
        [HttpPost]
        public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO inputDTO)
        {
            var diretor = await _filmeService.GetDiretorId(inputDTO.DiretorId);

            var filme = new Filme(inputDTO.Titulo, diretor.Id, inputDTO.Ano);
            await _filmeService.Add(filme);

            var outputDTO = new FilmeOutputPostDTO(filme.Id, filme.Titulo, filme.Ano);
            return Ok(outputDTO);
        }

        /// <summary>
        /// O método Put atualiza o id do filme, titulo e id do diretor no banco de acordo com o id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT/filme/id
        ///     {
        ///        "id": 2,
        ///        "titulo": "O Ultimo dos Moicanos"
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do filme</param>
        /// <param name="inputDTO">Titulo do filme</param>
        /// <returns>O filme atualizado no banco</returns>
        /// <response code="200">Filme atualizado com sucesso</response>

        // PUT api/filmes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<FilmeOutputPutDTO>> Put(int id, [FromBody] FilmeInputPutDTO inputDTO)
        {
            var filme = new Filme(inputDTO.Titulo, inputDTO.DiretorId);
            await _filmeService.Update(filme, inputDTO.DiretorId, id);

            var outputDTO = new FilmeOutputPutDTO(filme.Id, filme.Titulo);
            return Ok(outputDTO);
        }

        /// <summary>
        /// O método Delete remove um filme no banco de acordo com o id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/filme/id
        ///     {
        ///        "id": 2,
        ///        "titulo": "O Ultimo dos Moicanos",
        ///        "ano": null,
        ///        "genero": null,
        ///        "diretorId": 1,
        ///        "diretor": null
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do filme</param>
        /// <returns>O filme excluido</returns>
        /// <response code="200">Filme removido com sucesso</response>

        // DELETE api/filmes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var filme = await _filmeService.Delete(id);
            return Ok(filme);
        }
    }
}