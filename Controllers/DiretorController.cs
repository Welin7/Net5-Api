using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net5_Api.Controllers.Model;
using Net5_Api.DTOs.Diretor;
using System.Linq;
using System;

namespace Net5_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiretorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiretorController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// O método Get retorna uma lista de todos os diretores do banco.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/diretor
        ///     {
        ///        "id": 1,
        ///        "nome": "James Cameron"
        ///     },
        ///     {
        ///        "id": 2,
        ///        "nome": "Benito Deltoro"
        ///     } 
        ///       
        /// </remarks>
        /// <returns>Todos os diretores já cadastrados no banco</returns>
        /// <response code="200">Diretores listados com sucesso</response>

        // GET api/diretores
        [HttpGet]
        public async Task<ActionResult<List<DiretorOutputGetAlllDTO>>> Get()
        {

            var diretores = await _context.Diretores.ToListAsync();
            var outputDTOList = new List<DiretorOutputGetAlllDTO>();

            foreach (Diretor diretor in diretores)
            {
                outputDTOList.Add(new DiretorOutputGetAlllDTO(diretor.Id, diretor.Nome));
            }

            if (!outputDTOList.Any())
            {
                return NotFound("Não existem diretores cadastrados!");
            }

            return outputDTOList;

        }

        /// <summary>
        /// O método Get retorna um registro do diretor de acordo com o parâmetro id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/diretor/id
        ///     {
        ///        "id": 2,
        ///        "nome": "Benito Deltoro"
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do diretor</param>
        /// <returns>Registro do diretor informado como parâmetro</returns>
        /// <response code="200">Diretor localizado sucesso</response>

        // GET api/diretores/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id)
        {

            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

            if (diretor == null)
            {
                return NotFound("Não existem diretores cadastrados!");
            }

            var outputDTO = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome);
            return Ok(outputDTO);

        }

        /// <summary>
        /// O método Post registra um diretor no banco de acordo com o nome informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/diretor
        ///     {
        ///        "nome": "Tim Burton",
        ///     } 
        ///       
        /// </remarks>
        /// <param name="diretorInputPostDto">Nome do diretor</param>
        /// <returns>O diretor cadastrado no banco</returns>
        /// <response code="200">Diretor criado com sucesso</response>
        /// <response code="500">Erro interno inesperado</response>
        
        // POST api/diretores
        [HttpPost]
        public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDto)
        {

            var diretor = new Diretor(diretorInputPostDto.Nome);
            _context.Diretores.Add(diretor);

            await _context.SaveChangesAsync();

            var diretorOutputDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
            return Ok(diretorOutputDto);

        }

        /// <summary>
        /// O método Put atualiza o nome do diretor no banco de acordo com o id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT/diretor/id
        ///     {
        ///        "id": 5,
        ///        "nome": "Bernardo Bertolucci Atualizado"
        ///     } 
        ///       
        /// </remarks>
        /// <param name="diretorInputPutDTO">Nome do diretor</param>
        /// <param name="id">Id do diretor</param>
        /// <returns>O diretor atualizado no banco</returns>
        /// <response code="200">Diretor atualizado com sucesso</response>

        // PUT api/diretores/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<DiretorOutPutPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDTO)
        {

            var diretor = new Diretor(diretorInputPutDTO.Nome);
            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();
            var diretorOutPutDTO = new DiretorOutPutPutDTO(diretor.Id, diretor.Nome);
            return Ok(diretorOutPutDTO);

        }

        /// <summary>
        /// O método Delete remove um diretor no banco de acordo com o id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/diretor/id
        ///     {
        ///        "id": 7,
        ///        "nome": "Michael Mann",
        ///        "filmes": []
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do diretor</param>
        /// <returns>O diretor excluido</returns>
        /// <response code="200">Diretor removido com sucesso</response>

        // DELETE api/diretores/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {

            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
            _context.Remove(diretor);
            await _context.SaveChangesAsync();
            return Ok(diretor);

        }
    }
}