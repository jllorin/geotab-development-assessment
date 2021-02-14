using JokeCreator.Joke;
using JokeCreator.Person;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator
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
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins(Configuration["Cors:Origins"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddHttpClient();


            services.AddTransient(typeof(IJokeRepository<Joke.Joke>), typeof(JokeRepository));
            services.AddTransient(typeof(IJokeService), typeof(JokeService));

            services.AddTransient(typeof(ICategoryRepository<List<string>>), typeof(CategoryRepository));
            services.AddTransient(typeof(ICategoryService), typeof(CategoryService));

            services.AddTransient(typeof(IPersonRepository<Person.Person>), typeof(PersonRepository));
            services.AddTransient(typeof(IPersonService), typeof(PersonService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
