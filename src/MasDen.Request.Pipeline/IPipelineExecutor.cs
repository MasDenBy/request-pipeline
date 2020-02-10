using System.Threading.Tasks;

namespace MasDen.Request.Pipeline
{
	public interface IPipelineExecutor
	{
		Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request)
			where TRequest : IRequest<TResult>;
	}
}