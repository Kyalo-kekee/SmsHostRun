using Microsoft.Extensions.Configuration;
using SmsHostRun;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((hostContext,services) =>
	{


		IConfiguration configuration = hostContext.Configuration;
		services.Configure<SymfonySettings>(configuration.GetSection("SymfonySettings"));
		services.AddHostedService<Worker>();

	})
	.Build();

await host.RunAsync();
