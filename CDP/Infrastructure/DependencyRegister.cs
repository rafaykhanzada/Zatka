using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog.Core;
using Serilog.Events;
using Serilog;
using Core.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Repository.IRepository;
using Repository.Repository;
using Service.IService;
using Service.Service;
using System.Text;

namespace CDP.Infrastructure
{
    public class DependencyRegister
    {
        public DependencyRegister(WebApplicationBuilder applicationBuilder)
        {
            ConfigurationManager configuration = applicationBuilder.Configuration;
            IServiceCollection services = applicationBuilder.Services;
            Config.config = configuration;
            if (applicationBuilder.Environment.IsDevelopment())
                Config.env = Environments.Development;
            else
                Config.env = Environments.Production;
            #region Auto_Mapper
            IMapper mapper = MappingRegister.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion
            #region Swagger_Setting
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Staging API - V1",
                    Version = "v1",
                    License = new OpenApiLicense { Name = "Microsoft ASP.NET Core" },
                    Description = "FINOSYS PRIVATE LIMITED",
                    Contact = new OpenApiContact() { Email = "info@finosys.com", Name = "Finosys" }
                });
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, new[] { "Bearer" } }
                });

            });
            #endregion
            #region ErrorHandling
            services.AddProblemDetails();
            services.AddRouting(options => options.LowercaseUrls = true);
            #endregion
            #region JWT & Authentication
            // These will eventually be moved to a secrets file, but for alpha development appsettings is fine
            var validIssuer = Config.config.GetValue<string>("JwtTokenSettings:ValidIssuer");
            var validAudience = Config.config.GetValue<string>("JwtTokenSettings:ValidAudience");
            var symmetricSecurityKey = Config.config.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.UseSecurityTokenValidators = true; //ADD THIS
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = validIssuer,
                        ValidAudience = validAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(symmetricSecurityKey)
                        ),
                    };
                });

            #endregion
            #region Logs
            var levelSwitch = new LoggingLevelSwitch();
            levelSwitch.MinimumLevel = LogEventLevel.Information;
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Serilog"))
                  .AddSerilog(new LoggerConfiguration().MinimumLevel.ControlledBy(levelSwitch).WriteTo.File($"logs\\logs-{DateTime.Now.ToString("D")}.log").CreateLogger())
                  .AddConsole();
                builder.AddDebug();
            });
            #endregion
            #region Cache Service
            services.AddMemoryCache();
            #endregion
            #region Jobs
            if (false)
            {
                //services.AddHostedService<MyService>();
                //services.AddSingleton<IJobFactory, SignletonJobFactory>();
                //services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                ////services.AddSingleton<TagScheduleService>();
                ////At minute
                //var jobs = _context.ScheduleJob.Where(x => x.IsActive && x.DeletedOn == null && x.CronExpression != null && x.JobName != null);
                //foreach (var item in jobs)
                //{
                //    var type = System.AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).First(x => x.Name == item.JobType);
                //    try
                //    {
                //        services.AddSingleton(new ScheduleJob(type, item.CronExpression, item.JobName));
                //    }
                //    catch (Exception ex)
                //    {
                //        var logFactory = new LoggerFactory();

                //        //var logger = logFactory.CreateLogger<TagScheduleService>();

                //        //logger.LogInformation("this is debug log");
                //    }
                //}
            }
            #endregion
            #region Repository
            services.AddScoped<IBranchRepository, BranchRepository>();
            #endregion
            #region Services
            services.AddScoped<ResultModel>();
            services.AddScoped<TokenValidationParameters>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IAuthService, AuthService>();
            #endregion
        }
    }
}
