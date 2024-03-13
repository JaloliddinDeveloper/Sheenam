//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using Microsoft.OpenApi.Models;
namespace Sheenam.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
           Configuration = configuration;


        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiInfo = new OpenApiInfo
            {
                Title = "Sheenam.Api",
                Version = "v1"
            };

           // services.AddDbContext<StorageBroker>();
            services.AddControllers();
            AddBrokers(services);



            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    name: "v1",
                    info: apiInfo);
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment enveronment)
        {
            if (enveronment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        url: "/swagger/v1/swagger.json",
                        name: "Sheenam.Api v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());
        }
        private static void AddBrokers(IServiceCollection services)
        {
            //services.AddTransient<IStorageBroker, StorageBroker>();
           // services.AddTransient<ILoggingBroker, LoggingBroker>();
        }
    }
}
