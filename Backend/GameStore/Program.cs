using GameStore.Interfaces.IRepository;
using GameStore.Interfaces.IServices;
using GameStore.Models;
using GameStore.Repositories;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;

namespace GameStore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IGameRepository, GameRepository>();
        builder.Services.AddScoped<IGameService, GameService>();

        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        builder.Services.AddScoped<IGameKeyRepository, GameKeyRepository>();
        builder.Services.AddScoped<IGameKeyService, GameKeyService>();

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();

        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseMiddleware<LoggingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors(x =>
        {
            x.WithHeaders().AllowAnyHeader();
            x.WithOrigins("http://localhost:3000");
            x.WithMethods().AllowAnyMethod();
        });

        app.Run();
    }
}
