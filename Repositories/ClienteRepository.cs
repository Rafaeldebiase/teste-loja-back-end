using MongoDB.Driver;
using teste_loja_back_end.Domain.Entities;

namespace teste_loja_back_end.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ILogger<ClienteRepository> _logger;
        private readonly IMongoCollection<Cliente> _clienteCollection;

        public ClienteRepository(ILogger<ClienteRepository> logger, IMongoDatabase database)
        {
            _logger = logger;
            _clienteCollection = database.GetCollection<Cliente>("Cliente");
        }

        public async Task<List<Cliente>> BuscarClientesPaginadoAsync(int numeroPagina, int tamanhoPagina)
        {
            var pular = (numeroPagina -1) * tamanhoPagina;

            var clientes = await _clienteCollection.Find(Builders<Cliente>.Filter.Empty)
                .Skip(pular)
                .Limit(tamanhoPagina)
                .ToListAsync();

            return clientes;
        }

        public async Task<Cliente> BuscarClientePeloEmailAsync(string email)
        {
            try
            {
                var filter = Builders<Cliente>.Filter.Eq(c => c.Email, email);
                var result = await _clienteCollection.Find(filter).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                throw;
            }
        }

        public async Task<Cliente> BuscarClientePeloDocumento(long documento)
        {
            try
            {
                var filter = Builders<Cliente>.Filter.Eq(c => c.Documento, documento);
                var result = await _clienteCollection.Find(filter).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                throw;
            }
        }

        public async Task<Cliente> BuscarClientePelaInscricaoEstadual(long inscricaoEstadual)
        {
            try
            {
                var filter = Builders<Cliente>.Filter.Eq(c => c.InscricaoEstadual, inscricaoEstadual);
                var result = await _clienteCollection.Find(filter).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                throw;
            }
        }

        public async Task<bool> AtualizarClienteAsync(Cliente cliente)
        {
            try
            {
                var filter = Builders<Cliente>.Filter.Eq(x => x.Id, cliente.Id);

                var result = await _clienteCollection.ReplaceOneAsync(filter, cliente);
                return result.ModifiedCount != 0;
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                throw;
            }
        }

        public async Task<bool> InserirClienteAsync(Cliente cliente)
        {
            try
            {
                await _clienteCollection.InsertOneAsync(cliente);
                return true;
            }
            catch (Exception erro)
            {
                _logger.LogError(erro.Message);
                return false;
            }
        }
    }
}
