namespace teste_loja_back_end.Domain.Dto
{
    public class ClienteDto
    {
        public ClienteDto(string? nomeRazaoSocial, string? email, string? telefone, string? tipoPessoa, string? documento, bool isentoInscricaoEstadual, string? inscricaoEstadual, string? genero, DateTime? dataNascimento, bool bloqueado, string? senha, bool inscricaoEstadualPessoaFisica)
        {
            NomeRazaoSocial = nomeRazaoSocial;
            Email = email;
            Telefone = telefone;
            TipoPessoa = tipoPessoa;
            Documento = documento;
            IsentoInscricaoEstadual = isentoInscricaoEstadual;
            InscricaoEstadual = inscricaoEstadual;
            Genero = genero;
            DataNascimento = dataNascimento;
            Bloqueado = bloqueado;
            Senha = senha;
            InscricaoEstadualPessoaFisica = inscricaoEstadualPessoaFisica;
        }

        public string? NomeRazaoSocial { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? TipoPessoa { get; set; }
        public string? Documento { get; set; }
        public bool IsentoInscricaoEstadual { get; set; }
        public bool InscricaoEstadualPessoaFisica { get; set; }
        public string? InscricaoEstadual { get; set; }
        public string? Genero { get; set; }
        public DateTime? DataNascimento { get; set; }
        public bool Bloqueado { get; set; }
        public string? Senha { get; set; }

        public bool EhValido() => ValidarNomeRazaoSocial() &&
            ValidarEmail() &&
            ValidarTelefone() &&
            ValidarTipoPessoa() &&
            ValidarDocumento() &&
            ValidarInscricaoEstadual() &&
            ValidarGenero() &&
            ValidarSenha() &&
            ValidarDataNascimento();

        public bool ValidarNomeRazaoSocial() => string.IsNullOrWhiteSpace(NomeRazaoSocial) ? false : true && NomeRazaoSocial.Length <= 150;
 

        public bool ValidarEmail()
        {
            if (string.IsNullOrWhiteSpace(Email) || Email.Length > 150)
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(Email);
                return addr.Address == Email;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidarTelefone() => string.IsNullOrWhiteSpace(Telefone) ? false : true && Telefone.Length <= 11;

        public bool ValidarTipoPessoa()
        {
            if(string.IsNullOrWhiteSpace(TipoPessoa))
                return false;

            return TipoPessoa.ToUpper().Equals("FISICA") || TipoPessoa.ToUpper().Equals("JURIDICA");
        }

        public bool ValidarDocumento()
        {
            if (string.IsNullOrWhiteSpace(Documento) || !Documento.All(char.IsDigit))
                return false;

            if (TipoPessoa?.ToUpper() == "FISICA" && Documento.Length == 11)
                return true;

            if (TipoPessoa?.ToUpper() == "JURIDICA" && Documento.Length == 14)
                return true;

      
            return false;
        }

        public bool ValidarInscricaoEstadual()
        {
            if (IsentoInscricaoEstadual)
            {
                if (string.IsNullOrWhiteSpace(InscricaoEstadual))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            var numerosInscricaoEstadual = new string(InscricaoEstadual?.Where(char.IsDigit).ToArray());

            switch (TipoPessoa?.ToUpper())
            {
                case  "JURIDICA":
                    if (string.IsNullOrWhiteSpace(InscricaoEstadual))
                    {
                        return false;
                    }

                    if (numerosInscricaoEstadual.Length > 12)
                    {
                        return false;
                    }

                    return true;
                case "FISICA":
                    if(InscricaoEstadualPessoaFisica)
                    {
                        if (string.IsNullOrWhiteSpace(InscricaoEstadual))
                        {
                            return false;
                        }
                    }

                    if (numerosInscricaoEstadual.Length > 12)
                    {
                        return false;
                    }

                    return true;

                default:
                    return false;
            }
        }

        public bool ValidarGenero()
        {
            if(TipoPessoa?.ToUpper() == "JURIDICA")
                return false;

            var generosValidos = new[] { "FEMININO", "MASCULINO", "OUTRO" };
            return !string.IsNullOrWhiteSpace(Genero) && generosValidos.Contains(Genero.ToUpper());
        }

        public bool ValidarDataNascimento()
        {
            if (TipoPessoa?.ToUpper() == "FISICA")
            {
                return DataNascimento.HasValue;
            }
            return false;
        }

        public bool ValidarSenha() => 
            !string.IsNullOrWhiteSpace(Senha) && Senha.Length >= 8 && Senha.Length <= 15 && Senha.All(char.IsLetterOrDigit);




    }
}
