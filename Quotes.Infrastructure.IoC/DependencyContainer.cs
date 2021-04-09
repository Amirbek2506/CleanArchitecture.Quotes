using Microsoft.Extensions.DependencyInjection;
using Quotes.Application.Interfaces;
using Quotes.Application.Services;
using Quotes.Domain.Interfaces;
using Quotes.Infra.Data.Repositories;
using System;

namespace Quotes.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IQuoteService, QuoteService>();

            services.AddScoped<IQuoteRepository, QuoteRepository>();
        }
    }
}
