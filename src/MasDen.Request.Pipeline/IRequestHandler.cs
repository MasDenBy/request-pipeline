using System.Threading.Tasks;

namespace MasDen.Request.Pipeline
{
	public interface IRequestHandler<TRequest, TResult>
		where TRequest : IRequest<TResult>
	{
		Task<TResult> ExecuteAsync(RequestContext<TRequest, TResult> requestContext);
	}
}