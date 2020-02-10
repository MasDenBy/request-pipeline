using System;
using System.Collections.Generic;

namespace MasDen.Request.Pipeline
{
	internal class Pipeline<TRequest, TResult>
		where TRequest : IRequest<TResult>
	{
		#region Constructors

		public Pipeline(IRequestHandler<TRequest, TResult> requestHandler,
			IEnumerable<IPreRequestHandler<TRequest, TResult>> preRequestHandlers,
			IEnumerable<IPostRequestHandler<TRequest, TResult>> postRequestHandlers,
			IEnumerable<IFinishRequestHandler<TRequest, TResult>> finishRequestHandlers)
		{
			this.RequestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
			this.PreRequestHandlers = preRequestHandlers ?? throw new ArgumentNullException(nameof(preRequestHandlers));
			this.PostRequestHandlers = postRequestHandlers ?? throw new ArgumentNullException(nameof(postRequestHandlers));
			this.FinishRequestHandlers = finishRequestHandlers ?? throw new ArgumentNullException(nameof(finishRequestHandlers));
		}

		#endregion

		#region Public Properties

		public IRequestHandler<TRequest, TResult> RequestHandler { get; }

		public IEnumerable<IPreRequestHandler<TRequest, TResult>> PreRequestHandlers { get; }

		public IEnumerable<IPostRequestHandler<TRequest, TResult>> PostRequestHandlers { get; }

		public IEnumerable<IFinishRequestHandler<TRequest, TResult>> FinishRequestHandlers { get; }

		#endregion
	}
}