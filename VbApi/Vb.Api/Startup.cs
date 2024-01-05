
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Business.Cqrs;
using Vb.Business.Mapper;
using Vb.Business.Validator;
using Vb.Business.Command;
using Vb.Business.Query;
using MediatR;

namespace VbApi;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string connection = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<VbDbContext>(options => options.UseSqlServer(connection));
        //services.AddDbContext<VbDbContext>(options => options.UseNpgsql(connection));
        //services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateAddressCommand).GetTypeInfo().Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommand).GetTypeInfo().Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateAccountTransactionCommand).GetTypeInfo().Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateEftTransactionCommand).GetTypeInfo().Assembly);
            cfg.RegisterServicesFromAssembly(typeof(CreateContactCommand).GetTypeInfo().Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetAddressByParameterQuery).GetTypeInfo().Assembly);
        });


        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
        services.AddSingleton(mapperConfig.CreateMapper());


        services.AddControllers().AddFluentValidation(x =>
        {
            x.RegisterValidatorsFromAssemblyContaining<CreateCustomerValidator>();
            x.RegisterValidatorsFromAssemblyContaining<CreateAddressValidator>();
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(x => { x.MapControllers(); });
    }
}
