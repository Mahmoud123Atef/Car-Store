using HurghadaStore.APIs.Errors;
using HurghadaStore.APIs.Helper;
using HurghadaStore.Core.Entities;
using HurghadaStore.Core.Entities.Identity;
using HurghadaStore.Core.Repositories.Contract;  
using HurghadaStore.Repository;
using HurghadaStore.Repository.Data;
using HurghadaStore.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace HurghadaStore.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            //builder.Services.AddScoped<IGenericRepository<Car>, GenericRepository<Car>>();
            //builder.Services.AddScoped<IGenericRepository<CarBrand>, GenericRepository<CarBrand>>();
            //builder.Services.AddScoped<IGenericRepository<CarCategory>, GenericRepository<CarCategory>>();

            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection); // Create object of Redis database.
            });

            //builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository)); // new Generic way

            //builder.Services.AddScoped<IGenericRepository, GenericRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));


            // Validation Error
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var error = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                        .SelectMany(P => P.Value.Errors)
                                                        .Select(E => E.ErrorMessage)
                                                        .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.Password.RequiredUniqueChars = 2;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<AppIdentityDbContext>(); 
            #endregion

            var app = builder.Build(); // Kestrel

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            // Ask CLR explicitly to create object from DbContext.
            var _dbContext = service.GetRequiredService<StoreContext>();

            // Ask CLR explicitly to create object from IdentityDbContext.
            var _identityDbContext = service.GetRequiredService<AppIdentityDbContext>();

            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); // Update Database
                await StoreContextSeed.SeedAsync(_dbContext);

                await _identityDbContext.Database.MigrateAsync(); // Update Database
                var _userManager = service.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(_userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error has been occured during apply the migration"); // To log the error on the Kestrel
            }

            #region Configure Kestrel Pipeline

            #region Middleware order
            /*
             * 1) ExceptionHandler
             * 2) HSTS
             * 3) HttpsRedirection
             * 4) Static Files
             * 5) Routing (MapControllers())
             * 6) CORS
             * 7) Authentication
             * 8) Authorization
             * 9) Custom Middlewares (Made your own Middleware and put it in the order you want)
             */
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseStaticFiles(); // wwwroot
            #endregion

            app.Run();
        }
    }
}
