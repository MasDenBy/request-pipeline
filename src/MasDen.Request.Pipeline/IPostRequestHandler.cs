namespace MasDen.Request.Pipeline
{
	public interface IPostRequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
		where TRequest : IRequest<TResult>
	{
	}
}