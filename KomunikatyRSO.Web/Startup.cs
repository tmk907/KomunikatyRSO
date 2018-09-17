using System.Text;
using KomunikatyRSO.Web.Infrastructure.Commands;
using KomunikatyRSO.Web.Infrastructure.EF;
using KomunikatyRSO.Web.Infrastructure.Handlers.Accounts;
using KomunikatyRSO.Web.Infrastructure.Handlers.Notifications;
using KomunikatyRSO.Web.Infrastructure.Services;
using KomunikatyRSO.Web.Infrastructure.Settings;
using KomunikatyRSO.Web.Infrastructure.WNS;
using KomunikatyRSO.Shared.Commands;
using KomunikatyRSO.Shared.Commands.Accounts;
using KomunikatyRSO.Shared.Commands.Notifications;
using KomunikatyRSO.Web.Framework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace KomunikatyRSO.Web
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            JwtSettings jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            WnsSettings wnsSettings = new WnsSettings();
            Configuration.Bind(nameof(WnsSettings), wnsSettings);
            services.AddSingleton(wnsSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                });

            services.AddDbContext<NotificationsDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("SQLiteConnection"));
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddScoped<AccountService>();
            services.AddScoped<NotificationsService>();
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddScoped<NewsService>();
            services.AddScoped<JwtService>();

            services.AddScoped<IPushNotificationSender, WindowsPushNotificationService>();
            services.AddScoped<WNSAuthenticationHelper>();
            services.AddScoped<WnsTokenStorage>();

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            services.AddScoped<ICommandHandler<Register>, RegisterHandler>();
            services.AddScoped<ICommandHandler<CreateToken>, CreateTokenHandler>();
            services.AddScoped<ICommandHandler<UpdatePreferences>, UpdatePreferencesHandler>();
            services.AddScoped<ICommandHandler<UpdatePushChannel>, UpdatePushChannelHandler>();

            //services.AddHostedService<TimedHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseMyExceptionHandler();

            app.UseAuthentication();

            
            app.UseMvc();
        }
    }
}
