using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using We._Project.ConnectionFactory.UnitOfWork.Interface;
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
        public ColaboradorController(ILogger<ColaboradorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll([FromServices] IColaboradorRepository _colaborador)
        {

            return Ok(_colaborador.GetAll());
        }


        [HttpGet("{cpf}")]
        public IActionResult Get([FromServices] IColaboradorRepository _colaborador, string cpf)
        {
            if (cpf == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            var consulta = _colaborador.GetByCpfAsync(cpf);
            return Ok(consulta);

        }
        //Create
        [HttpPost]

        public IActionResult Create(
            [FromServices] IColaboradorRepository _colaborador,
            [FromServices] IUnitOfWork unitOfWork,
            [Bind("cpf, matricula, nome_completo, data_admissao, departamento_colaborador")] Colaborador colaborador)
        {
            unitOfWork.BeginTransaction();
            if (colaborador == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _colaborador.CreateAsync(colaborador);
            unitOfWork.CommitTransaction();
            return RedirectToAction("GetAll");
        }

        //Update
        [HttpPut]
        public async Task<IActionResult> Put(
            [FromServices] IColaboradorRepository _colaborador,
            [FromBody] Colaborador colaborador)
        {
            if (colaborador == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(await _colaborador.UpdateAsync(colaborador));
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Delete([FromServices] IColaboradorRepository _colaborador, string cpf)
        {
            await _colaborador.DeleteAsync(cpf);
            return RedirectToAction("GetAll");
        }
    }
}
