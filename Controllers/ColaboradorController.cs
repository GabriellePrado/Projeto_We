using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using We._Project.Model;
using We._Project.Repository.Interface;
using We._Project.Service.Interface;

namespace We._Project.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradorController : ControllerBase
    {
        private readonly ILogger<ColaboradorController> _logger;
        private readonly IColaboradorRepository _colaboradorRepository;
        public ColaboradorController(ILogger<ColaboradorController> logger, IColaboradorRepository colaboradorRepository)
        {
            _logger = logger;
            _colaboradorRepository = colaboradorRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_colaboradorRepository.GetAll());
        }

        [HttpGet("{cpf}")]
        public  IActionResult Get(string cpf)
        {
            if (cpf == null)
            {
                return null;
            }
            var consulta = _colaboradorRepository.GetByCpfAsync(cpf);
            return Ok(consulta);

        }
        //Create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Colaborador colaborador)
        {
            if (colaborador == null)
            {
                return null;
            }
            return Ok(await _colaboradorRepository.CreateAsync(colaborador));
        }

        //Update
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Colaborador colaborador)
        {
            if (colaborador == null)
            {
                return NotFound();
            }
            return Ok( await _colaboradorRepository.UpdateAsync(colaborador));
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Delete(string cpf)
        {
            await _colaboradorRepository.DeleteAsync(cpf);
            return NoContent();
        }
    }
}
