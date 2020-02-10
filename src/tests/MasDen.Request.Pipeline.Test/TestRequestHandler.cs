using System;
using System.Threading.Tasks;

namespace MasDen.Request.Pipeline.Test
{
	public class TestRequestHandler : IRequestHandler<TestRequest, object>
	{
		public Task<object> ExecuteAsync(RequestContext<TestRequest, object> requestContext)
		{
			throw new NotImplementedException();
		}
	}
}