using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using MultiValueDictionaryProvider;
using MultiValueDictionaryContract;

namespace MultiValueDictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = CreateHostBuilder(args).Build().Services.GetRequiredService<MultiValueDictionaryApplication>();

            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                
                if (input.ToUpperInvariant() == "QUIT")  
                    break;
                    app.CallMethod(input);
                //app.ParseResponse(res);

            }
           // Stop();

        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                    .ConfigureServices(services =>
                    {
                        services.AddSingleton<IPrinter, Printer>();
                        services.AddTransient<IMultiValueDictionaryProvider<string, string>, MultiValueDictionaryProvider<string, string>>();
                        services.AddSingleton<MultiValueDictionaryApplication>();
                    });
        }

    }
}
