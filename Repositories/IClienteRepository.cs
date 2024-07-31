using teste_loja_back_end.Domain.Entities;

namespace teste_loja_back_end.Repositories
{
    public interface IClienteRepository
    {
        Task<bool> InserirClienteAsync(Cliente cliente);
        Task<bool> AtualizarClienteAsync(Cliente cliente);
        Task<Cliente> BuscarClientePeloEmailAsync(string email);
        Task<Cliente> BuscarClientePeloDocumento(long documento);
        Task<Cliente> BuscarClientePelaInscricaoEstadual(long inscricaoEstadual);
        Task<List<Cliente>> BuscarClientesPaginadoAsync(int numeroPagina, int tamanhoPagina);
    }
}
