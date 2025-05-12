using System.Net;
using System.Text.Json;

namespace PedidoProcessor.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado no processamento da requisição.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var resposta = new
                {
                    statusCode = context.Response.StatusCode,
                    mensagem = "Erro interno no servidor. Tente novamente mais tarde."
                };

                var json = JsonSerializer.Serialize(resposta);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
