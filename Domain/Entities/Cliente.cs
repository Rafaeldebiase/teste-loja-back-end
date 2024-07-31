using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using teste_loja_back_end.Domain.Dto;

namespace teste_loja_back_end.Domain.Entities
{
    public record Cliente
    {
        protected Cliente(string? nomeRazaoSocial, string? email, long telefone, string? tipoPessoa, long documento, long? inscricaoEstadual, string? genero, DateTime? dataNascimento, bool bloqueado, string? senha, bool isentoInscricaoEstadual, bool inscricaoEstadualPessoaFisica)
        {
            NomeRazaoSocial = nomeRazaoSocial;
            Email = email;
            Telefone = telefone;
            TipoPessoa = tipoPessoa;
            Documento = documento;
            InscricaoEstadual = inscricaoEstadual;
            Genero = genero;
            DataNascimento = dataNascimento;
            Bloqueado = bloqueado;
            Senha = senha;
            IsentoInscricaoEstadual = isentoInscricaoEstadual;
            InscricaoEstadualPessoaFisica = inscricaoEstadualPessoaFisica;
            DataCadastro = DateTime.UtcNow.Subtract(new TimeSpan(3, 0, 0)).ToString("dd/MM/yyyy");
        }

        [BsonId]
        public ObjectId Id { get; private set; }

        public string? NomeRazaoSocial { get; private set; }
        public string? Email { get; private set; }
        public long Telefone { get; private set; }
        public string? TipoPessoa { get; private set; }
        public long Documento { get; private set; }
        public bool IsentoInscricaoEstadual { get; private set; }
        public bool InscricaoEstadualPessoaFisica { get; private set; }
        public long? InscricaoEstadual { get; private set; }
        public string? Genero { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public bool Bloqueado { get; private set; }
        public string? Senha { get; private set; }
        public string DataCadastro { get; private set; }

        public void Bloquear() => Bloqueado = !Bloqueado;

        public void Atualizar(ClienteDto dto)
        {
            var telefoneStr = new string(dto.Telefone?.Where(char.IsDigit).ToArray());
            var telefone = long.Parse(telefoneStr);

            var documentoStr = new string(dto.Documento?.Where(char.IsDigit).ToArray());
            var documento = long.Parse(documentoStr);

            var inscricaoEstadualStr = new string(dto.InscricaoEstadual?.Where(char.IsDigit).ToArray());
            var inscricaoEstadual = long.Parse(inscricaoEstadualStr);

            NomeRazaoSocial = dto.NomeRazaoSocial;
            Email = dto.Email;
            Telefone = telefone;
            TipoPessoa = dto.TipoPessoa;
            Documento = documento;
            IsentoInscricaoEstadual = dto.IsentoInscricaoEstadual;
            InscricaoEstadualPessoaFisica = dto.InscricaoEstadualPessoaFisica;
            InscricaoEstadual = inscricaoEstadual;
            Genero = dto.Genero;
            DataNascimento = dto.DataNascimento;
            Bloqueado = dto.Bloqueado;
            Senha = dto.Senha;
        }

        public static class ClienteFactory
        {
            public static Cliente CriarCliente(ClienteDto dto)
            {
                var telefoneStr = new string(dto.Telefone?.Where(char.IsDigit).ToArray());
                var telefone = long.Parse(telefoneStr);

                var documentoStr = new string(dto.Documento?.Where(char.IsDigit).ToArray());
                var documento = long.Parse(documentoStr);

                var inscricaoEstadualStr = new string(dto.InscricaoEstadual?.Where(char.IsDigit).ToArray());
                var inscricaoEstadual = long.Parse(inscricaoEstadualStr);

                var cliente = new Cliente(
                        dto.NomeRazaoSocial,
                        dto.Email,
                        telefone,
                        dto.TipoPessoa,
                        documento,
                        inscricaoEstadual,
                        dto.Genero,
                        dto.DataNascimento,
                        dto.Bloqueado,
                        dto.Senha,
                        dto.IsentoInscricaoEstadual,
                        dto.InscricaoEstadualPessoaFisica
                    );

                return cliente;
            }
        }
    }

}
