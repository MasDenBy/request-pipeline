using System;
using System.Linq;
using System.Threading.Tasks;

using MasDen.Request.Pipeline.Factories;

using NSubstitute;

using NUnit.Framework;

namespace MasDen.Request.Pipeline.Test
{
	[TestFixture]
	public class PipelineExecutorTest
	{
		#region Private Fields

		private PipelineExecutor pipelineExecutor;

		private IPipelineFactory pipelineFactory;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.pipelineFactory = Substitute.For<IPipelineFactory>();
			this.pipelineExecutor = new PipelineExecutor(this.pipelineFactory);
		}

		#endregion

		#region Tests

		[Test]
		public void ExecuteAsyncIfPipelineIsNullShouldThrowExceptionTest()
		{
			// Arrange
			this.pipelineFactory.Create<TestRequest, object>().Returns((Pipeline<TestRequest, object>)null);

			// Act & Assert
			var exception = Assert.ThrowsAsync<InvalidOperationException>(
				async () => await this.pipelineExecutor.ExecuteAsync<TestRequest, object>(new TestRequest()));
			Assert.AreEqual("Pipeline for MasDen.Request.Pipeline.Test.TestRequest does not exist.", exception.Message);
		}

		[Test]
		public async Task ExecuteAsyncTest()
		{
			// Arrange
			var pipeline = new Pipeline<TestRequest, object>(
				Substitute.For<IRequestHandler<TestRequest, object>>(),
				new[] { Substitute.For<IPreRequestHandler<TestRequest, object>>() },
				new[] { Substitute.For<IPostRequestHandler<TestRequest, object>>() },
				new[] { Substitute.For<IFinishRequestHandler<TestRequest, object>>() });

			this.pipelineFactory.Create<TestRequest, object>().Returns(pipeline);

			// Act
			await this.pipelineExecutor.ExecuteAsync<TestRequest, object>(new TestRequest());

			// Assert
			await pipeline.RequestHandler.Received().Handle(Arg.Any<RequestContext<TestRequest, object>>());
			pipeline.PreRequestHandlers.ToList().ForEach(x => x.Received().Handle(Arg.Any<RequestContext<TestRequest, object>>()));
			pipeline.PostRequestHandlers.ToList().ForEach(x => x.Received().Handle(Arg.Any<RequestContext<TestRequest, object>>()));
			pipeline.FinishRequestHandlers.ToList().ForEach(x => x.Received().Handle(Arg.Any<RequestContext<TestRequest, object>>()));
		}

		#endregion
	}
}