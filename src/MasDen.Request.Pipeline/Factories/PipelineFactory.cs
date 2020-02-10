using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace MasDen.Request.Pipeline.Factories
{
	internal class PipelineFactory : IPipelineFactory
	{
		#region Private Fields

		private readonly IServiceProvider serviceProvider;

		#endregion

		#region Constructors

		public PipelineFactory(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		#endregion

		#region IPipelineFactory Members

		public Pipeline<TRequest, TResult> Create<TRequest, TResult>()
			where TRequest : IRequest<TResult>
		{
			var handlers = this.serviceProvider.GetServices<IRequestHandler<TRequest, TResult>>();

			var requestHandler = handlers?.FirstOrDefault(x => 
				x.GetType().GetInterfaces().First() == typeof(IRequestHandler<TRequest, TResult>));

			if (requestHandler == null)
				throw new InvalidOperationException($"Request handler for the request {typeof(TRequest)} does not found.");

			return new Pipeline<TRequest, TResult>(
				requestHandler,
				handlers?.Where(x => x is IPreRequestHandler<TRequest, TResult>).Cast<IPreRequestHandler<TRequest, TResult>>(),
				handlers?.Where(x => x is IPostRequestHandler<TRequest, TResult>).Cast<IPostRequestHandler<TRequest, TResult>>(),
				handlers?.Where(x => x is IFinishRequestHandler<TRequest, TResult>).Cast<IFinishRequestHandler<TRequest, TResult>>());
		}

		#endregion
	}
}