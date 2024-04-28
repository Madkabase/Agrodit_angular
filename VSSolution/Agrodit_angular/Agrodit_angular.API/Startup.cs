using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Agrodit_angular.DataAccess.Impl;
using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Manager.Impl;
using Agrodit_angular.Manager.Interface;
using Agrodit_angular.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Agrodit_angular.API
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
			 services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                   builder =>
                   {
                       builder.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
                   });
            });
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
           services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.ContractResolver = null; //Turn off camel case
               // options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); //Turn on camel case
            });
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IConfiguration>(Configuration);
            string connectionString = Configuration.GetConnectionString("MSSQLDatabase");
             services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddTransient(_ => new MSSqlDatabase(connectionString));
			
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddTransient<IUploadManager, UploadManager>();

			#region Dependency
            services.AddTransient<IDevicedataDataAccess, DevicedataDataAccess>();
services.AddTransient<IDevicedataManager, DevicedataManager>();
services.AddTransient<IDevicesDataAccess, DevicesDataAccess>();
services.AddTransient<IDevicesManager, DevicesManager>();
services.AddTransient<IGlobalthresholdpresetsDataAccess, GlobalthresholdpresetsDataAccess>();
services.AddTransient<IGlobalthresholdpresetsManager, GlobalthresholdpresetsManager>();
services.AddTransient<IThresholdsDataAccess, ThresholdsDataAccess>();
services.AddTransient<IThresholdsManager, ThresholdsManager>();
services.AddTransient<IUsersDataAccess, UsersDataAccess>();
services.AddTransient<IUsersManager, UsersManager>();
services.AddTransient<ICompaniesDataAccess, CompaniesDataAccess>();
services.AddTransient<ICompaniesManager, CompaniesManager>();
services.AddTransient<IThresholdpresetsDataAccess, ThresholdpresetsDataAccess>();
services.AddTransient<IThresholdpresetsManager, ThresholdpresetsManager>();
services.AddTransient<IAlertsDataAccess, AlertsDataAccess>();
services.AddTransient<IAlertsManager, AlertsManager>();
services.AddTransient<ICompanyusersDataAccess, CompanyusersDataAccess>();
services.AddTransient<ICompanyusersManager, CompanyusersManager>();
services.AddTransient<ISpatial_Ref_SysDataAccess, Spatial_Ref_SysDataAccess>();
services.AddTransient<ISpatial_Ref_SysManager, Spatial_Ref_SysManager>();
services.AddTransient<IFieldsDataAccess, FieldsDataAccess>();
services.AddTransient<IFieldsManager, FieldsManager>();
			#endregion
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agrodit_angular", Version = "v1" });

                // Add a security definition
                c.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Input a valid token to access this API"
                });

                // Add a security requirement
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiBearerAuth"
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {	app.UseCors("AllowAllHeaders");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

			
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}

