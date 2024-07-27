namespace teste_loja_back_end.Domain.Entities
{
    public record struct Cliente(
        string NomeRazaoSocial,
        string Email,
        long Telefone,
        string TipoPessoa,
        long Documento,
        long? InscricaoEstadual,
        string? Genero,
        DateTime? DataNascimento,
        bool Bloqueado,
        string Senha
       );
}
