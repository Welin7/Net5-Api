using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net5_Api.Controllers.Model;
using Net5_Api.DTOs.Filme;

namespace Net5_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilmeController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<ActionResult<List<FilmeOutputGetAllDTO>>> Get()
        {

            var filmes = await _context.Filmes.ToListAsync();
            var outputDTOList = new List<FilmeOutputGetAllDTO>();

            foreach (Filme filme in filmes)
            {
                outputDTOList.Add(new FilmeOutputGetAllDTO(filme.Id, filme.Titulo, filme.Ano));
            }

            if (!outputDTOList.Any())
            {
                return NotFound("Não existem filmes cadastrados!");
            }

            return outputDTOList;

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

            var filme = await _context.Filmes.Include(filme => filme.Diretor).FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                throw new ArgumentNullException("Filme não encontrado!");
            }

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

            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == inputDTO.DiretorId);

            if (diretor == null)
            {
                return NotFound("Diretor informado não encontrado!");
            }

            var filme = new Filme(inputDTO.Titulo, diretor.Id);
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();

            var outputDTO = new FilmeOutputPostDTO(filme.Id, filme.Titulo);

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

            if (inputDTO.DiretorId == 0)
            {
                return NotFound("Id informado não existe para atualização!");
            }

            filme.Id = id;
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();

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

            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
            _context.Remove(filme);
            await _context.SaveChangesAsync();
            return Ok(filme);

        }
    }
}