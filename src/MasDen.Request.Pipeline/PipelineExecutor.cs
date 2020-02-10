using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MasDen.Request.Pipeline.Factories;

namespace MasDen.Request.Pipeline
{
	internal class PipelineExecutor : IPipelineExecutor
	{
		#region Private Fields

		private readonly IPipelineFactory pipelineFactory;

		#endregion

		#region Constructors

		public PipelineExecutor(IPipelineFactory pipelineFactory)
		{
			this.pipelineFactory = pipelineFactory;
		}

		#endregion

		#region IPipelineExecutor Members

		public async Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request)
			where TRequest : IRequest<TResult>
		{
			var pipeline = this.pipelineFactory.Create<TRequest, TResult>();

			if (pipeline == null)
				throw new InvalidOperationException($"Pipeline for {request.GetType()} does not exist.");

			var context = new RequestContext<TRequest, TResult>(request);

			try
			{
				context = await ExecuteRequestHandlersAsync(context, pipeline.PreRequestHandlers);
				context = await ExecuteRequestHandlersAsync(context, new[] { pipeline.RequestHandler });
				context = await ExecuteRequestHandlersAsync(context, pipeline.PostRequestHandlers);
			}
			finally
			{
				context = await ExecuteRequestHandlersAsync(context, pipeline.FinishRequestHandlers);
			}

			return context.Result;
		}

		#endregion

		#region Private Methods

		private static async Task<RequestContext<TRequest, TResult>> ExecuteRequestHandlersAsync<TRequest, TResult>(
			RequestContext<TRequest, TResult> requestContext,
			IEnumerable<IRequestHandler<TRequest, TResult>> requestHandlers)
			where TRequest : IRequest<TResult>
		{
			if (requestHandlers != null && requestHandlers.Any())
			{
				foreach (var handler in requestHandlers)
				{
					var result = await handler.ExecuteAsync(requestContext);
					requestContext = new RequestContext<TRequest, TResult>(requestContext.Request, result);
				}
			}

			return requestContext;
		}

		#endregion
	}
}