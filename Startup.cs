using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using System.Reflection;

namespace SmartSchool.WebAPI;
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
        services.AddDbContext<DataContext>(
            context => context.UseSqlite(Configuration.GetConnectionString("Default"))
        );

        services.AddControllers()
                .AddNewtonsoftJson(
                    opt => opt.SerializerSettings.ReferenceLoopHandling = 
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies( ));
        services.AddScoped<IRepository, Repository>();

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        })
        .AddApiVersioning(options => 
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        var apiProvider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

        services.AddSwaggerGen(options =>
        {

            foreach (var description in apiProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                description.GroupName,
                new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "SmartSchool API",
                    Version = description.ApiVersion.ToString()
                });
            }

            var comentariosXml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var comentariosXmlPath = Path.Combine(AppContext.BaseDirectory, comentariosXml);
            options.IncludeXmlComments(comentariosXmlPath);
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, 
                          IWebHostEnvironment env,
                          IApiVersionDescriptionProvider apiVersion
                         )
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // app.UseHttpsRedirection();

        app.UseRouting();

        app.UseSwagger()
           .UseSwaggerUI(options =>
           {
               foreach (var description in apiVersion.ApiVersionDescriptions)
               {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());   
               }
               options.RoutePrefix = "";
           });

        // app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
