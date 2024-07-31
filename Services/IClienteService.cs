using teste_loja_back_end.Domain.Dto;

namespace teste_loja_back_end.Services
{
    public interface IClienteService
    {
        Task<bool> CriarClienteAsync(ClienteDto dto);
        Task<bool> EditarClienteAsync(ClienteDto dto);
        Task<bool> BloquearClienteAsync(string email);

    }
}
