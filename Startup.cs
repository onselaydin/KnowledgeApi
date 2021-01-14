using FluentValidation;
using KnowledgeApi.Models;
using KnowledgeApi.Services;
using KnowledgeApi.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KnowledgeApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //JWT Authentication
            IdentityModelEventSource.ShowPII = true;
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.Key); //appSettings.Key
            services.AddAuthentication(au =>
            {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(jwt=>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateActor = false
                    
                   
                };
            });
            
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            //services.AddCors();
            services.AddMvc();
           
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Knowledge Service API",
                    Version = "v1.0",
                    Description = "Knowledge Service API",
                });
            });
            services.AddCors(c => c.AddDefaultPolicy(p=>
                p.AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials()
                  .SetIsOriginAllowed(origin=>true)
            ));

            string mongoConnectionString = this.Configuration.GetConnectionString("MongoConnectionString");
            services.AddTransient(s => new ArticleRepository(mongoConnectionString, "knowledgedb", "articles"));
            services.AddTransient(s => new ArtTypeRepository(mongoConnectionString, "knowledgedb", "arttypes"));
            services.AddTransient(s => new MessageRepository(mongoConnectionString, "knowledgedb", "messages"));
            //services.AddTransient(s => new ArticleCustomService(mongoConnectionString, "knowledgedb", "articles"));
            services.Configure<ConnectionStrings>(Configuration.GetSection(nameof(ConnectionStrings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ConnectionStrings>>().Value);
            services.AddSingleton<ArticleCustomService>();
            services.AddSingleton<IValidator<ArtType>, ArtTypeValidator>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // app.UseCors(bldr => bldr
            //.WithOrigins("http://okipu.net","http://localhost:8080")
            //.AllowAnyOrigin()
            //.WithMethods("GET", "POST", "PUT", "DELETE")
            //.AllowAnyHeader()
            //.AllowAnyMethod()
            //);

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "PlaceInfo Services"));

            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
