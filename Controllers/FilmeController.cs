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

        // GET api/filmes
        [HttpGet]
        public async Task<ActionResult<List<FilmeOutputGetAllDTO>>> Get()
        {
            try
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
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // GET api/filmes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id)
        {
            try
            {
                var filme = await _context.Filmes.Include(filme => filme.Diretor).FirstOrDefaultAsync(filme => filme.Id == id);

                if (filme == null)
                {
                    return NotFound("Filme informado não encontrado!");
                }

                var outputDTO = new FilmeOutputGetByIdDTO(filme.Id, filme.Titulo, filme.Diretor.Nome);
                return Ok(outputDTO);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // POST api/filmes
        [HttpPost]
        public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO inputDTO)
        {
            try
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
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // PUT api/filmes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<FilmeOutputPutDTO>> Put(int id, [FromBody] FilmeInputPutDTO inputDTO)
        {
            try
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
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // DELETE api/filmes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
                _context.Remove(filme);
                await _context.SaveChangesAsync();
                return Ok(filme);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}