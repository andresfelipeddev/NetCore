using FluentValidation.AspNetCore;
using MediatR;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VentasMS.Api.Facturas.Application;
using VentasMS.Api.Facturas.Persistency;
using VentasMS.Api.Facturas.RemoteInterfase;
using VentasMS.Api.Facturas.RemoteService;

namespace VentasMS.Api.Facturas
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

            services.AddControllers().AddFluentValidation(
                        cfg => cfg.RegisterValidatorsFromAssemblyContaining<NewInvoice>()); 

            services.AddDbContext<InvoiceContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConexionArticulos"));
            });

            services.AddMediatR(typeof(NewInvoice.Manager).Assembly);
            // services.AddAutoMapper(typeof(ConsultUnidad.Manager));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VentasMS.Api.Facturas", Version = "v1" });
            });

            services.AddHttpClient("Articulos", cfg => {
                cfg.BaseAddress = new Uri(Configuration["Services:Articulos"]);
            });

            //Esta es la inyeccion de dependencias de un servicio
            services.AddScoped<IArticulosService, ArticulosService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "VentasMS.Api.Facturas v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
