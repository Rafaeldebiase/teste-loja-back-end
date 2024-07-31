using System.Runtime.InteropServices;
using teste_loja_back_end.Domain.Dto;
using teste_loja_back_end.Domain.Entities;
using teste_loja_back_end.Repositories;

namespace teste_loja_back_end.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IClienteRepository repository, ILogger<ClienteService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> BloquearClienteAsync(string email)
        {
            try
            {
                var cliente = await _repository.BuscarClientePeloEmailAsync(email);
                cliente.Bloquear();
                var resultado = await _repository.AtualizarClienteAsync(cliente);
                return resultado;

            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                throw;
            }
        }

        public async Task<bool> CriarClienteAsync(ClienteDto dto)
        {
            try
            {
                var cliente = Cliente.ClienteFactory.CriarCliente(dto);

                var resultado = await _repository.InserirClienteAsync(cliente);

                return resultado;
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                throw;
            }
        }

        public async Task<bool> EditarClienteAsync(ClienteDto dto)
        {
            try
            {
                var cliente = await _repository.BuscarClientePeloEmailAsync(dto.Email);

                cliente.Atualizar(dto);

                var resultado = await _repository.AtualizarClienteAsync(cliente);
                return resultado;
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                throw;
            }
            
        }
    }
}
