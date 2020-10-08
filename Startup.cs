using KnowledgeApi.Models;
using KnowledgeApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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

            services.AddCors();
            services.AddMvc();
            services.AddCors(c =>
                {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());

                });

            string mongoConnectionString = this.Configuration.GetConnectionString("MongoConnectionString");
            services.AddTransient(s => new ArticleRepository(mongoConnectionString, "knowledgedb", "articles"));
            services.AddTransient(s => new ArtTypeRepository(mongoConnectionString, "knowledgedb", "arttypes"));

            //services.AddTransient(s => new ArticleCustomService(mongoConnectionString, "knowledgedb", "articles"));
            services.Configure<ConnectionStrings>(Configuration.GetSection(nameof(ConnectionStrings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ConnectionStrings>>().Value);
            services.AddSingleton<ArticleCustomService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(bldr => bldr
           .WithOrigins("http://okipu.net","http://localhost:8080")
           //.AllowAnyOrigin()
           .WithMethods("GET", "POST", "PUT", "DELETE")
           .AllowAnyHeader()
           //.AllowAnyMethod()
           );


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
