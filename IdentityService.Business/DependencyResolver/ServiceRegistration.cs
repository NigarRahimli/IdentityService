using AutoMapper;
using IdentityService.Core.EventBus.RabbitMq;
using IdentityService.Core.EventBus;
using IdentityService.Core.Utilities.MailHelper;
using IdentityService.DataAccess.Abstract;
using IdentityService.DataAccess.Concrete.EntityFramework;
using IdentityService.DataAccess.Concrete;
using Microsoft.Extensions.DependencyInjection;

using IdentityService.Business.AutoMapper;
using MassTransit;
using IdentityService.Business.Abstract;
using IdentityService.Business.Concrete;
using IdentityService.Business.Consumers;
using IdentityService.Entities.SharedModels;

namespace IdentityService.Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddBusinessRegistration(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

           

            

            services.AddScoped<IAppUserDal, EfAppUserDal>();
            services.AddScoped<IUserService, UserManager>();

            services.AddScoped<IMailSender, MailSender>();


            services.AddScoped<IServiceBus, RabbitMqServiceBus>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddMassTransit(config =>
            {
                config.AddConsumer<SendEmailConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("amqps://ultqcncz:6SXk01KqKsY7HYR8kX421TGzCyNgXeGh@toad.rmq.cloudamqp.com/ultqcncz");
                    cfg.Message<SendEmailCommand>(x => x.SetEntityName("SendEmailCommand"));

                    cfg.ReceiveEndpoint("send-email-command", c =>
                    {
                        c.ConfigureConsumer<SendEmailConsumer>(ctx);
                    });
                });
            });





        }
    }
}
