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

        // GET api/diretores
        [HttpGet]
        public async Task<ActionResult<List<DiretorOutputGetAlllDTO>>> Get()
        {
            try
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
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // GET api/diretores/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id)
        {
            try
            {
                var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

                if (diretor == null)
                {
                    return NotFound("Não existem diretores cadastrados!");
                }

                var outputDTO = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome);
                return Ok(outputDTO);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // POST api/diretores
        [HttpPost]
        public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDto)
        {
            try
            {
                var diretor = new Diretor(diretorInputPostDto.Nome);
                _context.Diretores.Add(diretor);

                await _context.SaveChangesAsync();

                var diretorOutputDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
                return Ok(diretorOutputDto);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // PUT api/diretores/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<DiretorOutPutPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDTO)
        {
            try
            {
                var diretor = new Diretor(diretorInputPutDTO.Nome);
                diretor.Id = id;
                _context.Diretores.Update(diretor);
                await _context.SaveChangesAsync();
                var diretorOutPutDTO = new DiretorOutPutPutDTO(diretor.Id, diretor.Nome);
                return Ok(diretorOutPutDTO);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        // DELETE api/diretores/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
                _context.Remove(diretor);
                await _context.SaveChangesAsync();
                return Ok(diretor);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}