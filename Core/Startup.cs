﻿using Core.Extensions;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Service;
using Service.Interface;
using SqlSugar;
using System.Text;

namespace Core
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
      services.AddSingleton<IConnectionDatabase<SqlSugarClient>>(new SqlSugarConnectDBService(Configuration.GetConnectionString("Mariadb")));
      services.AddHangfire(Configuration.GetConnectionString("Mariadb"));
      services.AddJWT(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBackgroundJobClient backgroundJobs)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }
      app.UseAuthentication();
      //  app.UseSerilogRequestLogging(); 
      app.UseHttpsRedirection();
      app.UseHangfireDashboard();
      // backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
      app.UseMvc();
    }
  }
}
