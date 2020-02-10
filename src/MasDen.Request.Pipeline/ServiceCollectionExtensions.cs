using MasDen.Request.Pipeline.Factories;

using Microsoft.Extensions.DependencyInjection;

namespace MasDen.Request.Pipeline
{
	public static class ServiceCollectionExtensions
	{
		#region Public Methods

		public static IServiceCollection AddRequestPipeline(this IServiceCollection services)
		{
			services.AddScoped<IPipelineFactory, PipelineFactory>()
					.AddScoped<IPipelineExecutor, PipelineExecutor>();

			return services;
		}

		#endregion
	}
}