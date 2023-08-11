using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SmsHostRun
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly SymfonySettings _symfonySettings;

		public Worker(ILogger<Worker> logger, IOptions<SymfonySettings> symfonySettings)
		{
			_logger = logger;
			_symfonySettings = symfonySettings.Value;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			if (!IsSymfonyServerRunning())
			{
				_logger.LogInformation("Starting Symfony server...");

				// Create a new process to execute Symfony command
				ProcessStartInfo processInfo = new ProcessStartInfo("symfony", $"{_symfonySettings.StartCommand}")
				{
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true
				};

				using (Process process = new Process { StartInfo = processInfo })
				{
					process.Start();

					// Read and log output and errors
					string output = await process.StandardOutput.ReadToEndAsync();
					string errors = await process.StandardError.ReadToEndAsync();

					process.WaitForExit();

					// Log output and errors
					_logger.LogInformation("Symfony server started. Output: {output}", output);
					_logger.LogError("Errors: {errors}", errors);
				}
			}
		}

		private bool IsSymfonyServerRunning()
		{
			try
			{
				// Try to connect to the Symfony server
				using (var client = new WebClient())
				{
					string response = client.DownloadString("https://127.0.0.1:8000"); // Adjust the URL as needed
					return true;
				}
			}
			catch (Exception)
			{
				// Unable to connect to the server
				return false;
			}
		}
	}
}
