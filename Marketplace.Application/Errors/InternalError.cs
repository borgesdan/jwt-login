namespace Marketplace.Application.Errors
{
    public class InternalError
    {
        public static ErrorValue ConfigurationAccessJwtAccess 
            => new("internal_error_000", "Não foi possível acessar ou são inválidas as configurações de token JWT");

        public static ErrorValue UserCreateTransaction
            => new("internal_error_001", "Não foi possível concluir a transação de criação do usuário.");
    }
}