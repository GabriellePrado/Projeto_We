using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using We._Project.ConnectionFactory;
using We._Project.ConnectionFactory.Interface;
using We._Project.ConnectionFactory.UnitOfWork;
using We._Project.ConnectionFactory.UnitOfWork.Interface;
using We._Project.Repository;
using We._Project.Repository.Interface;
using We._Project.Service;
using We._Project.Service.Interface;

namespace We._Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string DefaultCorsPolicy = "origin_allow_all";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "We._Project", Version = "v1" });
            });
            services.AddApplicationInsightsTelemetry();

            string connectionString = Configuration.GetConnectionString("default");
            services.AddScoped<IDBConnector>(db => new DBConnector(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();



            services.AddCors(option => option.AddPolicy(name: DefaultCorsPolicy, builder =>
           {
               builder.WithOrigins("*");
           }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var enUs = new CultureInfo("en-US");
            var OpcoesLocalizacao = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUs),
                SupportedCultures = new List<CultureInfo> { enUs },
                SupportedUICultures = new List<CultureInfo> { enUs }
            };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "We._Project v1"));
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(DefaultCorsPolicy);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //configurçao cors


        }
    }
}
