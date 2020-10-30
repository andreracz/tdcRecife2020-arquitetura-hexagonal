using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using hexagonal_ddd.Core.Ports;
using hexagonal_ddd.Core.Domain;
using hexagonal_ddd.Adapter.Controllers.VO;

namespace hexagonal_ddd.Adapter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {

		private IContaCorrenteService service;

        public ContaCorrenteController(IContaCorrenteService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<ContaCorrente> List()
        {
            return service.TodasContas();
        }

		[HttpPost]
        public ContaCorrente CriarConta([FromBody]Guid idCliente)
        {
            return service.CriarConta(idCliente);
        }

		[HttpPost]
		[Route("{idConta}/depositar")]
        public void Depositar([FromRoute]Guid idConta, [FromBody] ValorDescricao valorDescricao)
        {
            service.Depositar(idConta, valorDescricao.Valor, valorDescricao.Descricao);
        }
    }
}
