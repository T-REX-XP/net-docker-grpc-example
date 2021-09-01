using GrpcService1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using AutoMapper;
using GrpcService1.Profiles;
using GrpcService1.Repositories;

namespace GrpcService1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {    // requires using Microsoft.Extensions.Options
            services.Configure<MoviesDatabaseSettings>(
                Configuration.GetSection(nameof(MoviesDatabaseSettings)));

            services.AddSingleton<IMoviesDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MoviesDatabaseSettings>>().Value);
            services.AddSingleton<Movie>();
            services.AddSingleton<MovieRepository>();
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MovieProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(Startup));
            services.AddGrpc();
            services.AddGrpcHttpApi();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddGrpcSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

            }

            app.UseRouting();

            app.UseEndpoints((System.Action<Microsoft.AspNetCore.Routing.IEndpointRouteBuilder>)(endpoints =>
            {
                //GrpcEndpointRouteBuilderExtensions.MapGrpcService<MovieServiceGRPC>(endpoints);
                //GrpcEndpointRouteBuilderExtensions.MapGrpcService<Greeter>(endpoints);
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapGrpcService<MovieGRPCService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            }));
        }
    }
}
