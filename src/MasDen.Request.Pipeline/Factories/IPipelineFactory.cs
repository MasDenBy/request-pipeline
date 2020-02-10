namespace MasDen.Request.Pipeline.Factories
{
	internal interface IPipelineFactory
	{
		Pipeline<TRequest, TResult> Create<TRequest, TResult>()
			where TRequest : IRequest<TResult>;
	}
}