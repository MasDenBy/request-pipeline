namespace MasDen.Request.Pipeline
{
	public interface IPreRequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
		where TRequest : IRequest<TResult>
	{
	}
}