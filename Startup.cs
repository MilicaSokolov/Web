using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;

namespace WEB_Projekat
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
             services.AddDbContext<VodeniSportoviContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("VodeniSportoviCS"));
            });

     
            services.AddCors(options=>
            {
                    options.AddPolicy("CORS",builder=>
                    {
                        builder.WithOrigins(new string[]
                        {
                            "http://localhost:8080",
                            "https://localhost:8080",
                            "http://127.0.0.1:8080",
                            "https://127.0.0.1:8080",
                            "http://localhost:5500",
                            "https://localhost:5500",
                            "http://127.0.0.1:5500",
                            "https://127.0.0.1:5500",

                            //"http://192.168.0.113:5001",
                           //"https://192.168.0.113:5001",
                            "https://localhost:5000",
                            "http://localhost:5000",
                           "https://localhost:5001",
                            "http://localhost:5001",

                           // "http://192.168.0.113:5502",
                            //"https://192.168.0.113:5502",
                            //"https://localhost:5502",
                            //"http://localhost:5502",
                          
                            //"http://192.168.0.113:5000",
                            //"https://192.168.0.113:5000",
                            //"https://localhost:5000",
                            //"http://localhost:5000"
                            "http://192.168.0.93:5502",
                            "https://192.168.0.93:5502",
                            "http://192.168.0.38:5502",
                            "https://192.168.0.38:5502"

                        }).AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WEB_Projekat", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEB_Projekat v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseCors("CORS"); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
