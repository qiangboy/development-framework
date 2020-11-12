using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.RabbitMQ;

namespace BookStore.Application.EventBus
{
    [DependsOn(
        typeof(BookStoreDomainModule),
        typeof(AbpEventBusRabbitMqModule)
    )]
    public class BookStoreApplicationEventBusModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDistributedEntityEventOptions>(opt =>
            {
                opt.AutoEventSelectors.AddAll();
            });

            Configure<AbpRabbitMqOptions>(opt =>
            {
                opt.Connections.Default.UserName = configuration["RabbitMQ:Connections:Default:UserName"];
                opt.Connections.Default.Password = configuration["RabbitMQ:Connections:Default:Password"];
                opt.Connections.Default.HostName = configuration["RabbitMQ:Connections:Default:HostName"];
                opt.Connections.Default.Port = Convert.ToInt32(configuration["RabbitMQ:Connections:Default:Port"]);
            });

            Configure<AbpRabbitMqEventBusOptions>(opt =>
            {
                opt.ClientName = configuration["RabbitMQ:EventBus:ClientName"];
                opt.ExchangeName = configuration["RabbitMQ:EventBus:ExchangeName"];
                //opt.ConnectionName = configuration["RabbitMQ:EventBus:Default:ConnectionName"];
            });

            
        }
    }
}
