using Microsoft.AspNetCore.Identity;

namespace services;
public class PasswordService
{
    readonly PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
    public string CreatePassword(string password)
    {
        // Criando um hash da senha
        return passwordHasher.HashPassword(null!, password);
    }

    public bool VerifyPassword(string passwordSaved, string password)
    {
        // Verificando a senha
        var result = passwordHasher.VerifyHashedPassword(null!, passwordSaved, password);

        if (result == PasswordVerificationResult.Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal string CreatePassword(object password)
    {
        throw new NotImplementedException();
    }
}