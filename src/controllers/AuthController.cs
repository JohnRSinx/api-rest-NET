using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using dataContext;
using dto;
using entities;
using exceptionError;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using services;

namespace controller;

// Define o controlador de autenticação com atributos para ser um controlador de API e definir a rota base
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // Declaração de dependências necessárias para o controlador
    private readonly AuthService _authService;
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;
    private readonly PasswordService _passwordService;

    // Construtor que injeta as dependências
    public AuthController(AuthService authService, DataContext context, IConfiguration configuration, PasswordService passwordService)
    {
        _authService = authService;
        _context = context;
        _configuration = configuration;
        _passwordService = passwordService;
    }

    // Método para registrar um novo usuário
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterDTO model)
    {
        // Verifica se o e-mail já está registrado no banco de dados
        var userByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        // Se o usuário já existir, retorna um conflito
        if (userByEmail != null)
            return Conflict(new ResponseDTO { Status = "Error", Message = "User already exists!" });

        try
        {
            // Cria um hash de senha seguro
            var password = _passwordService.CreatePassword(model.Password);

            // Cria uma nova entidade de usuário
            UserEntity user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                Password = password,
                DateUpgrade = DateTime.Now,
                DateCreate = DateTime.Now,
            };

            // Adiciona o novo usuário ao banco de dados e salva as alterações
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Retorna uma resposta de criação bem-sucedida
            return Created("/Register", new ResponseDTO { Status = "Success", Message = "User created successfully" });
        }
        catch (Exception ex)
        {
            // Em caso de exceção, retorna um erro interno do servidor
            return this.InternalServerError(ex);
        }
    }

    // Método para login de usuário
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO model)
    {
        // Obtém o usuário pelo e-mail
        var userByEmail = await _authService.GetUserByEmailAsync(_context, model.Email);
        if (userByEmail == null)
            return BadRequest(new ResponseDTO { Status = "Error", Message = "Invalid email or password!" });

        // Verifica se a senha está correta
        var verifyPassword = _passwordService.VerifyPassword(userByEmail.Password!, model.Password);
        if (verifyPassword == false)
            return BadRequest(new ResponseDTO { Status = "Error", Message = "Invalid email or password!" });

        if (userByEmail != null && verifyPassword == true)
        {
            // Cria as claims de autenticação
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userByEmail.Email!),
                new Claim(ClaimTypes.NameIdentifier, userByEmail.Id!),
            };

            // Gera o token JWT com as claims
            var token = _authService.GererateAccessToken(authClaims, _configuration);

            // Retorna o token JWT e a data de expiração
            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }
        else
        {
            // Retorna uma resposta de erro com mensagem de e-mail ou senha incorretos
            return BadRequest(new ResponseDTO { Status = "Success", Message = "Incorrect email or password" });
        }
    }
}
