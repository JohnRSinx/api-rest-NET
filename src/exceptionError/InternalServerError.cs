using dto;
using Microsoft.AspNetCore.Mvc;

namespace exceptionError;

// Classe que contém um método de extensão para tratar exceções de erro interno do servidor
public static class InternalServerErrorException
{
    // Método de extensão para ControllerBase que gera uma resposta de erro interno do servidor
    public static ActionResult InternalServerError(this ControllerBase controller, Exception ex)
    {
        // Obtém o serviço de logger do contexto da requisição e registra o erro
        controller.HttpContext.RequestServices.GetService<ILogger<ControllerBase>>()?.LogError(ex, "Internal server error");

        // Retorna uma resposta de status 500 com um objeto ResponseDTO contendo a mensagem de erro
        return controller.StatusCode(500, new ResponseDTO { Status = "Error", Message = $"Internal server error: {ex.Message}" });
    }
}
