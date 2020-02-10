namespace MasDen.Request.Pipeline
{
	public class RequestContext<TRequest, TResult>
			where TRequest : IRequest<TResult>
	{
		#region Constructors

		public RequestContext(TRequest request)
			: this(request, default(TResult))
		{
		}

		public RequestContext(TRequest request, TResult result)
		{
			this.Request = request;
			this.Result = result;
		}

		#endregion

		#region Public Properties

		public TRequest Request { get; }
		public TResult Result { get; }

		#endregion
	}
}