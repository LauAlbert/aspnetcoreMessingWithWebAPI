using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.Data;
using Contact.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contact
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=ContactDb;Trusted_Connection=True;";
            services.AddDbContext<ContactContext>(o => o.UseSqlServer(connectionString));
            services.AddMvc();



            services.AddScoped<IPeopleContactRepository, PeopleContactRepository>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
            {
                var actionContext =
                implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ContactContext contactContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.PeopleContact, Dtos.PeopleContactPutDto>();
                cfg.CreateMap<Models.PeopleContact, Dtos.PeopleContactDto>()
                    .ForMember(dst => dst.Gender, opts => opts.MapFrom(src => src.Gender == Models.Gender.Male ? "Male" : "Female"));
                cfg.CreateMap<Dtos.PeopleContactPostDto, Models.PeopleContact>();
                cfg.CreateMap<Dtos.PeopleContactPutDto, Models.PeopleContact>();
            });

            contactContext.Initializer();


            app.UseMvc();
        }
    }
}
