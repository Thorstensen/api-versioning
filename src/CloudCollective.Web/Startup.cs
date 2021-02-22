using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CloudCollective.Web
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
            services.AddControllers();
            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ApiVersionReader = ApiVersionReader.Combine
                (
                    new QueryStringApiVersionReader("version"),
                    new HeaderApiVersionReader("version"),
                    new MediaTypeApiVersionReader("version"),
                    new UrlSegmentApiVersionReader()
                ) ;
            });

            services.AddVersionedApiExplorer(cfg =>
            {
                cfg.GroupNameFormat = "'v'VVV";

                //If you use the UrlSegmentApiVersionReader set this to true.
                cfg.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(cfg =>
            {
                var provider = services.BuildServiceProvider();
                var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();
                service.ApiVersionDescriptions.ToList().ForEach(apiVersionDescription =>
                {
                    cfg.SwaggerDoc(apiVersionDescription.GroupName, new OpenApiInfo
                    {
                        Title = "Cloud Collective APIs",
                        Version = apiVersionDescription.ApiVersion.ToString()
                    });
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(cfg =>
                {
                    apiVersionDescriptionProvider.ApiVersionDescriptions.ToList().ForEach(description =>
                    {
                        cfg.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                    });
                });
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
