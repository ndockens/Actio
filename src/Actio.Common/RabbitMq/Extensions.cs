using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Instantiation;
using Actio.Common.Commands;
using Actio.Common.Events;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            return bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cgf => cgf.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));
        }
        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            return bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cgf => cgf.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));
        }

        private static string GetQueueName<T>()
        {
            return $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
        }

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("rabbitmq");
            var options = new RabbitMqOptions();
            configSection.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });

            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}