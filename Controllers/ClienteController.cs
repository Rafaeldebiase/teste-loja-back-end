using Microsoft.AspNetCore.Mvc;
using teste_loja_back_end.Domain.Dto;
using teste_loja_back_end.Domain.Entities;
using teste_loja_back_end.Repositories;
using teste_loja_back_end.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace teste_loja_back_end.Controllers
{
    [Route("/api/v1/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;
        private readonly IClienteRepository _repository;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IClienteService service, IClienteRepository repository, ILogger<ClienteController> logger)
        {
            _service = service;
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("buscarClientes/{numeroPagina}&{tamanhoPagina}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Cliente>>> BuscarClientesPaginados(int numeroPagina, int tamanhoPagina)
        {
            try
            {
                var clientes = await _repository.BuscarClientesPaginadoAsync(numeroPagina, tamanhoPagina);
                
                return Ok(clientes);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
                throw;
            }
        }

        [HttpGet("verificarSeClienteEstaBloqueado/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> VerificarSeClienteEstaBloqueado(string email)
        {
            try
            {
                var cliente = await _repository.BuscarClientePeloEmailAsync(email);
                if(cliente is not null)
                {
                    return Ok(cliente.Bloqueado);
                }
                else
                {
                    return BadRequest("Cliente não está cadastrado");
                }
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                return BadRequest(erro.Message);
            }

        }

        [HttpGet("verificarEmail/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> VericarSeEmailEstaCadastrado(string email)
        {
            try
            {
                var cliente = await _repository.BuscarClientePeloEmailAsync(email);
                var resultado = cliente is not null;
                return Ok(resultado);
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                return BadRequest(erro.Message);
            }
            
        }

        [HttpGet("verificarCpfCnpj/{cpfCnpj}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> VerificarSeCpfCnpjEstaCadastrado(string cpfCnpj)
        {
            try
            {
                var documentoStr = new string(cpfCnpj?.Where(char.IsDigit).ToArray());
                var documento = long.Parse(documentoStr);

                var cliente = await _repository.BuscarClientePeloDocumento(documento);

                var resultado = cliente is not null;

                return Ok(resultado);
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                return BadRequest(erro.Message);
            }

        }

        [HttpGet("verificarInscricaoEstadual/{inscricaoEstadual}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> VerificarSeInscricaoEstadualEstaCadastrado(string inscricaoEstadual)
        {
            try
            {
                var inscricaoEstadualStr = new string(inscricaoEstadual?.Where(char.IsDigit).ToArray());
                var documento = long.Parse(inscricaoEstadualStr);

                var cliente = await _repository.BuscarClientePelaInscricaoEstadual(documento);

                var resultado = cliente is not null;

                return Ok(resultado);
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                return BadRequest(erro.Message);
            }

        }

        [HttpPost("inserir")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CriarCliente([FromBody] ClienteDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Os dados do cliente não foram enviados");

                if (!dto.EhValido())
                    return BadRequest("Verificar os dados informados");

                var clienteCadastrado = await _repository.BuscarClientePeloEmailAsync(dto.Email);

                if (clienteCadastrado is not null)
                    return BadRequest("Email já cadastrado");

                await _service.CriarClienteAsync(dto);

                return Ok();
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                return BadRequest(erro);
            }
        }

        [HttpPut("editar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditarCliente([FromBody] ClienteDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Os dados do cliente não foram enviados");

                if (!dto.EhValido())
                    return BadRequest("Verificar os dados informados");

                await _service.EditarClienteAsync(dto);

                return Ok();
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                return BadRequest(erro);
            }
        }

        [HttpPut("bloquear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> BloquearCliente([FromBody]BloquearDto dto)
        {
            try
            {
                if (dto is null)
                    return BadRequest("Não veio o email");

                var resposta = await _service.BloquearClienteAsync(dto.Email);

                return Ok(resposta);
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                return BadRequest(erro);
            }
        }

    }
}
