using Service.Core.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class StartUp
    {
        public static void Start(IServiceCollection services)
        {
            services.AddSingleton<ICurrencyConverter, CurrencyConverter>();
        }
    }
}
