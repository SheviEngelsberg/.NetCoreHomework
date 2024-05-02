using Microsoft.OpenApi.Models;
using myTask.Services;
using myTask.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace core_l1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.TokenValidationParameters = TokenService.GetTokenValidationParameters();
            });
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Admin", policy => policy.RequireClaim("Type", "Admin"));
                cfg.AddPolicy("User", policy => policy.RequireClaim("Type","User"));
               
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "our tasks", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                    },
                        new string[] {}
                    }
                });
            });
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddTask();
            services.AddUser();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMyMiddleExtensions("file.log");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "core_l1 v1"));
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
