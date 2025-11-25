
using CleanArchMvc.Infra.IoC;
using Microsoft.OpenApi.Models;

namespace CleanArchMvc.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructureAPI(builder.Configuration);
        // Ativar autenticação e validar o token
        builder.Services.AddInfrastructureJWT(builder.Configuration);
        // Adicionar configurações do swagger
        builder.Services.AddInfrastructureSwagger();

        // Add services to the container.

        builder.Services.AddControllers().AddNewtonsoftJson(x =>
            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseStatusCodePages();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
