#region (c) Kamuoliai 2022

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using opp_library;
using opp_server.Hubs;

namespace opp_server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(opt => new GameState());
            services.AddSingleton(opt => new BallMovement());
            services.AddSingleton(opt => new Field());
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapHub<GameHub>("/gamehub"); });
        }
    }
}