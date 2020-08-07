using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SqlParquetImported.Infra;
using SqlParquetImported.Infra.Providers;
using SqlParquetImported.Infra.Repositories;
using SqlParquetImported.Infra.Service;
using SqlParquetImporter.Database;
using SqlParquetImporter.Domain;
using SqlParquetImporter.Domain.Interfaces.Providers;
using SqlParquetImporter.Domain.Interfaces.Repositories;
using SqlParquetImporter.Domain.Interfaces.Services;
using SqlParquetImporter.Domain.Services;

namespace SqlParquetImporter.ImporterApi
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
            services.AddControllers();

            #region "Composition Root"
            
            services.AddDbContext<PriceForecastContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PriceForecastConnection")));
            
            services.AddScoped<IPriceForecastRepository, PriceForecastRepository>();
            services.AddScoped<IImportEventRepository, ImportEventRepository>();
            services.AddScoped<IParquetWriterService, ParquetWriterService>();
            services.AddScoped<IForecastImportService, ForecastImportService>();
            services.AddSingleton<IConfigProvider, ConfigProvider>();

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();//Not needed

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
