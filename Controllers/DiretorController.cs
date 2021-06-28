using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net5_Api.Controllers.Model;
using Net5_Api.DTOs.Diretor;

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
        public async Task<List<DiretorOutputGetAlllDTO>> Get()
        {
            var diretores = await _context.Diretores.ToListAsync();
            var outputDTOList = new List<DiretorOutputGetAlllDTO>();

            foreach(Diretor diretor in diretores)
            {
                outputDTOList.Add(new DiretorOutputGetAlllDTO(diretor.Id,diretor.Nome));
            }

            return outputDTOList;
        }

        // GET api/diretores/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
            var outputDTO = new DiretorOutputGetByIdDTO(diretor.Id,diretor.Nome);
            return Ok(outputDTO);
        }

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

        // PUT api/diretores/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<DiretorOutPutPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDTO)
        {
            var diretor  = new Diretor(diretorInputPutDTO.Nome);
            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();
            var diretorOutPutDTO = new DiretorOutPutPutDTO(diretor.Id, diretor.Nome);
            return Ok(diretorOutPutDTO);
        }

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