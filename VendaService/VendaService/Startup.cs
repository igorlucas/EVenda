using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VendaService.Data;
using VendaService.Services.AzureServiceBus;
using VendaService.Services.AzureServiceBus.Queues;

namespace VendaService
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VendaServiceContext>();
            services.AddScoped<ServiceBusMessageSender>();
            services.AddScoped<ProdutoVendidoMessageSender>();
            services.AddSingleton<ServiceBusMessageConsumer>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var serviceBusMessageConsumer = app.ApplicationServices.GetService<ServiceBusMessageConsumer>();
            serviceBusMessageConsumer.RegisterAndWaitMessages();
        }
    }
}
