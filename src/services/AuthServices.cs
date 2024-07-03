using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dataContext;
using entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace services;

// Serviço de autenticação que fornece métodos para gerar tokens JWT e obter usuários por e-mail
public class AuthService
{
    // Método para gerar um token de acesso JWT
    public JwtSecurityToken GererateAccessToken(IEnumerable<Claim> claims, IConfiguration _configuration)
    {
        // Cria a chave secreta usando a chave definida nas configurações
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));

        // Cria o token JWT
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidAudience"], 
            audience: _configuration["JWT:ValidIssuer"],
            claims: claims, 
            notBefore: DateTime.Now, 
            expires: DateTime.Now.AddHours(double.Parse(_configuration["JWT:ExpireHours"]!)), 
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        );

        return token; // Retorna o token gerado
    }

    // Método assíncrono para obter um usuário pelo e-mail
    public async Task<UserEntity?> GetUserByEmailAsync(DataContext _context, string Email)
    {
        // Realiza uma consulta no banco de dados para encontrar o primeiro usuário com o e-mail fornecido
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
    }
}
