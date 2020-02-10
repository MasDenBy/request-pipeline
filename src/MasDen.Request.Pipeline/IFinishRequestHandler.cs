namespace MasDen.Request.Pipeline
{
	public interface IFinishRequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
		where TRequest : IRequest<TResult>
	{
	}
}