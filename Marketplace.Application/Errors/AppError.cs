namespace Marketplace.Application.Errors
{
    public class AppError
    {
        public static ErrorValue NoDataHasBeenReported
                => new("app_000", "Nenhum dado foi informado.");

        public static ErrorValue InvalidAccessLevel
                => new("app_001", "Nível de acesso inválido.");

        public class User
        {
            public static ErrorValue NameNotInformed
                => new("user_000", "O nome do usuário deve ser informado.");

            public static ErrorValue EmailNotInformed
                => new("user_001", "O campo email deve ser informado.");

            public static ErrorValue PasswordNotInformed
                => new("user_002", "A senha deve ser informada.");

            public static ErrorValue AlreadyExistingEmail
                => new("user_003", "Já existe uma conta cadastrada com esse email.");

            public static ErrorValue InvalidLogin
                => new("user_004", "Dados de login inválido.");

            public static ErrorValue InvalidPermissionToCreateUser
                => new("user_005", "Somente administradores com privilégios podem criar outros administradores.");
        }   
        
        public class Advertisement
        {
            public static ErrorValue TitleNotInformed
                => new("ad_000", "O título deve ser informado.");

            public static ErrorValue DescriptionNotInformed
                => new("ad_001", "A descrição deve ser informada.");

            public static ErrorValue DiscountCannotBeNegative
                => new("ad_002", "O desconto não pode ter um valor negativo");
        }
    }
}
